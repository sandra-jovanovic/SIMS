using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Services;
using System;
using System.Collections.Generic;

namespace Apoteka.Controllers
{
    public class MedicineController
    {
        private IMedicineService medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            this.medicineService = medicineService;
        }

        public void AddMedicine(Medicine medicine)
        {
            medicineService.AddMedicine(medicine);
        }

        public List<Medicine> GetAllAcceptedMedicines()
        {
            return medicineService.GetAllAcceptedMedicines();
        }

        public List<Medicine> GetNotAcceptedMedicines(User user)
        {
            return medicineService.GetNotAcceptedMedicines(user);
        }

        public List<Medicine> GetRefusedMedicines()
        {
            return medicineService.GetRefusedMedicines();
        }

        public List<Medicine> SearchRefusedMedicines(string searchBy, string searchText)
        {
            return medicineService.SearchRefusedMedicines(searchBy, searchText);
        }

        public List<Medicine> SearchAcceptedMedicines(string searchBy, string searchText)
        {
            return medicineService.SearchAcceptedMedicines(searchBy, searchText);
        }

        public void IncreaseMedicineQuantity(int medicineId, int quantity)
        {
            medicineService.IncreaseMedicineQuantity(medicineId, quantity);
        }

        public bool AcceptMedicine(Medicine medicine, User user)
        {
            return medicineService.AcceptMedicine(medicine, user);
        }

        public void RevokeMedicineAcceptanceByUser(User user, Medicine medicine)
        {
            medicineService.RevokeMedicineAcceptanceByUser(user, medicine);
        }

        public void RefuseMedicine(int medicineId, string reason, string refusedBy)
        {
            medicineService.RefuseMedicine(medicineId, reason, refusedBy);
        }

        public void UnmarkMedicineAsRefused(int medicineId, User user)
        {
            medicineService.UnmarkMedicineAsRefused(medicineId, user);
        }
    }
}
