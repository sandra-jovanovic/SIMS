using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public interface IIngredientsService
    {
        List<Ingredient> GetAllIngredients();
    }
}
