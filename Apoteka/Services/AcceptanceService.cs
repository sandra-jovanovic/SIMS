using Apoteka.Models;
using Apoteka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Services
{
    public class AcceptanceService : IAcceptanceService
    {
        private IAcceptanceRepository acceptanceRepository;

        public AcceptanceService(IAcceptanceRepository acceptanceRepository)
        {
            this.acceptanceRepository = acceptanceRepository;
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
