using Apoteka.Dialogs;
using Apoteka.Models;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Data;
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
        private readonly User user;
        private readonly IMedicineService _medicineService;
        private readonly IAcceptanceService _acceptanceService;
        private List<Medicine> medicines;
        public event Action backButtonClicked;

        public MedicinesWaitingForAcceptancePage(User user, IMedicineService medicineService, IAcceptanceService acceptanceService)
        {
            InitializeComponent();
            this.user = user;
            this._medicineService = medicineService;
            this._acceptanceService = acceptanceService;

            this.medicines = this._medicineService.GetNotAcceptedMedicines();
            var acceptances = _acceptanceService.GetAcceptancesByUser(user.JMBG);

            this.medicines.ForEach(medicine =>
            {
                var acceptance = acceptances.Find(acceptance => acceptance.JMBG == user.JMBG && acceptance.MedicineId == medicine.Id);
                medicine.Accepted = acceptance != null;
            });

            dgMedicines.ItemsSource = this.medicines;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!dgMedicines.IsLoaded)
            {
                return;
            }

            Medicine medicine = (Medicine)((CheckBox)e.Source).DataContext;
            var medicineApproved = _acceptanceService.AcceptMedicineByUser(user.JMBG, medicine.Id, user.Role == UserRole.Lekar);
            if (medicineApproved)
            {
                _medicineService.MarkMedicineAsApproved(medicine.Id);
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
            _acceptanceService.RevokeMedicineAcceptanceByUser(user.JMBG, medicine.Id);
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
                this.medicines = medicines.Where(x => x.Id != medicine.Id).ToList();
                dgMedicines.ItemsSource = this.medicines;
                _medicineService.SetMedicineRefused(medicine.Id, dialog.ResponseText, $"{user.Name} {user.Surname}");
                _acceptanceService.DeleteAllAcceptancesForMedicine(medicine.Id);
            }

            ((CheckBox)sender).IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            backButtonClicked();
        }
    }
}
