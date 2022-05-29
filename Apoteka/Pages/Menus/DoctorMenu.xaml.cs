using System;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages.Menus
{
    /// <summary>
    /// Interaction logic for DoctorMenu.xaml
    /// </summary>
    public partial class DoctorMenu : Page
    {
        public event Action AllMedicinesButtonClicked;
        public event Action AcceptOrRefuseMedicinesButtonClicked;
        public DoctorMenu()
        {
            InitializeComponent();
        }

        private void btnAllMedicines_Click(object sender, RoutedEventArgs e)
        {
            AllMedicinesButtonClicked();
        }

        private void btnMedicinesAcceptingAndRefusing_Click(object sender, RoutedEventArgs e)
        {
            AcceptOrRefuseMedicinesButtonClicked();
        }
    }
}
