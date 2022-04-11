using lab3.model;

namespace lab3.db;

public interface IDiagnosisDao
{
    Diagnosis Create(Diagnosis entity);
    bool Update(Diagnosis entity);
    bool Delete(Diagnosis entity);
    Diagnosis? RetrieveById(int id);
    IEnumerable<Diagnosis> RetrieveAll();
}