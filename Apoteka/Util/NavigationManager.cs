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
        private readonly IUserService _userService;
        private readonly IMedicineService _medicineService;
        private readonly IAcceptanceService _acceptanceService;
        private readonly IIngredientsService _ingredientService;

        public NavigationManager(Frame mainFrame, User user, CompositeService compositeService)
        {
            this.mainFrame = mainFrame;
            this.user = user;
            _userService = compositeService.UserService;
            _medicineService = compositeService.MedicineService;
            _acceptanceService = compositeService.AcceptanceService;
            _ingredientService = compositeService.IngredientsService;

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
                var page = new RegistrationPage(_userService);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.usersOverviewButtonClicked += () =>
            {
                var page = new UsersOverviewPage(_userService);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.newMedicneButtonClicked += () =>
            {
                var page = new NewMedicinePage(_ingredientService, _medicineService);
                page.BackButtonClicked += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.medicinesOverviewButtonClicked += () =>
            {
                var page = new AcceptedMedicinesOverviewPage(_medicineService);
                page.BackButtonPressed += () => HandleNavigationForAdmin();
                mainFrame.Content = page;
            };

            adminMenu.medicinesOrderingButtonClicked += () =>
            {};

            mainFrame.Content = adminMenu;
        }

        private void HandleNavigationForDoctor()
        {
            var doctorMenu = new DoctorMenu();

            doctorMenu.AllMedicinesButtonClicked += () =>
            {
                var page = new AcceptedMedicinesOverviewPage(_medicineService);
                page.BackButtonPressed += () => HandleNavigationForDoctor();
                mainFrame.Content = page;
            };

            doctorMenu.AcceptOrRefuseMedicinesButtonClicked += () =>
            {
                var page = new MedicinesWaitingForAcceptancePage(user, _medicineService, _acceptanceService);
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
                var page = new AcceptedMedicinesOverviewPage(_medicineService);
                page.BackButtonPressed += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            pharmacisMenu.AcceptOrRefuseMedicinesButtonClicked += () =>
            {
                var page = new MedicinesWaitingForAcceptancePage(user, _medicineService, _acceptanceService);
                page.backButtonClicked += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            pharmacisMenu.RefusedMedicinesButtonClicked += () =>
            {
                var page = new RefusedMedicinesOverviewPage(user, _medicineService, _acceptanceService);
                page.BackButtonPressed += () => HandleNavigationForPharmacist();
                mainFrame.Content = page;
            };

            mainFrame.Content = pharmacisMenu;
        }
    }
}
