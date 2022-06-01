﻿using Apoteka.Exceptions;
using Apoteka.Models;
using Apoteka.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        private readonly IUserService _userService;
        public event Action BackButtonClicked;

        public RegistrationPage(IUserService userService)
        {
            InitializeComponent();

            this._userService = userService;

            cbUserRole.ItemsSource = Enum.GetValues(typeof(UserRole));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            lblError.Visibility = Visibility.Hidden;

            if (txtName.Text == "" || txtSurname.Text == "" || txtEmail.Text == "" ||
                txtJMBG.Text == "" || txtMobilePhone.Text == "" || txtPassword.Text == "" || cbUserRole.Text == "")
            {
                lblError.Content = "Potrebno je da sva polja budu popunjena";
                lblError.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                _userService.save(new User(txtJMBG.Text, txtEmail.Text, txtPassword.Text,
                                             txtName.Text, txtSurname.Text, txtMobilePhone.Text,
                                             (UserRole)cbUserRole.SelectedItem, false));
            }
            catch (ExistingIdException ex)
            {
                lblError.Content = "Korisnik sa unetim JMBG-om već postoji";
                lblError.Visibility = Visibility.Visible;
                return;
            }

            MessageBox.Show("Korisnik uspešno sačuvan");

            txtEmail.Text = "";
            txtJMBG.Text = "";
            txtMobilePhone.Text = "";
            txtName.Text = "";
            txtPassword.Text = "";
            txtSurname.Text = "";
            cbUserRole.Text = "";
        }
    }
}