namespace Apoteka.Models
{
    public class Acceptance
    {
        public Acceptance()
        {
        }

        public Acceptance(string jMBG, int medicineId, bool byDoctor)
        {
            JMBG = jMBG;
            MedicineId = medicineId;
            ByDoctor = byDoctor;
        }

        public string JMBG { get; set; }
        public int MedicineId { get; set; }
        public bool ByDoctor { get; set; }
    }
}
