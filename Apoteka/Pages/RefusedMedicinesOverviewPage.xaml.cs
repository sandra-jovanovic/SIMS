using Apoteka.Constants;
using Apoteka.Controllers;
using Apoteka.Models;
using Apoteka.Services;
using Apoteka.Util;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for RefusedMedicinesOverviewPage.xaml
    /// </summary>
    public partial class RefusedMedicinesOverviewPage : Page
    {
        private List<string> comboBoxOptions = new List<string>()
        {
            MedicineSeachingFilters.ID,
            MedicineSeachingFilters.NAME,
            MedicineSeachingFilters.MANUFACTURER,
            MedicineSeachingFilters.PRICE_RANGE,
            MedicineSeachingFilters.QUANTITY,
            MedicineSeachingFilters.INGREDIENTS
        };
        private List<Medicine> medicines;
        private readonly User user;
        private readonly IMedicineController _medicineController;
        private readonly IAcceptanceController _acceptanceController;

        public event Action BackButtonPressed;

        public RefusedMedicinesOverviewPage(User user, IMedicineController medicineController, IAcceptanceController acceptanceController)
        {
            InitializeComponent();
            this.user = user;
            this._medicineController = medicineController;
            this._acceptanceController = acceptanceController;
            this.medicines = this._medicineController.GetRefusedMedicines();

            dgMedicines.ItemsSource = medicines;

            cbSearchingType.ItemsSource = comboBoxOptions;

            tfSearch.TextChanged += (object sender, TextChangedEventArgs args) => HandleTextChanged();
            cbSearchingType.SelectionChanged += (object sender, SelectionChangedEventArgs args) => HandleComboboxChanged();
        }

        private void HandleComboboxChanged()
        {
            lblHelp.Visibility = System.Windows.Visibility.Hidden;
            dgMedicines.ItemsSource = medicines;
            tfSearch.Text = "";

            if (comboBoxOptions[cbSearchingType.SelectedIndex] == MedicineSeachingFilters.PRICE_RANGE)
            {
                lblHelp.Visibility = System.Windows.Visibility.Visible;
                lblHelp.Content = "Koristiti format x,y";
            }
            else if (comboBoxOptions[cbSearchingType.SelectedIndex] == MedicineSeachingFilters.PRICE_RANGE)
            {
                lblHelp.Visibility = System.Windows.Visibility.Visible;
                lblHelp.Content = "Koristiti & i | bez razmaka";
            }
        }

        private void HandleTextChanged()
        {
            var userInput = tfSearch.Text.ToLower().Trim();
            if (userInput == String.Empty)
            {
                dgMedicines.ItemsSource = medicines;
                return;
            }

            var selectedIndexInComboBox = cbSearchingType.SelectedIndex;

            if (selectedIndexInComboBox == -1) return;

            switch (comboBoxOptions[selectedIndexInComboBox])
            {
                case MedicineSeachingFilters.ID:
                    dgMedicines.ItemsSource = medicines.FindAll(medicine => medicine.Id.ToString().Contains(userInput));
                    break;

                case MedicineSeachingFilters.NAME:
                    dgMedicines.ItemsSource = medicines.FindAll(medicine => medicine.Name.ToLower().Contains(userInput));
                    break;

                case MedicineSeachingFilters.MANUFACTURER:
                    dgMedicines.ItemsSource = medicines.FindAll(medicine => medicine.Manufacturer.ToLower().Contains(userInput));
                    break;

                case MedicineSeachingFilters.PRICE_RANGE:
                    var splittedString = userInput.Split(',');
                    if (splittedString.Length != 2)
                    {
                        dgMedicines.ItemsSource = medicines;
                        return;
                    }

                    int minVal;
                    int maxVal;
                    try
                    {
                        minVal = int.Parse(splittedString[0]);
                        maxVal = int.Parse(splittedString[1]);
                    }
                    catch
                    {
                        dgMedicines.ItemsSource = medicines;
                        return;
                    }

                    dgMedicines.ItemsSource = medicines.FindAll(medicine => minVal <= medicine.Price && medicine.Price <= maxVal);
                    break;

                case MedicineSeachingFilters.QUANTITY:
                    dgMedicines.ItemsSource = medicines.FindAll(medicine => medicine.Quantity.ToString().Equals(userInput));
                    break;

                case MedicineSeachingFilters.INGREDIENTS:
                    dgMedicines.ItemsSource = SearchingHelper.GetMedicinesUsingIngredientsFilter(medicines, userInput);
                    break;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonPressed();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;
            _acceptanceController.AcceptMedicineByUser(user.JMBG, medicine.Id, user.Role == UserRole.Lekar);
            _medicineController.UnmarkMedicineAsRefused(medicine.Id);
            this.medicines = this.medicines.FindAll(iter => iter.Id != medicine.Id);
            dgMedicines.ItemsSource = medicines;
        }
    }
}
