using Apoteka.Models;
using Apoteka.Pages;
using Apoteka.Repositories;
using Apoteka.Services;
using Apoteka.Util;
using System.Windows;

namespace Apoteka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var compositeService = InitializeServices();

            var loginPage = new LoginPage(compositeService);

            mainFrame.Content = loginPage;

            loginPage.SuccessfullyLoggedIn += (User user) =>
            {
                var navigationManager = new NavigationManager(mainFrame, user, compositeService);
            };
        }

        private CompositeService InitializeServices()
        {
            IUserRepository userRepository = new UserRepository();
            IMedicineRepository medicineRepository = new MedicineRepository();
            IAcceptanceRepository acceptanceRepository = new AcceptanceRepository();

            IUserService userService = new UserService(userRepository);
            IMedicineService medicineService = new MedicineService(medicineRepository);
            IAcceptanceService acceptanceService = new AcceptanceService(acceptanceRepository);

            return new CompositeService(userService, medicineService, acceptanceService);

        }
    }
}
