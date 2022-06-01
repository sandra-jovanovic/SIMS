using Apoteka.Models;
using System.Collections.Generic;

namespace Apoteka.Repositories
{
    public interface ISchedulingRepository
    {
        void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering);
        List<ScheduledMedicineOrdering> GetAllSchedules();
        void RemoveAllSchedulesForTodayOrPreviousPeriod();
    }
}
