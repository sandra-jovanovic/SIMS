using Apoteka.Models;
using Apoteka.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Services
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingRepository schedulingRepository;
        private readonly IMedicineRepository medicineRepository;

        public SchedulingService(ISchedulingRepository schedulingRepository, IMedicineRepository medicineRepository)
        {
            this.schedulingRepository = schedulingRepository;
            this.medicineRepository = medicineRepository;
        }

        public int OrderAllMedicinesScheduledForTodayOrForPreviousPeriod()
        {
            var schedules = schedulingRepository.GetAllSchedules().FindAll(schedule => schedule.Date <= DateTime.Now);

            schedules.ForEach(schedule =>
            {
                medicineRepository.IncreaseMedicineQuantity(schedule.MedicineId, schedule.Quantity);
            });

            schedulingRepository.RemoveAllSchedulesForTodayOrPreviousPeriod();

            return schedules.Count;
        }

        public void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering)
        {
            schedulingRepository.ScheduleOrderingForDate(scheduledMedicineOrdering);
        }
    }
}
