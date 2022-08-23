using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Repositories
{
    public interface ISchedulingRepository
    {
        List<ScheduledMedicineOrdering> GetAllSchedules();
        void AddNewScheduledOrder(ScheduledMedicineOrdering scheduledMedicineOrdering);
        void RemoveScheduledOrder(ScheduledMedicineOrdering scheduledMedicineOrdering);
    }
}
