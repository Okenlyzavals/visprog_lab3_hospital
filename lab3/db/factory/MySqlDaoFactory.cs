using System.Runtime.CompilerServices;
using lab3.db.impl;

namespace lab3.db.factory;

public sealed class MySqlDaoFactory : IDaoFactory
{
    private static volatile MySqlDaoFactory? _instance;
    
    public static MySqlDaoFactory Instance
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        get => _instance = _instance ?? new MySqlDaoFactory();
    }

    private IDiagnosisDao _diagnosisDao = new MySqlDiagnosisDao();
    private IPatientDao _patientDao = new MySqlPatientDao();
    private IHospitalizationDao _hospitalizationDao = new MySqlHospitalizationsDao();
    
    private MySqlDaoFactory()
    {}

    public IDiagnosisDao GetDiagnosisDao()
    {
        return _diagnosisDao;
    }

    public IPatientDao GetPatientsDao()
    {
        return _patientDao;
    }

    public IHospitalizationDao GetHospitalizationDao()
    {
        return _hospitalizationDao;
    }
}