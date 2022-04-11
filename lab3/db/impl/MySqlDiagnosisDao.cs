using lab3.model;

namespace lab3.db.impl;

public class MySqlDiagnosisDao : IDiagnosisDao
{
    public Diagnosis Create(Diagnosis entity)
    {
        using var context = new HospitalContext();

        var diagnosisThatIsThere = (from diagnosis in context.Diagnoses 
            where diagnosis.Name == entity.Name 
            select diagnosis).Take(1).ToList();

        if (diagnosisThatIsThere.Count > 0)
        {
            entity = diagnosisThatIsThere[0];
        }
        else
        {
            context.Diagnoses.Add(entity);
        }

        context.SaveChanges();
        return entity;
    }

    public bool Update(Diagnosis entity)
    {
        using var context = new HospitalContext();
        context.Diagnoses.Update(entity);
        var result = context.SaveChanges()>0;
        return result;
    }

    public bool Delete(Diagnosis entity)
    {
        using var context = new HospitalContext();
        context.Diagnoses.Remove(entity);
        var result = context.SaveChanges()>0;
        return result;
    }

    public Diagnosis? RetrieveById(int id)
    {
        using var context = new HospitalContext();
        var result = context.Diagnoses.Find(id);
        return result;
    }

    public IEnumerable<Diagnosis> RetrieveAll()
    {
        using var context = new HospitalContext();
        var result = (from diag in context.Diagnoses 
            select diag).ToList();
        return result;
    }
}