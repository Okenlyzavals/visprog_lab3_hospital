using lab3.db.factory;
using lab3.model;
using Microsoft.AspNetCore.Mvc;

namespace lab3.Controllers;

[ApiController]
[Route("[controller]/patients")]
public class HospitalController : ControllerBase
{
    private readonly IDaoFactory _factory = MySqlDaoFactory.Instance;
    
    [HttpDelete]
    [Route("delete/{id:int}")]
    public bool DeletePatients(int id)
    {
        var toDelete = _factory.GetPatientsDao().RetrieveById(id);
        return toDelete != null && _factory.GetPatientsDao().Delete(toDelete);
    }
    
    [HttpGet]
    [Route("get")]
    public IEnumerable<Patient> GetPatients()
    {
        return _factory.GetPatientsDao().RetrieveAll();
    }
    
    [HttpGet]
    [Route("get/{id:int}")]
    public Patient? GetPatientById(int id)
    {
        return _factory.GetPatientsDao().RetrieveById(id);
    }

    [HttpPut]
    [Route("update/{id:int}")]
    public bool UpdatePatient(int id, [FromBody] PatientParameterDto patientDto)
    {
        var toUpdate = _factory.GetPatientsDao().RetrieveById(id);
        if (toUpdate == null)
        {
            return false;
        }

        toUpdate.Age = patientDto.Age;
        toUpdate.Name = patientDto.Name;
        toUpdate.Surname = patientDto.Surname;
        toUpdate.Patronymic = patientDto.Patronymic;

        var lastHosp = (from hosp in toUpdate.Hospitalizations
            where hosp.DischargeDate > DateOnly.FromDateTime(DateTime.Now)
            select hosp).Take(1).ToList();

        var newDiag = _factory.GetDiagnosisDao().Create(new Diagnosis()
        {
            Name = patientDto.DiagnosisName
        });

        if (lastHosp.Count > 0)
        {
            lastHosp[0].AdmissionDate = DateOnly.FromDateTime(patientDto.AdmissionDate);
            lastHosp[0].DischargeDate = DateOnly.FromDateTime(patientDto.DischargeDate);
            lastHosp[0].FkDiagnosis = newDiag.IdDiagnosis;
        }
        else
        {
            var newHosp = new Hospitalization()
            {
                AdmissionDate = DateOnly.FromDateTime(patientDto.AdmissionDate),
                DischargeDate = DateOnly.FromDateTime(patientDto.DischargeDate),
                FkDiagnosis = newDiag.IdDiagnosis,
                FkPatient = toUpdate.IdPatients
            };
            toUpdate.Hospitalizations.Add(newHosp);
        }
        
        return _factory.GetPatientsDao().Update(toUpdate);
}
    

    [HttpPost]
    [Route("create")]
    public Patient CreatePatient([FromBody]PatientParameterDto patientDto)
    {
        var toCreate = new Patient()
        {
            Name = patientDto.Name,
            Surname = patientDto.Surname,
            Patronymic = patientDto.Patronymic,
            Age = patientDto.Age
        };

        var diagnosis = _factory
            .GetDiagnosisDao()
            .Create(new Diagnosis() {Name = patientDto.DiagnosisName});

        var hospitalization = new Hospitalization()
        {
            DischargeDate = DateOnly.FromDateTime(patientDto.DischargeDate),
            AdmissionDate = DateOnly.FromDateTime(patientDto.AdmissionDate),
            FkDiagnosis = diagnosis.IdDiagnosis,
            FkPatient = toCreate.IdPatients
        };
        toCreate.Hospitalizations.Add(hospitalization);
        _factory.GetPatientsDao().Create(toCreate);
        
        return toCreate;
    }
}