using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public interface IMedicineService
    {
        List<Medicine> GetAllAcceptedMedicines();
        List<Medicine> GetNotAcceptedMedicines(User user);
        List<Medicine> GetRefusedMedicines();
        List<Medicine> SearchRefusedMedicines(string searchBy, string searchText);
        List<Medicine> SearchAcceptedMedicines(string searchBy, string searchText);
        void AddMedicine(Medicine medicine);
        void IncreaseMedicineQuantity(int medicineId, int quantity);
        void RefuseMedicine(int medicineId, string reason, string refusedBy);
        void UnmarkMedicineAsRefused(int medicineId, User user);
        bool AcceptMedicine(Medicine medicine, User user);
        void RevokeMedicineAcceptanceByUser(User user, Medicine medicine);
    }
}
