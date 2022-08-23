using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Repositories
{
    public interface IMedicineRepository
    {
        List<Medicine> GetAllMedicines();
        void AddNewMedicine(Medicine medicine);
        void UpdateMedicine(Medicine medicine);
    }
}