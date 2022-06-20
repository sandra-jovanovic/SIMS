using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apoteka.Controllers
{
    public class SchedulingController : ISchedulingController
    {
        private readonly ISchedulingService schedulingService;

        public SchedulingController(ISchedulingService schedulingService)
        {
            this.schedulingService = schedulingService;
        }

        public int OrderAllMedicinesScheduledForTodayOrForPreviousPeriod()
        {
            return schedulingService.OrderAllMedicinesScheduledForTodayOrForPreviousPeriod();
        }

        public void ScheduleOrderingForDate(ScheduledMedicineOrdering scheduledMedicineOrdering)
        {
            schedulingService.ScheduleOrderingForDate(scheduledMedicineOrdering);
        }
    }
}
