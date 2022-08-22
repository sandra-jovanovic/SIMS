using Apoteka.Controllers;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Controllers
{
    public class CompositeController
    {
        private readonly UserController _userController;
        private readonly MedicineController _medicineController;
        private readonly SchedulingController _schedulingController;
        private readonly IngredientsController _ingredientController;

        public UserController UserController => _userController;
        public MedicineController MedicineController => _medicineController;
        public SchedulingController SchedulingController => _schedulingController;
        public IngredientsController IngredientsController => _ingredientController;

        public CompositeController(UserController userController, MedicineController medicineController,
                                   IngredientsController ingredientsController, SchedulingController schedulingController)
        {
            _userController = userController;
            _medicineController = medicineController;
            _ingredientController = ingredientsController;
            _schedulingController = schedulingController;
        }
    }
}
