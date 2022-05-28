using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Repositories
{
    public interface IAcceptanceRepository
    {
        List<Acceptance> GetAllAcceptances();
        List<Acceptance> GetAcceptancesByUser(string JMBG);
        bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor);
        void RevokeMedicineAcceptanceByUser(string jMBG, int id);
        void DeleteAllAcceptancesForMedicine(int medicineId);
    }
}
