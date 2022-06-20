using Apoteka.Controllers;
using Apoteka.Models;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for UsersOverviewPage.xaml
    /// </summary>
    public partial class UsersOverviewPage : Page
    {
        private readonly IUserController _userController;
        private List<User> users;

        public event Action BackButtonClicked;
        public UsersOverviewPage(IUserController userService)
        {
            InitializeComponent();
            this._userController = userService;
            users = _userController.GetAllUsers();

            dgUsers.ItemsSource = users;

            cbUserRole.ItemsSource = Enum.GetValues(typeof(UserRole));
            cbUserRole.SelectionChanged += (object sender, SelectionChangedEventArgs args) => HandleComboboxChanged();
        }

        private void HandleComboboxChanged()
        {
            var filteredUsers = users.FindAll(x => x.Role.Equals(cbUserRole.SelectedItem));
            dgUsers.ItemsSource = filteredUsers;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!dgUsers.IsLoaded)
            {
                return;
            }

            User user = (User)((CheckBox)e.Source).DataContext;

            _userController.BlockUser(user);

            users.ForEach(iterUser =>
            {
                if (iterUser.JMBG == user.JMBG) user.Blocked = true;
            });
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!dgUsers.IsLoaded)
            {
                return;
            }

            User user = (User)((CheckBox)e.Source).DataContext;

            _userController.UnblockUser(user);

            users.ForEach(iterUser =>
            {
                if (iterUser.JMBG == user.JMBG) user.Blocked = false;
            });
        }
    }
}
