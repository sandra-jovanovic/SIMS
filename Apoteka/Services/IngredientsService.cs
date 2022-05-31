using Apoteka.Models;
using Apoteka.Repositories;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public class IngredientsService : IIngredientsService
    {
        private IIngredientRepository ingredientRepository;

        public IngredientsService(IIngredientRepository ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return ingredientRepository.GetAllIngredients();
        }
    }
}
