using System;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages.Menus
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        public event Action registrationButtonClicked;
        public event Action usersOverviewButtonClicked;
        public event Action newMedicneButtonClicked;
        public event Action medicinesOverviewButtonClicked;
        public event Action medicinesOrderingButtonClicked;
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            registrationButtonClicked();
        }

        private void btnUsersOverview_Click(object sender, RoutedEventArgs e)
        {
            usersOverviewButtonClicked();
        }

        private void btnNewMedicine_Click(object sender, RoutedEventArgs e)
        {
            newMedicneButtonClicked();
        }

        private void btnMedicinesOverview_Click(object sender, RoutedEventArgs e)
        {
            medicinesOverviewButtonClicked();
        }

        private void btnOrderMedicines_Click(object sender, RoutedEventArgs e)
        {
            medicinesOrderingButtonClicked();
        }
    }
}
