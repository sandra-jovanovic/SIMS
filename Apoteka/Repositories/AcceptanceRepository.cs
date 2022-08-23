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

        public void CreateAcceptance(Acceptance acceptance)
        {
            var acceptances = GetAllAcceptances();
            acceptances.Add(acceptance);
            SaveAcceptances(acceptances);
        }

        public void DeleteAcceptance(Acceptance acceptance)
        {
            var acceptances = GetAllAcceptances();
            acceptances = acceptances.FindAll(a => !a.JMBG.Equals(acceptance.JMBG) && a.MedicineId != acceptance.MedicineId);
            SaveAcceptances(acceptances);
        }

        private Acceptance parseAcceptanceLine(string line)
        {
            var fields = line.Split(",");
            var JMBG = fields[0];
            var medicineId = int.Parse(fields[1]);
            var byDoctor = bool.Parse(fields[2]);

            return new Acceptance(JMBG, medicineId, byDoctor);
        }

        private void SaveAcceptances(List<Acceptance> acceptances) 
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                foreach (var acceptance in acceptances)
                {
                    var newLine = $"{acceptance.JMBG},{acceptance.MedicineId},{acceptance.ByDoctor}";
                    sw.WriteLine(newLine);
                }
            }
        }
    }
}
