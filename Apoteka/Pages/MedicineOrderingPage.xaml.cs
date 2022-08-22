using Apoteka.Controllers;
using Apoteka.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for MedicineOrderingPage.xaml
    /// </summary>
    public partial class MedicineOrderingPage : Page
    {
        private readonly MedicineController _medicineController;
        private readonly SchedulingController schedulingController;

        public event Action BackButtonClicked;
        public MedicineOrderingPage(MedicineController medicineController, SchedulingController schedulingController)
        {
            InitializeComponent();
            this._medicineController = medicineController;
            this.schedulingController = schedulingController;

            cbMedicines.ItemsSource = _medicineController.GetAllAcceptedMedicines();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            lblError.Visibility = Visibility.Hidden;

            if (cbMedicines.SelectedIndex == -1)
            {
                lblError.Content = "Izaberite lek za poručivanje";
                lblError.Visibility = Visibility.Visible;
                return;
            }

            if (txtQuantity.Text == "")
            {
                lblError.Content = "Unesite količinu za poručivanje";
                lblError.Visibility = Visibility.Visible;
                return;
            }

            int quantity;
            try
            {
                quantity = int.Parse(txtQuantity.Text);
                if (quantity <= 0)
                    throw new Exception();

            }
            catch
            {
                lblError.Content = "Uneta količina mora da bude brojna vrednost veća od 0";
                lblError.Visibility = Visibility.Visible;
                return;
            }

            var medicine = (Medicine)cbMedicines.SelectedItem;
            var selecteDate = dpOrderScheduling.SelectedDate;

            if (selecteDate is null)
            {
                OrderNow(medicine, quantity);
            }
            else
            {
                ScheduleOrdering(medicine, quantity, selecteDate.Value);
            }

            cbMedicines.SelectedIndex = -1;
            txtQuantity.Text = "";
            dpOrderScheduling.SelectedDate = null;
        }

        private void OrderNow(Medicine medicine, int quantity)
        {
            _medicineController.IncreaseMedicineQuantity(medicine.Id, quantity);

            MessageBox.Show("Količina je uspešno nabavljena");
        }

        private void ScheduleOrdering(Medicine medicine, int quantity, DateTime scheduledDate)
        {
            schedulingController.ScheduleOrderingForDate(new ScheduledMedicineOrdering(scheduledDate, medicine.Id, quantity));
            MessageBox.Show($"Nabavka je sačuvana za datum {scheduledDate}");
        }
    }
}
