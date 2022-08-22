using Apoteka.Models;
using Apoteka.Repositories;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public class AcceptanceService : IAcceptanceService
    {
        private IAcceptanceRepository acceptanceRepository;

        public AcceptanceService(IAcceptanceRepository acceptanceRepository)
        {
            this.acceptanceRepository = acceptanceRepository;
        }

        public Acceptance? GetAcceptanceByUserIdAndMedicineId(string JMBG, int medicineId)
        {
            return acceptanceRepository.GetAcceptancesByUser(JMBG).Find(a => a.MedicineId == medicineId);
        }


        public bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor)
        {
            return acceptanceRepository.AcceptMedicineByUser(JMBG, medicineId, isDoctor);
        }

        public void DeleteAllAcceptancesForMedicine(int medicineId)
        {
            acceptanceRepository.DeleteAllAcceptancesForMedicine(medicineId);
        }

        public List<Acceptance> GetAcceptancesByUser(string JMBG)
        {
            return acceptanceRepository.GetAcceptancesByUser(JMBG);
        }

        public List<Acceptance> GetAllAcceptances()
        {
            return acceptanceRepository.GetAllAcceptances();
        }

        public void RevokeMedicineAcceptanceByUser(string jMBG, int id)
        {
            acceptanceRepository.RevokeMedicineAcceptanceByUser(jMBG, id);
        }
    }
}
