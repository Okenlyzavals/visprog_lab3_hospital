namespace lab3.Controllers;

public class PatientParameterDto
{
    public string Surname { get; set; }
    public string Name { get; set; }
    public string? Patronymic { get; set; }
    public byte Age { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime DischargeDate { get; set; }
    public string DiagnosisName { get; set; }

    public PatientParameterDto(string surname, string name, string? patronymic, byte age, DateTime admissionDate, DateTime dischargeDate, string diagnosisName)
    {
        Surname = surname;
        Name = name;
        Patronymic = patronymic;
        Age = age;
        AdmissionDate = admissionDate;
        DischargeDate = dischargeDate;
        DiagnosisName = diagnosisName;
    }

}