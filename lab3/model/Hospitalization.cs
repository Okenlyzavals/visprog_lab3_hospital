namespace lab3.model
{
    public partial class Hospitalization
    {
        public int IdHospitalizations { get; set; }
        public DateOnly AdmissionDate { get; set; }
        public DateOnly DischargeDate { get; set; }
        public int FkPatient { get; set; }
        public int FkDiagnosis { get; set; }

        public virtual Diagnosis FkDiagnosisNavigation { get; set; } = null!;
        public virtual Patient FkPatientNavigation { get; set; } = null!;
    }
}
