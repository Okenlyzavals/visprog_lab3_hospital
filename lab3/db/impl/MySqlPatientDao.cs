using lab3.model;
using Microsoft.EntityFrameworkCore;

namespace lab3.db.impl;

public class MySqlPatientDao : IPatientDao
{
    public Patient Create(Patient entity)
    {
        using var context = new HospitalContext();
        context.Patients.Add(entity);
        context.SaveChanges();
        return entity;
    }

    public bool Update(Patient entity)
    {
        using var context = new HospitalContext();
        context.Patients.Update(entity);
        return context.SaveChanges() > 0;
    }

    public bool Delete(Patient entity)
    {
        using var context = new HospitalContext();
        context.Patients.Remove(entity);
        return context.SaveChanges() > 0;
    }

    public Patient? RetrieveById(int id)
    {
        using var context = new HospitalContext();

        var result = (from patient in context.Patients
                .Include(p => p.Hospitalizations)
                .ThenInclude(h =>h.FkDiagnosisNavigation)
            where patient.IdPatients == id
            select patient).Take(1).ToList();

        return result.Any() ? result[0] : null;
    }

    public IEnumerable<Patient> RetrieveAll()
    {
        using var context = new HospitalContext();

        var result = (from patient in context.Patients
                .Include(p => p.Hospitalizations)
                .ThenInclude(h =>h.FkDiagnosisNavigation)
            select patient).ToList();

        return result;
    }
}