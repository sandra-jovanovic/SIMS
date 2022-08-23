using Apoteka.Models;
using Apoteka.Repositories;
using System;

namespace Apoteka.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingRepository schedulingRepository;
        private readonly IMedicineService medicineService;

        public SchedulingService(ISchedulingRepository schedulingRepository, IMedicineService medicineService)
        {
            this.schedulingRepository = schedulingRepository;
            this.medicineService = medicineService;
        }

        public int OrderAllMedicinesScheduledForTodayOrForPreviousPeriod()
        {
            var schedules = schedulingRepository.GetAllSchedules().FindAll(schedule => schedule.Date <= DateTime.Now);

            schedules.ForEach(schedule =>
            {
                medicineService.IncreaseMedicineQuantity(schedule.MedicineId, schedule.Quantity);
                schedulingRepository.RemoveScheduledOrder(schedule);
            });

            return schedules.Count;
        }

        public void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering)
        {
            schedulingRepository.AddNewScheduledOrder(scheduledMedicineOrdering);
        }
    }
}
