using Apoteka.Constants;
using Apoteka.Controllers;
using Apoteka.Models;
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
        private readonly MedicineController _medicineController;

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

        public event Action BackButtonPressed;

        public RefusedMedicinesOverviewPage(User user, MedicineController medicineController)
        {
            InitializeComponent();
            this.user = user;
            this._medicineController = medicineController;
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

            dgMedicines.ItemsSource = _medicineController.SearchRefusedMedicines(comboBoxOptions[selectedIndexInComboBox], userInput);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonPressed();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;
            _medicineController.UnmarkMedicineAsRefused(medicine.Id, user);

            this.medicines = this.medicines.FindAll(iter => iter.Id != medicine.Id);
            dgMedicines.ItemsSource = medicines;

            HandleTextChanged();
        }
    }
}
