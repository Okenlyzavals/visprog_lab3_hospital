namespace lab3.model
{
    public partial class Patient
    {
        public Patient()
        {
            
        }

        public int IdPatients { get; set; }
        public string Surname { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Patronymic { get; set; }
        public byte Age { get; set; }

        public virtual ICollection<Hospitalization> Hospitalizations { get; set; } = new HashSet<Hospitalization>();

        public Hospitalization? GetLastHospitalization()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            var result = (from hosp in Hospitalizations
                where hosp.AdmissionDate <= today && hosp.DischargeDate >= today
                select hosp).TakeLast(1).ToList();
            return result.Any() ? result[0] : null;
        }
    }
}
