using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public interface IAcceptanceService
    {
        List<Acceptance> GetAllAcceptances();
        List<Acceptance> GetAcceptancesByUser(string JMBG);
        bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor);
        void RevokeMedicineAcceptanceByUser(string jMBG, int id);
        void DeleteAllAcceptancesForMedicine(int medicineId);
    }
}
