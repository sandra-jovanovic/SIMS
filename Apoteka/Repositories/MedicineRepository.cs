using Apoteka.Exceptions;
using Apoteka.Models;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Apoteka.Repositories
{
    public class MedicineRepository : IMedicineRepository
    {
        private const string FILE_PATH = "medicines-storage.bin";

        public List<Medicine> GetAllMedicines()
        {

            if (!File.Exists(FILE_PATH))
            {
                using (File.Create(FILE_PATH)) ;
            }

            IFormatter formatter = new BinaryFormatter();
            using (var stream = new FileStream(FILE_PATH, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                List<Medicine> medicines;
                try
                {
                    medicines = (List<Medicine>)formatter.Deserialize(stream);
                } catch
                {
                    medicines = new List<Medicine>();
                }

                return medicines;
            }
            
        }

        private void SaveMedicines(List<Medicine> medicines)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(FILE_PATH, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, medicines);
            stream.Close();
        }

        public void AddNewMedicine(Medicine medicine)
        {
            var allMedicines = GetAllMedicines();

            foreach (var iterMedicine in allMedicines)
            {
                if (medicine.Id == iterMedicine.Id)
                    throw new ExistingIdException();
            }

            List<Medicine> medicines = allMedicines;
            medicines.Add(medicine);

            SaveMedicines(medicines);
        }

        public void UpdateMedicine(Medicine medicine)
        {
            var allMedicines = GetAllMedicines();

            foreach (var iterMedicine in allMedicines)
            {
                if (medicine.Id == iterMedicine.Id)
                {
                    iterMedicine.Price = medicine.Price;
                    iterMedicine.Accepted = medicine.Accepted;
                    iterMedicine.Quantity = medicine.Quantity;
                    iterMedicine.RefusedBy = medicine.RefusedBy;
                    iterMedicine.Refused = medicine.Refused;
                    iterMedicine.Manufacturer = medicine.Manufacturer;
                    iterMedicine.ReasonForRefusing = medicine.ReasonForRefusing;
                    iterMedicine.Ingredients = medicine.Ingredients;
                    break;
                }
            }

            SaveMedicines(allMedicines);
        }


        //public void UnmarkMedicineAsRefused(int medicineId)
        //{
        //    List<Medicine> allMedicines = GetAllMedicines();
        //    allMedicines.ForEach(iter =>
        //    {
        //        if (iter.Id == medicineId)
        //        {
        //            iter.Refused = false;
        //            iter.RefusedBy = "";
        //            iter.ReasonForRefusing = "";
        //        }
        //    });

        //    SaveMedicines(allMedicines);
        //}

    }
}
