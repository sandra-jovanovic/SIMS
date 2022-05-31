namespace Apoteka.Services
{
    public class CompositeService
    {
        private readonly IUserService _userService;
        private readonly IMedicineService _medicineService;
        private readonly IAcceptanceService _acceptanceService;
        private readonly IIngredientsService _ingredientService;

        public IUserService UserService => _userService;
        public IMedicineService MedicineService => _medicineService;
        public IAcceptanceService AcceptanceService => _acceptanceService;
        public IIngredientsService IngredientsService => _ingredientService;

        public CompositeService(IUserService userService, IMedicineService medicineService, IAcceptanceService acceptanceService,
                                IIngredientsService ingredientsService)
        {
            _userService = userService;
            _medicineService = medicineService;
            _acceptanceService = acceptanceService;
            _ingredientService = ingredientsService;
        }
    }
}
