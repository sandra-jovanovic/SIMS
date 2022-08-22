using Apoteka.Controllers;
using Apoteka.Dialogs;
using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for MedicinesWaitingForAcceptancePage.xaml
    /// </summary>
    public partial class MedicinesWaitingForAcceptancePage : Page
    {
        private readonly MedicineController _medicineController;

        private readonly User user;
        private List<Medicine> medicines;
        public event Action backButtonClicked;

        public MedicinesWaitingForAcceptancePage(User user, MedicineController medicineService)
        {
            InitializeComponent();
            this.user = user;
            this._medicineController = medicineService;

            this.medicines = this._medicineController.GetNotAcceptedMedicines(user);
            dgMedicines.ItemsSource = this.medicines;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!dgMedicines.IsLoaded)
            {
                return;
            }

            Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;

            bool isAccepted = _medicineController.AcceptMedicine(medicine, user);
            if (isAccepted)
            {
                this.medicines = this.medicines.FindAll(iter => iter.Id != medicine.Id);
                dgMedicines.ItemsSource = this.medicines;
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!dgMedicines.IsLoaded)
            {
                return;
            }

            Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;
            _medicineController.RevokeMedicineAcceptanceByUser(user, medicine);
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (!dgMedicines.IsLoaded)
            {
                return;
            }

            var dialog = new ConfirmRefusingMedicineDialog();

            if (dialog.ShowDialog() == true)
            {
                Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;

                _medicineController.RefuseMedicine(medicine.Id, dialog.ResponseText, $"{user.Name} {user.Surname}");

                this.medicines = medicines.Where(x => x.Id != medicine.Id).ToList();
                dgMedicines.ItemsSource = this.medicines;
            }

            ((CheckBox)sender).IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            backButtonClicked();
        }
    }
}
