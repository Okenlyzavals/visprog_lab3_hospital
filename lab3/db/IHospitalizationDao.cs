using lab3.model;

namespace lab3.db;

public interface IHospitalizationDao
{
    Hospitalization Create(Hospitalization entity);
    bool Update(Hospitalization entity);
    bool Delete(Hospitalization entity);
    Hospitalization? RetrieveById(int id);

    IEnumerable<Hospitalization> RetrieveByPatient(int patientId);
    IEnumerable<Hospitalization> RetrieveAll();
}