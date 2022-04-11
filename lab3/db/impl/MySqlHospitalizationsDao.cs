using lab3.model;

namespace lab3.db.impl;

public class MySqlHospitalizationsDao : IHospitalizationDao
{
    public Hospitalization Create(Hospitalization entity)
    {
        using var context = new HospitalContext();

        var existing = (from hosp in context.Hospitalizations
            where hosp.DischargeDate > DateOnly.FromDateTime(DateTime.Now)
            && hosp.FkPatient == entity.FkPatient
            select hosp).Take(1).ToList();

        if (existing.Count > 0)
        {
            entity = existing[0];
        }
        else
        {
            context.Hospitalizations.Add(entity); 
        }

        context.SaveChanges();
        return entity;
    }

    public bool Update(Hospitalization entity)
    {
        using var context = new HospitalContext();
        context.Update(entity);
        return context.SaveChanges() > 0;
    }

    public bool Delete(Hospitalization entity)
    {
        using var context = new HospitalContext();
        context.Remove(entity);
        return context.SaveChanges() > 0;
    }

    public Hospitalization? RetrieveById(int id)
    {
        using var context = new HospitalContext();

        var result = (from hosp in context.Hospitalizations
            where hosp.IdHospitalizations == id
            select hosp).Take(1).ToList();

        return result.Any() ? result[0] : null;
    }

    public IEnumerable<Hospitalization> RetrieveByPatient(int patientId)
    {
        using var context = new HospitalContext();

        var result = (from hosp in context.Hospitalizations
            where hosp.FkPatient == patientId
            select hosp).ToList();

        return result;
    }

    public IEnumerable<Hospitalization> RetrieveAll()
    {
        using var context = new HospitalContext();

        var result = (from hosp in context.Hospitalizations
            select hosp).ToList();

        return result;
    }
}