using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Services;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public class MedicineController : IMedicineController
    {
        private IMedicineService medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public void AddMedicine(Medicine medicine)
        {
            medicineService.AddMedicine(medicine);
        }

        public List<Medicine> GetAllAcceptedMedicines()
        {
            return medicineService.GetAllAcceptedMedicines();
        }

        public List<Medicine> GetNotAcceptedMedicines()
        {
            return medicineService.GetNotAcceptedMedicines();
        }

        public List<Medicine> GetRefusedMedicines()
        {
            return medicineService.GetRefusedMedicines();
        }

        public void IncreaseMedicineQuantity(int medicineId, int quantity)
        {
            medicineService.IncreaseMedicineQuantity(medicineId, quantity);
        }

        public void MarkMedicineAsApproved(int medicineId)
        {
            medicineService.MarkMedicineAsApproved(medicineId);
        }

        public void SetMedicineRefused(int medicineId, string reason, string refusedBy)
        {
            medicineService.SetMedicineRefused(medicineId, reason, refusedBy);
        }

        public void UnmarkMedicineAsRefused(int medicineId)
        {
            medicineService.UnmarkMedicineAsRefused(medicineId);
        }
    }
}
