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

            var numberOfOrderingsDone = compositeService.SchedulingService.OrderAllMedicinesScheduledForTodayOrForPreviousPeriod();
            if (numberOfOrderingsDone > 0)
            {
                MessageBox.Show($"Uspešno izvršeno {numberOfOrderingsDone} porudžbina");
            }
        }

        private CompositeService InitializeServices()
        {
            IUserRepository userRepository = new UserRepository();
            IMedicineRepository medicineRepository = new MedicineRepository();
            ISchedulingRepository schedulingRepository = new SchedulingRepository();
            IAcceptanceRepository acceptanceRepository = new AcceptanceRepository();
            IIngredientRepository ingredientRepository = new IngredientRepository();

            IUserService userService = new UserService(userRepository);
            IMedicineService medicineService = new MedicineService(medicineRepository);
            ISchedulingService schedulingService = new SchedulingService(schedulingRepository, medicineRepository);
            IAcceptanceService acceptanceService = new AcceptanceService(acceptanceRepository);
            IIngredientsService ingredientsService = new IngredientsService(ingredientRepository);

            return new CompositeService(userService, medicineService, acceptanceService, ingredientsService, schedulingService);

        }
    }
}
