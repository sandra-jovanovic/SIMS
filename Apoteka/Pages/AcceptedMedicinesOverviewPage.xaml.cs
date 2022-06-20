using Apoteka.Constants;
using Apoteka.Controllers;
using Apoteka.Models;
using Apoteka.Services;
using Apoteka.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for MedicinesOverviewPage.xaml
    /// </summary>
    public partial class AcceptedMedicinesOverviewPage : Page
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
        private readonly List<Medicine> medicines;
        private readonly IMedicineController _medicineService;
        public event Action BackButtonPressed;

        public AcceptedMedicinesOverviewPage(IMedicineController medicineService)
        {
            InitializeComponent();
            this._medicineService = medicineService;
            this.medicines = this._medicineService.GetAllAcceptedMedicines();

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
            else if (comboBoxOptions[cbSearchingType.SelectedIndex] == MedicineSeachingFilters.INGREDIENTS)
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

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BackButtonPressed();
        }
    }
}
