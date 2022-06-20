using Apoteka.Controllers;
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

            var compositeController = InitializeControllers();

            var loginPage = new LoginPage(compositeController);

            mainFrame.Content = loginPage;

            loginPage.SuccessfullyLoggedIn += (User user) =>
            {
                var navigationManager = new NavigationManager(mainFrame, user, compositeController);
            };

            var numberOfOrderingsDone = compositeController.SchedulingController.OrderAllMedicinesScheduledForTodayOrForPreviousPeriod();
            if (numberOfOrderingsDone > 0)
            {
                MessageBox.Show($"Uspešno izvršeno {numberOfOrderingsDone} porudžbina");
            }
        }

        private CompositeController InitializeControllers()
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

            IUserController userController = new UserController(userService);
            IMedicineController medicineController = new MedicineController(medicineService);
            ISchedulingController schedulingController = new SchedulingController(schedulingService);
            IAcceptanceController acceptanceController = new AcceptanceController(acceptanceService);
            IIngredientsController ingredientsController = new IngredientsController(ingredientsService);

            return new CompositeController(userController, medicineController, acceptanceController, ingredientsController, schedulingController);

        }
    }
}
