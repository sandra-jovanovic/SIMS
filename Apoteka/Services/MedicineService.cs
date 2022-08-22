using Apoteka.Constants;
using Apoteka.Models;
using Apoteka.Repositories;
using Apoteka.Util;
using System.Collections.Generic;

namespace Apoteka.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository medicineRepository;
        private readonly IAcceptanceService acceptanceService;

        public MedicineService(IMedicineRepository medicineRepository, IAcceptanceService acceptanceService)
        {
            this.medicineRepository = medicineRepository;
            this.acceptanceService = acceptanceService;
        }

        public void AddMedicine(Medicine medicine)
        {
            medicineRepository.AddMedicine(medicine);
        }

        public List<Medicine> GetAllAcceptedMedicines()
        {
            return medicineRepository.GetAllAcceptedMedicines();
        }

        public List<Medicine> GetNotAcceptedMedicines(User user)
        {
            List<Medicine> medicines = medicineRepository.GetNotAcceptedMedicines();
            medicines.ForEach(medicine =>
            {
                var acceptance = acceptanceService.GetAcceptanceByUserIdAndMedicineId(user.JMBG, medicine.Id);
                medicine.Accepted = acceptance != null;
            });

            return medicines;
        }

        public List<Medicine> GetRefusedMedicines()
        {
            return medicineRepository.GetRefusedMedicines();
        }

        public List<Medicine> SearchRefusedMedicines(string searchBy, string searchText)
        {
            switch (searchBy)
            {
                case MedicineSeachingFilters.ID:
                    return medicineRepository.GetRefusedMedicines().FindAll(medicine => medicine.Id.ToString().Contains(searchText));
                case MedicineSeachingFilters.NAME:
                    return medicineRepository.GetRefusedMedicines().FindAll(medicine => medicine.Name.ToLower().Contains(searchText));
                case MedicineSeachingFilters.MANUFACTURER:
                    return medicineRepository.GetRefusedMedicines().FindAll(medicine => medicine.Manufacturer.ToLower().Contains(searchText));
                case MedicineSeachingFilters.PRICE_RANGE:
                    var splittedString = searchText.Split(',');
                    if (splittedString.Length != 2)
                    {
                        return medicineRepository.GetRefusedMedicines();
                    }

                    int minVal;
                    int maxVal;
                    try
                    {
                        minVal = int.Parse(splittedString[0]);
                        maxVal = int.Parse(splittedString[1]);
                    }
                    catch
                    {
                        return medicineRepository.GetRefusedMedicines();
                    }

                    return medicineRepository.GetRefusedMedicines().FindAll(medicine => minVal <= medicine.Price && medicine.Price <= maxVal);
                case MedicineSeachingFilters.QUANTITY:
                    return medicineRepository.GetRefusedMedicines().FindAll(medicine => medicine.Quantity.ToString().Equals(searchText));
                case MedicineSeachingFilters.INGREDIENTS:
                    return (List<Medicine>)SearchingHelper.GetMedicinesUsingIngredientsFilter(medicineRepository.GetRefusedMedicines(), searchText);
                default:
                    return medicineRepository.GetRefusedMedicines();
            }
        }

        public List<Medicine> SearchAcceptedMedicines(string searchBy, string searchText)
        {
            switch (searchBy)
            {
                case MedicineSeachingFilters.ID:
                    return medicineRepository.GetAllAcceptedMedicines().FindAll(medicine => medicine.Id.ToString().Contains(searchText));
                case MedicineSeachingFilters.NAME:
                    return medicineRepository.GetAllAcceptedMedicines().FindAll(medicine => medicine.Name.ToLower().Contains(searchText));
                case MedicineSeachingFilters.MANUFACTURER:
                    return medicineRepository.GetAllAcceptedMedicines().FindAll(medicine => medicine.Manufacturer.ToLower().Contains(searchText));
                case MedicineSeachingFilters.PRICE_RANGE:
                    var splittedString = searchText.Split(',');
                    if (splittedString.Length != 2)
                    {
                        return medicineRepository.GetAllAcceptedMedicines();
                    }

                    int minVal;
                    int maxVal;
                    try
                    {
                        minVal = int.Parse(splittedString[0]);
                        maxVal = int.Parse(splittedString[1]);
                    }
                    catch
                    {
                        return medicineRepository.GetAllAcceptedMedicines();
                    }

                    return medicineRepository.GetAllAcceptedMedicines().FindAll(medicine => minVal <= medicine.Price && medicine.Price <= maxVal);
                case MedicineSeachingFilters.QUANTITY:
                    return medicineRepository.GetAllAcceptedMedicines().FindAll(medicine => medicine.Quantity.ToString().Equals(searchText));
                case MedicineSeachingFilters.INGREDIENTS:
                    return (List<Medicine>)SearchingHelper.GetMedicinesUsingIngredientsFilter(medicineRepository.GetAllAcceptedMedicines(), searchText);
                default:
                    return medicineRepository.GetAllAcceptedMedicines();
            }
        }

        public void IncreaseMedicineQuantity(int medicineId, int quantity)
        {
            medicineRepository.IncreaseMedicineQuantity(medicineId, quantity);
        }

        public bool AcceptMedicine(Medicine medicine, User user)
        {
            var medicineApproved = acceptanceService.AcceptMedicineByUser(user.JMBG, medicine.Id, user.Role == UserRole.Lekar);
            if (medicineApproved)
            {
                medicineRepository.MarkMedicineAsApproved(medicine.Id);
            }

            return medicineApproved;
        }

        public void RefuseMedicine(int medicineId, string reason, string refusedBy)
        {
            medicineRepository.SetMedicineRefused(medicineId, reason, refusedBy);
            acceptanceService.DeleteAllAcceptancesForMedicine(medicineId);
        }

        public void UnmarkMedicineAsRefused(int medicineId, User user)
        {
            medicineRepository.UnmarkMedicineAsRefused(medicineId);
            acceptanceService.AcceptMedicineByUser(user.JMBG, medicineId, user.Role == UserRole.Lekar);
        }

        public void RevokeMedicineAcceptanceByUser(User user, Medicine medicine)
        {
            acceptanceService.RevokeMedicineAcceptanceByUser(user.JMBG, medicine.Id);
        }
    }
}
