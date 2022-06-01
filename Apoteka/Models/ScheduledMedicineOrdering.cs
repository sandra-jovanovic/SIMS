using System;

namespace Apoteka.Models
{
    public class ScheduledMedicineOrdering
    {
        public DateTime Date { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }

        public ScheduledMedicineOrdering()
        {
        }

        public ScheduledMedicineOrdering(DateTime date, int medicineId, int quantity)
        {
            Date = date;
            MedicineId = medicineId;
            Quantity = quantity;
        }
    }
}
