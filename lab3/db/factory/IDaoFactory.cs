namespace lab3.db.factory;

public interface IDaoFactory
{
    IDiagnosisDao GetDiagnosisDao();
    IPatientDao GetPatientsDao();
    IHospitalizationDao GetHospitalizationDao();
}