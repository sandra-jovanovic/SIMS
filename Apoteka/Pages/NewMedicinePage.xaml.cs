using Apoteka.Exceptions;
using Apoteka.Models;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Apoteka.Pages
{
    /// <summary>
    /// Interaction logic for NewMedicinePage.xaml
    /// </summary>
    public partial class NewMedicinePage : Page
    {
        private readonly IMedicineService _medicineService;
        private readonly IIngredientsService _ingredientsService;

        public event Action BackButtonClicked;
        public NewMedicinePage(IIngredientsService ingredientsService, IMedicineService medicineService)
        {
            InitializeComponent();
            this._medicineService = medicineService;
            this._ingredientsService = ingredientsService;
            lbIngredients.ItemsSource = _ingredientsService.GetAllIngredients();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BackButtonClicked();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            lbError.Content = "";
            lbError.Visibility = Visibility.Hidden;

            if (txtId.Text == "" || txtManufacturer.Text == "" || txtName.Text == "" || txtQuantity.Text == "" || txtPrice.Text == "")
            {
                lbError.Content = "Sva polja moraju da budu popunjena";
                lbError.Visibility = Visibility.Visible;
                return;
            }

            if (lbIngredients.SelectedItems.Count == 0)
            {
                lbError.Content = "Minium 1 sastojak mora da bude selektovan";
                lbError.Visibility = Visibility.Visible;
                return;
            }

            int id;
            double price;
            int quantity;
            try
            {
                id = int.Parse(txtId.Text);
                price = double.Parse(txtPrice.Text);
                quantity = int.Parse(txtQuantity.Text);
            }
            catch
            {
                lbError.Content = "Vrednosi šifre, cene i količine moraju da budu brojne vrednosti";
                lbError.Visibility = Visibility.Visible;
                return;
            }

            var ingredientsDict = new Dictionary<string, Ingredient>();
            foreach (var ingredient in lbIngredients.SelectedItems)
            {
                var castedIngredient = (Ingredient)ingredient;
                ingredientsDict.Add(castedIngredient.Name, castedIngredient);
            }

            var newMedicine = new Medicine(id, txtName.Text, txtManufacturer.Text, price, quantity, ingredientsDict);

            try
            {
                _medicineService.AddMedicine(newMedicine);
            }
            catch (ExistingIdException ex)
            {
                lbError.Content = "Lek sa datom šifrom već postoji";
                lbError.Visibility = Visibility.Visible;
                return;
            }

            MessageBox.Show("Lek je uspešno sačuvan");

            txtId.Text = "";
            txtManufacturer.Text = "";
            txtName.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
            lbIngredients.UnselectAll();
        }
    }
}
