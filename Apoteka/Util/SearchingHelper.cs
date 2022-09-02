using Apoteka.Models;
using System.Collections.Generic;
using System.Linq;

namespace Apoteka.Util
{
    public class SearchingHelper
    {
        public static IEnumerable<Medicine> GetMedicinesUsingIngredientsFilter(IEnumerable<Medicine> medicines, string userInput)
        {
            IEnumerable<Medicine> filteredMedicines = new List<Medicine>();

            try
            {
                var splittedByOr = userInput.Split('|');
                foreach (string partOr in splittedByOr)
                {
                    IEnumerable<Medicine> medicinesBeforeOrFilter = medicines.Select(m => m).ToList();
                    var splittedByAnd = partOr.Split('&');
                    foreach (string partAnd in splittedByAnd)
                    {
                        medicinesBeforeOrFilter = medicinesBeforeOrFilter.Intersect(medicines.Where(m => m.Ingredients.ContainsKey(partAnd)));
                    }
                    filteredMedicines = filteredMedicines.Union(medicinesBeforeOrFilter).ToList();
                }
            }
            catch
            {

            }

            return filteredMedicines;
        }
    }
}
