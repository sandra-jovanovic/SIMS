using Apoteka.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Repositories
{
    public class SchedulingRepository : ISchedulingRepository
    {
        private const string filePath = "./schedulings.txt";
        public List<ScheduledMedicineOrdering> GetAllSchedules()
        {
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath)) ;
            }

            var scheduledOrderings = new List<ScheduledMedicineOrdering>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var scheduledOrdering = ParseScheduledMedicineOrdering(line);
                    scheduledOrderings.Add(scheduledOrdering);
                }

            }

            return scheduledOrderings;
        }

        public void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering)
        {
            var allSchedules = GetAllSchedules();
            allSchedules.Add(scheduledMedicineOrdering);
            SaveScheduledOrderings(allSchedules);
        }

        private void SaveScheduledOrderings(List<ScheduledMedicineOrdering> scheduledMedicineOrderings)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write("");
            }

            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                scheduledMedicineOrderings.ForEach(scheduling =>
                {
                    sw.WriteLine($"{scheduling.MedicineId},{scheduling.Quantity},{scheduling.Date}");
                });
            }
        }

        private ScheduledMedicineOrdering ParseScheduledMedicineOrdering(String line)
        {
            var fields = line.Split(",");
            var medicineId = int.Parse(fields[0]);
            var quantity = int.Parse(fields[1]);
            var date = DateTime.Parse(fields[2]);

            return new ScheduledMedicineOrdering(date, medicineId, quantity);
        }

        public void RemoveAllSchedulesForTodayOrPreviousPeriod()
        {
            var schedules = GetAllSchedules().FindAll(schedule => schedule.Date > DateTime.Now);
            SaveScheduledOrderings(schedules);
        }
    }
}
