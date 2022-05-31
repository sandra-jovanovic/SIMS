using Apoteka.Models;
using Apoteka.Services;
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
        private readonly IMedicineService _medicineService;
        private readonly ISchedulingService _schedulingService;

        public event Action BackButtonClicked;
        public MedicineOrderingPage(IMedicineService medicineService, ISchedulingService schedulingService)
        {
            InitializeComponent();
            this._medicineService = medicineService;
            this._schedulingService = schedulingService;
            cbMedicines.ItemsSource = _medicineService.GetAllAcceptedMedicines();
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
                lblError.Content = "Unteta količina mora da bude brojna vrednost veća od 0";
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
            _medicineService.IncreaseMedicineQuantity(medicine.Id, quantity);

            MessageBox.Show("Količina je uspešno nabavljena");
        }

        private void ScheduleOrdering(Medicine medicine, int quantity, DateTime scheduledDate)
        {
            _schedulingService.ScheduleOrderingForDate(new ScheduledMedicineOrdering(scheduledDate, medicine.Id, quantity));
            MessageBox.Show($"Nabavka je sačuvana za datum {scheduledDate}");
        }
    }
}
