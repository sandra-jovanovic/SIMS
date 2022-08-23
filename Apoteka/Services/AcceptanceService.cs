using Apoteka.Models;
using Apoteka.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Apoteka.Services
{
    public class AcceptanceService : IAcceptanceService
    {
        private IAcceptanceRepository acceptanceRepository;

        public AcceptanceService(IAcceptanceRepository acceptanceRepository)
        {
            this.acceptanceRepository = acceptanceRepository;
        }

        public List<Acceptance> GetAllAcceptances()
        {
            return acceptanceRepository.GetAllAcceptances();
        }

        public Acceptance? GetAcceptanceByUserIdAndMedicineId(string JMBG, int medicineId)
        {
            var acceptances = acceptanceRepository.GetAllAcceptances();
            return acceptances.Find(a => a.MedicineId == medicineId && a.JMBG.Equals(JMBG));
        }


        public bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor)
        {
            Acceptance newAcceptance = new Acceptance(JMBG, medicineId, isDoctor);
            acceptanceRepository.CreateAcceptance(newAcceptance);

            var allAcceptances = acceptanceRepository.GetAllAcceptances().FindAll(acc => acc.MedicineId == medicineId);

            int numberOfPharmacists = allAcceptances.Count(acc => !acc.ByDoctor);
            int numberOfDoctors = allAcceptances.Count(acc => acc.ByDoctor);

            if (isDoctor)
            {
                numberOfDoctors++;
            }
            else
            {
                numberOfPharmacists++;
            }

            var shoudBeAccepted = numberOfDoctors >= 1 && numberOfPharmacists >= 2;
            return shoudBeAccepted;
        }

        public void DeleteAllAcceptancesForMedicine(int medicineId)
        {
            var allAcceptances = acceptanceRepository.GetAllAcceptances();
            foreach (Acceptance a in allAcceptances)
            {
                if (a.MedicineId.Equals(medicineId))
                {
                    acceptanceRepository.DeleteAcceptance(a);
                }
            }
            
        }

        public List<Acceptance> GetAcceptancesByUser(string JMBG)
        {
            return acceptanceRepository.GetAllAcceptances().FindAll(a => a.JMBG.Equals(JMBG));
        }

        public void RevokeMedicineAcceptanceByUser(string JMBG, int id)
        {
            var acceptance = acceptanceRepository.GetAllAcceptances().Find(a => a.JMBG.Equals(JMBG) && a.MedicineId == id);
            acceptanceRepository.DeleteAcceptance(acceptance);
        }
    }
}
