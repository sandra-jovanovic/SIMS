using Apoteka.Models;

namespace Apoteka.Controllers
{
    public interface ISchedulingController
    {
        int OrderAllMedicinesScheduledForTodayOrForPreviousPeriod();
        void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering);
    }
}
