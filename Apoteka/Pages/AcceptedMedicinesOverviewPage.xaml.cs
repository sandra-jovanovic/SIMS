using Apoteka.Constants;
using Apoteka.Controllers;
using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

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
        private readonly MedicineController _medicineController;
        public event Action BackButtonPressed;

        public AcceptedMedicinesOverviewPage(MedicineController medicineController)
        {
            InitializeComponent();
            this._medicineController = medicineController;
            this.medicines = this._medicineController.GetAllAcceptedMedicines();

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

            dgMedicines.ItemsSource = _medicineController.SearchAcceptedMedicines(comboBoxOptions[selectedIndexInComboBox], userInput);
        }

        private void btnBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            BackButtonPressed();
        }
    }
}
