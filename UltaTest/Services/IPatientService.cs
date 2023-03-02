using System.Collections.Generic;
using UltaTest.Data.Domains;

namespace UltaTest.Services
{
    public interface IPatientService
    {
        IEnumerable<Patient> ListAll(string filter = "");
        Patient GetById(int id);
        int Add(Patient patient);
        int Update(Patient patient);
        int Delete(int id);
    }
}
