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
        private readonly IUserController _userController;
        private readonly IMedicineController _medicineController;
        private readonly ISchedulingController _schedulingController;
        private readonly IAcceptanceController _acceptanceController;
        private readonly IIngredientsController _ingredientController;

        public IUserController UserController => _userController;
        public IMedicineController MedicineController => _medicineController;
        public ISchedulingController SchedulingController => _schedulingController;
        public IAcceptanceController AcceptanceController => _acceptanceController;
        public IIngredientsController IngredientsController => _ingredientController;

        public CompositeController(IUserController userController, IMedicineController medicineController, IAcceptanceController acceptanceController,
                                   IIngredientsController ingredientsController, ISchedulingController schedulingController)
        {
            _userController = userController;
            _medicineController = medicineController;
            _acceptanceController = acceptanceController;
            _ingredientController = ingredientsController;
            _schedulingController = schedulingController;
        }
    }
}
