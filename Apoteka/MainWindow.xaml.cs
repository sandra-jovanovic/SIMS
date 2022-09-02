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
            IAcceptanceService acceptanceService = new AcceptanceService(acceptanceRepository);
            IMedicineService medicineService = new MedicineService(medicineRepository, acceptanceService);
            ISchedulingService schedulingService = new SchedulingService(schedulingRepository, medicineService);
            IIngredientsService ingredientsService = new IngredientsService(ingredientRepository);

            UserController userController = new UserController(userService);
            MedicineController medicineController = new MedicineController(medicineService);
            SchedulingController schedulingController = new SchedulingController(schedulingService);
            IngredientsController ingredientsController = new IngredientsController(ingredientsService);

            return new CompositeController(userController, medicineController, ingredientsController, schedulingController);

        }
    }
}
