using System.Windows;

namespace Apoteka.Dialogs
{
    /// <summary>
    /// Interaction logic for ConfirmRefusingMedicineDialog.xaml
    /// </summary>
    public partial class ConfirmRefusingMedicineDialog : Window
    {
        public ConfirmRefusingMedicineDialog()
        {
            InitializeComponent();
        }

        public string ResponseText
        {
            get { return ResponseTextBox.Text; }
            set { ResponseTextBox.Text = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
