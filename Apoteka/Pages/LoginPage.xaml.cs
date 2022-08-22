using Apoteka.Controllers;
using Apoteka.Models;
using Apoteka.Services;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public event Action<User> SuccessfullyLoggedIn;
        private int numberOfLoginsLeft = 3;
        private readonly UserController _userController;

        public LoginPage(CompositeController compositeController)
        {
            InitializeComponent();
            _userController = compositeController.UserController;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var email = tfEmail.Text;
            var password = tfPassword.Password;

            var user = AuthenticateUser(email, password);

            if (user == null)
            {
                lblError.Visibility = Visibility.Visible;
                numberOfLoginsLeft--;
                lblError.Content = $"Korisničko ime ili lozinka nisu ispravni. Preostalo pokušaja {numberOfLoginsLeft}.";

                if (numberOfLoginsLeft == 0)
                {
                    App.Current.Shutdown();
                }
            }
            else
            {
                SuccessfullyLoggedIn(user);
            }

        }

        private User AuthenticateUser(string email, string password)
        {
            return _userController.AuthenticateUser(email, password);
        }
    }
}
