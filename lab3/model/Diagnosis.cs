namespace lab3.model
{
    public partial class Diagnosis
    {
        public Diagnosis()
        {
        }

        public int IdDiagnosis { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Hospitalization> Hospitalizations { get; set; } = new HashSet<Hospitalization>();
    }
}
