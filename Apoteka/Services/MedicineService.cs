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
            medicineRepository.AddNewMedicine(medicine);
        }

        public List<Medicine> GetAllAcceptedMedicines()
        {
            return medicineRepository.GetAllMedicines().FindAll(medicine => medicine.Accepted && !medicine.Refused);
        }

        public List<Medicine> GetNotAcceptedMedicines(User user)
        {
            List<Medicine> medicines = medicineRepository.GetAllMedicines().FindAll(medicine => !medicine.Accepted && !medicine.Refused);
            medicines.ForEach(medicine =>
            {
                var acceptance = acceptanceService.GetAcceptanceByUserIdAndMedicineId(user.JMBG, medicine.Id);
                medicine.Accepted = acceptance != null;
            });

            return medicines;
        }

        public List<Medicine> GetRefusedMedicines()
        {
            return medicineRepository.GetAllMedicines().FindAll(medicine => medicine.Refused);
        }

        public List<Medicine> SearchRefusedMedicines(string searchBy, string searchText)
        {
            switch (searchBy)
            {
                case MedicineSeachingFilters.ID:
                    return GetRefusedMedicines().FindAll(medicine => medicine.Id.ToString().Contains(searchText));
                case MedicineSeachingFilters.NAME:
                    return GetRefusedMedicines().FindAll(medicine => medicine.Name.ToLower().Contains(searchText));
                case MedicineSeachingFilters.MANUFACTURER:
                    return GetRefusedMedicines().FindAll(medicine => medicine.Manufacturer.ToLower().Contains(searchText));
                case MedicineSeachingFilters.PRICE_RANGE:
                    var splittedString = searchText.Split(',');
                    if (splittedString.Length != 2)
                    {
                        return GetRefusedMedicines();
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
                        return GetRefusedMedicines();
                    }

                    return GetRefusedMedicines().FindAll(medicine => minVal <= medicine.Price && medicine.Price <= maxVal);
                case MedicineSeachingFilters.QUANTITY:
                    return GetRefusedMedicines().FindAll(medicine => medicine.Quantity.ToString().Equals(searchText));
                case MedicineSeachingFilters.INGREDIENTS:
                    return (List<Medicine>)SearchingHelper.GetMedicinesUsingIngredientsFilter(GetRefusedMedicines(), searchText);
                default:
                    return GetRefusedMedicines();
            }
        }

        public List<Medicine> SearchAcceptedMedicines(string searchBy, string searchText)
        {
            switch (searchBy)
            {
                case MedicineSeachingFilters.ID:
                    return GetAllAcceptedMedicines().FindAll(medicine => medicine.Id.ToString().Contains(searchText));
                case MedicineSeachingFilters.NAME:
                    return GetAllAcceptedMedicines().FindAll(medicine => medicine.Name.ToLower().Contains(searchText));
                case MedicineSeachingFilters.MANUFACTURER:
                    return GetAllAcceptedMedicines().FindAll(medicine => medicine.Manufacturer.ToLower().Contains(searchText));
                case MedicineSeachingFilters.PRICE_RANGE:
                    var splittedString = searchText.Split(',');
                    if (splittedString.Length != 2)
                    {
                        return GetAllAcceptedMedicines();
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
                        return GetAllAcceptedMedicines();
                    }

                    return GetAllAcceptedMedicines().FindAll(medicine => minVal <= medicine.Price && medicine.Price <= maxVal);
                case MedicineSeachingFilters.QUANTITY:
                    return GetAllAcceptedMedicines().FindAll(medicine => medicine.Quantity.ToString().Equals(searchText));
                case MedicineSeachingFilters.INGREDIENTS:
                    return (List<Medicine>)SearchingHelper.GetMedicinesUsingIngredientsFilter(GetAllAcceptedMedicines(), searchText);
                default:
                    return GetAllAcceptedMedicines();
            }
        }

        public void IncreaseMedicineQuantity(int medicineId, int quantity)
        {
            Medicine medicine = medicineRepository.GetAllMedicines().Find(m => m.Id == medicineId);
            medicine.Quantity += quantity;
            medicineRepository.UpdateMedicine(medicine);
        }

        public bool AcceptMedicine(Medicine medicine, User user)
        {
            var medicineApproved = acceptanceService.AcceptMedicineByUser(user.JMBG, medicine.Id, user.Role == UserRole.Lekar);
            if (medicineApproved)
            {
                var medicineToUpdate = medicineRepository.GetAllMedicines().Find(m => m.Id == medicine.Id);
                medicineToUpdate.Refused = false;
                medicineToUpdate.RefusedBy = string.Empty;
                medicineToUpdate.ReasonForRefusing = string.Empty;
                medicineToUpdate.Accepted = true;

                medicineRepository.UpdateMedicine(medicineToUpdate);
            }

            return medicineApproved;
        }

        public void RefuseMedicine(int medicineId, string reason, string refusedBy)
        {
            var medicineToUpdate = medicineRepository.GetAllMedicines().Find(m => m.Id == medicineId);
            medicineToUpdate.Refused = true;
            medicineToUpdate.RefusedBy = refusedBy;
            medicineToUpdate.ReasonForRefusing = reason;
            medicineToUpdate.Accepted = false;

            medicineRepository.UpdateMedicine(medicineToUpdate);

            acceptanceService.DeleteAllAcceptancesForMedicine(medicineId);
        }

        public void UnmarkMedicineAsRefused(int medicineId, User user)
        {
            var medicineToUpdate = medicineRepository.GetAllMedicines().Find(m => m.Id == medicineId);
            medicineToUpdate.Refused = false;
            medicineToUpdate.RefusedBy = string.Empty;
            medicineToUpdate.ReasonForRefusing = string.Empty;
            medicineRepository.UpdateMedicine(medicineToUpdate);

            acceptanceService.AcceptMedicineByUser(user.JMBG, medicineId, user.Role == UserRole.Lekar);
        }

        public void RevokeMedicineAcceptanceByUser(User user, Medicine medicine)
        {
            acceptanceService.RevokeMedicineAcceptanceByUser(user.JMBG, medicine.Id);
        }
    }
}
