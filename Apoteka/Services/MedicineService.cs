using Apoteka.Models;
using Apoteka.Repositories;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public class MedicineService : IMedicineService
    {
        private IMedicineRepository medicineRepository;

        public MedicineService(IMedicineRepository medicineRepository)
        {
            this.medicineRepository = medicineRepository;
        }

        public void AddMedicine(Medicine medicine)
        {
            medicineRepository.AddMedicine(medicine);
        }

        public List<Medicine> GetAllAcceptedMedicines()
        {
            return medicineRepository.GetAllAcceptedMedicines();
        }

        public List<Medicine> GetNotAcceptedMedicines()
        {
            return medicineRepository.GetNotAcceptedMedicines();
        }

        public List<Medicine> GetRefusedMedicines()
        {
            return medicineRepository.GetRefusedMedicines();
        }

        public void IncreaseMedicineQuantity(int medicineId, int quantity)
        {
            medicineRepository.IncreaseMedicineQuantity(medicineId, quantity);
        }

        public void MarkMedicineAsApproved(int medicineId)
        {
            medicineRepository.MarkMedicineAsApproved(medicineId);
        }

        public void SetMedicineRefused(int medicineId, string reason, string refusedBy)
        {
            medicineRepository.SetMedicineRefused(medicineId, reason, refusedBy);
        }

        public void UnmarkMedicineAsRefused(int medicineId)
        {
            medicineRepository.UnmarkMedicineAsRefused(medicineId);
        }
    }
}
