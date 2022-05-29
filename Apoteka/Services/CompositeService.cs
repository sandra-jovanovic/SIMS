namespace Apoteka.Services
{
    public class CompositeService
    {
        private readonly IUserService _userService;
        private readonly IMedicineService _medicineService;
        private readonly IAcceptanceService _acceptanceService;

        public IUserService UserService => _userService;
        public IMedicineService MedicineService => _medicineService;
        public IAcceptanceService AcceptanceService => _acceptanceService;

        public CompositeService(IUserService userService, IMedicineService medicineService, IAcceptanceService acceptanceService)
        {
            _userService = userService;
            _medicineService = medicineService;
            _acceptanceService = acceptanceService;
        }
    }
}
