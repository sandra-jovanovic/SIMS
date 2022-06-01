using Apoteka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Services
{
    public class CompositeService
    {
        private readonly IUserService _userService;
        private readonly IMedicineService _medicineService;
        private readonly ISchedulingService _schedulingService;
        private readonly IAcceptanceService _acceptanceService;
        private readonly IIngredientsService _ingredientService;

        public IUserService UserService => _userService;
        public IMedicineService MedicineService => _medicineService;
        public ISchedulingService SchedulingService => _schedulingService;
        public IAcceptanceService AcceptanceService => _acceptanceService;
        public IIngredientsService IngredientsService => _ingredientService;

        public CompositeService(IUserService userService, IMedicineService medicineService, IAcceptanceService acceptanceService,
                                IIngredientsService ingredientsService, ISchedulingService schedulingService)
        {
            _userService = userService;
            _medicineService = medicineService;
            _acceptanceService = acceptanceService;
            _ingredientService = ingredientsService;
            _schedulingService = schedulingService;
        }
    }
}
