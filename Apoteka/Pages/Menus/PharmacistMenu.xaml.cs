using System;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages.Menus
{
    /// <summary>
    /// Interaction logic for PharmacistMenu.xaml
    /// </summary>
    public partial class PharmacistMenu : Page
    {
        public event Action AllMedicinesButtonClicked;
        public event Action AcceptOrRefuseMedicinesButtonClicked;
        public event Action RefusedMedicinesButtonClicked;
        public PharmacistMenu()
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

        private void btnRefusedMedicinesOverview_Click(object sender, RoutedEventArgs e)
        {
            RefusedMedicinesButtonClicked();
        }
    }
}
