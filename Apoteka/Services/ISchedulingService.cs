using Apoteka.Models;

namespace Apoteka.Services
{
    public interface ISchedulingService
    {
        int OrderAllMedicinesScheduledForTodayOrForPreviousPeriod();
        void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering);
    }
}
