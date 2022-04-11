using lab3.model;

namespace lab3.db;

public interface IPatientDao
{
    Patient Create(Patient entity);
    bool Update(Patient entity);
    bool Delete(Patient entity);
    Patient? RetrieveById(int id);
    IEnumerable<Patient> RetrieveAll();
}