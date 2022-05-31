using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Repositories
{
    public interface IIngredientRepository
    {
        List<Ingredient> GetAllIngredients();
    }
}
