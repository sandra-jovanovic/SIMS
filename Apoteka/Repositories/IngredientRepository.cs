using Apoteka.Models;
using System.Collections.Generic;
using System.IO;

namespace Apoteka.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private const string filePath = "ingredients.txt";

        public List<Ingredient> GetAllIngredients()
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) ;
            }

            var ingredients = new List<Ingredient>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var ingredient = parseIngredientLine(line);
                    ingredients.Add(ingredient);
                }

            }

            return ingredients;
        }

        private Ingredient parseIngredientLine(string line)
        {
            var fields = line.Split(",");
            var name = fields[0];
            var description = fields[1];

            return new Ingredient(name, description);
        }
    }
}
