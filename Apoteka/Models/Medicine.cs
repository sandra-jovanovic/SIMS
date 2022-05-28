using System;
using System.Collections.Generic;

namespace Apoteka.Models
{
    [Serializable]
    public class Medicine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Dictionary<string, Ingredient> Ingredients { get; set; }
        public bool Accepted { get; set; }
        public bool Refused { get; set; }
        public string RefusedBy { get; set; }
        public string ReasonForRefusing { get; set; }

        public Medicine()
        {

        }

        public Medicine(int id, string name, string manufacturer, double price, int quantity, Dictionary<string, Ingredient> ingredients)
        {
            Id = id;
            Name = name;
            Manufacturer = manufacturer;
            Price = price;
            Quantity = quantity;
            Ingredients = ingredients;
        }
    }
}
