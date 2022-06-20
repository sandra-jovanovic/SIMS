using Apoteka.Models;
using Apoteka.Services;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public class AcceptanceController : IAcceptanceController
    {
        private IAcceptanceService acceptanceService;

        public AcceptanceController(IAcceptanceService acceptanceService)
        {
            this.acceptanceService = acceptanceService;
        }

        public bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor)
        {
            return acceptanceService.AcceptMedicineByUser(JMBG, medicineId, isDoctor);
        }

        public void DeleteAllAcceptancesForMedicine(int medicineId)
        {
            acceptanceService.DeleteAllAcceptancesForMedicine(medicineId);
        }

        public List<Acceptance> GetAcceptancesByUser(string JMBG)
        {
            return acceptanceService.GetAcceptancesByUser(JMBG);
        }

        public List<Acceptance> GetAllAcceptances()
        {
            return acceptanceService.GetAllAcceptances();
        }

        public void RevokeMedicineAcceptanceByUser(string jMBG, int id)
        {
            acceptanceService.RevokeMedicineAcceptanceByUser(jMBG, id);
        }
    }
}
