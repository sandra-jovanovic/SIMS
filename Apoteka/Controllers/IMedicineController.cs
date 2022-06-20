using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public interface IMedicineController
    {
        List<Medicine> GetAllAcceptedMedicines();
        List<Medicine> GetNotAcceptedMedicines();
        List<Medicine> GetRefusedMedicines();
        void AddMedicine(Medicine medicine);
        void IncreaseMedicineQuantity(int medicineId, int quantity);
        void SetMedicineRefused(int medicineId, string reason, string refusedBy);
        void UnmarkMedicineAsRefused(int medicineId);
        void MarkMedicineAsApproved(int medicineId);
    }
}
