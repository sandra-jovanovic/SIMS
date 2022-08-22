using Apoteka.Controllers;
using Apoteka.Models;
using Apoteka.Pages;
using Apoteka.Pages.Menus;
using Apoteka.Services;
using System.Windows.Controls;

namespace Apoteka.Util
{
    public class NavigationManager
    {
        private Frame mainFrame;
        private readonly User user;
        private readonly UserController _userController;
        private readonly MedicineController _medicineController;
        private readonly IngredientsController _ingredientController;
        private readonly SchedulingController _schedulingController;

        public NavigationManager(Frame mainFrame, User user, CompositeController compositeController)
        {
            this.mainFrame = mainFrame;
            this.user = user;
            _userController = compositeController.UserController;
            _medicineController = compositeController.MedicineController;
            _schedulingController = compositeController.SchedulingController;
            _ingredientController = compositeController.IngredientsController;

            switch (user.Role)
            {
                case UserRole.Farmaceut:
                    HandleNavigationForPharmacist();
                    break;
                case UserRole.Lekar:
                    HandleNavigationForDoctor();
                    break;
                case UserRole.Upravnik:
                    HandleNavigationForAdmin();
                    break;
                default:
                    break;
            }
        }

        private void HandleNavigationForAdmin()
        {
            var adminMenu = new AdminMenu();

            adminMenu.registrationButtonClicked += () =>
            {
                var page = new RegistrationPage(_userController);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.usersOverviewButtonClicked += () =>
            {
                var page = new UsersOverviewPage(_userController);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.newMedicneButtonClicked += () =>
            {
                var page = new NewMedicinePage(_ingredientController, _medicineController);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.medicinesOverviewButtonClicked += () =>
            {
                var page = new AcceptedMedicinesOverviewPage(_medicineController);
                page.BackButtonPressed += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.medicinesOrderingButtonClicked += () =>
            {
                var page = new MedicineOrderingPage(_medicineController, _schedulingController);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            mainFrame.Content = adminMenu;
        }

        private void HandleNavigationForDoctor()
        {
            var doctorMenu = new DoctorMenu();

            doctorMenu.AllMedicinesButtonClicked += () =>
            {
                var page = new AcceptedMedicinesOverviewPage(_medicineController);
                page.BackButtonPressed += () => HandleNavigationForDoctor();
                mainFrame.Content = page;
            };

            doctorMenu.AcceptOrRefuseMedicinesButtonClicked += () =>
            {
                var page = new MedicinesWaitingForAcceptancePage(user, _medicineController);
                page.backButtonClicked += () => HandleNavigationForDoctor();
                mainFrame.Content = page;
            };

            mainFrame.Content = doctorMenu;
        }

        private void HandleNavigationForPharmacist()
        {
            var pharmacisMenu = new PharmacistMenu();

            pharmacisMenu.AllMedicinesButtonClicked += () =>
            {
                var page = new AcceptedMedicinesOverviewPage(_medicineController);
                page.BackButtonPressed += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            pharmacisMenu.AcceptOrRefuseMedicinesButtonClicked += () =>
            {
                var page = new MedicinesWaitingForAcceptancePage(user, _medicineController);
                page.backButtonClicked += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            pharmacisMenu.RefusedMedicinesButtonClicked += () =>
            {
                var page = new RefusedMedicinesOverviewPage(user, _medicineController);
                page.BackButtonPressed += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            mainFrame.Content = pharmacisMenu;
        }
    }
}
