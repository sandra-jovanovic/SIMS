using Apoteka.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Apoteka.Repositories
{
    public class AcceptanceRepository : IAcceptanceRepository
    {
        private const string filePath = "./acceptances.txt";
        public List<Acceptance> GetAllAcceptances()
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) ;
            }

            var acceptances = new List<Acceptance>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var acceptance = parseAcceptanceLine(line);
                    acceptances.Add(acceptance);
                }

            }

            return acceptances;
        }

        private Acceptance parseAcceptanceLine(string line)
        {
            var fields = line.Split(",");
            var JMBG = fields[0];
            var medicineId = int.Parse(fields[1]);
            var byDoctor = bool.Parse(fields[2]);

            return new Acceptance(JMBG, medicineId, byDoctor);
        }

        public List<Acceptance> GetAcceptancesByUser(string JMBG)
        {
            return GetAllAcceptances().FindAll(acceptance => acceptance.JMBG == JMBG);
        }

        public bool AcceptMedicineByUser(string JMBG, int medicineId, bool isDoctor)
        {
            var allAcceptances = GetAllAcceptances().FindAll(acc => acc.MedicineId == medicineId);
            
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

            if (shoudBeAccepted)
            {
                var filteredAcceptances = GetAllAcceptances().FindAll(acc => acc.MedicineId != medicineId);

                using (StreamWriter sw = new StreamWriter(filePath, false))
                {
                    sw.Write("");
                }

                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    foreach (var filteredAcceptance in filteredAcceptances)
                    {
                        var newLine = $"{filteredAcceptance.JMBG},{filteredAcceptance.MedicineId},{filteredAcceptance.ByDoctor}";
                        sw.WriteLine(newLine);
                    }
                }
            } else
            {
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    var newLine = $"{JMBG},{medicineId},{isDoctor}";
                    sw.WriteLine(newLine);
                }
            }


            return shoudBeAccepted;
        }

        public void RevokeMedicineAcceptanceByUser(string JMBG, int medicineId)
        {
            var filteredAcceptances = GetAllAcceptances().Where(x => x.JMBG != JMBG || x.MedicineId != medicineId).ToList();

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                foreach (var filteredAcceptance in filteredAcceptances)
                {
                    var newLine = $"{filteredAcceptance.JMBG},{filteredAcceptance.MedicineId},{filteredAcceptance.ByDoctor}";
                    sw.WriteLine(newLine);
                }
            }
        }

        public void DeleteAllAcceptancesForMedicine(int medicineId)
        {
            List<Acceptance> filteredAcceptances = GetAllAcceptances().FindAll(acceptance => acceptance.MedicineId != medicineId);

            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                foreach (var filteredAcceptance in filteredAcceptances)
                {
                    var newLine = $"{filteredAcceptance.JMBG},{filteredAcceptance.MedicineId},{filteredAcceptance.ByDoctor}";
                    sw.WriteLine(newLine);
                }
            }
        }
    }
}
