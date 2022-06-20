using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Services;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public class IngredientsController : IIngredientsController
    {
        private IIngredientsService ingredientService;

        public IngredientsController(IIngredientsService ingredientService)
        {
            this.ingredientService = ingredientService;
        }

        public List<Ingredient> GetAllIngredients()
        {
            return ingredientService.GetAllIngredients();
        }
    }
}
