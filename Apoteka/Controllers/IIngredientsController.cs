using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public interface IIngredientsController
    {
        List<Ingredient> GetAllIngredients();
    }
}
