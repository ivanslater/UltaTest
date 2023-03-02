using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using UltaTest.Data;
using UltaTest.Data.Domains;

namespace UltaTest.Services
{
    public class PatientService: IPatientService
    {
        private readonly DBContext _dbcontext;

        public PatientService(DBContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IEnumerable<Patient> ListAll(string filter = "")
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "filter", Value = filter }
            };

            return _dbcontext.Database.SqlQuery<Patient>("exec sp_GetAllPatients @filter", parms.ToArray()).AsEnumerable();
        }

        public Patient GetById(int id)
        {
            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "id", Value = id }
            };

            return _dbcontext.Database.SqlQuery<Patient>("exec sp_GetAllPatients '', @id", parms.ToArray()).FirstOrDefault();
        }

        public int Add(Patient patient)
        {
            string sql = "exec sp_InsertPatient @Name, @Address, @Email, @DateOfBirth, @Gender";

            var parms = new List<SqlParameter>
            { 
                new SqlParameter { ParameterName = "Name", Value = patient.Name },
                new SqlParameter { ParameterName = "Address", Value = patient.Address },
                new SqlParameter { ParameterName = "Email", Value = patient.Email },
                new SqlParameter { ParameterName = "DateOfBirth", Value = patient.DateOfBirth },
                new SqlParameter { ParameterName = "Gender", Value = patient.Gender}
            };

            return _dbcontext.Database.ExecuteSqlCommand(sql, parms.ToArray());
        }

        public int Update(Patient patient)
        {
            string sql = "exec sp_UpdatePatient @Id, @Name, @Address, @Email, @DateOfBirth, @Gender";

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "Id", Value = patient.Id },
                new SqlParameter { ParameterName = "Name", Value = patient.Name },
                new SqlParameter { ParameterName = "Address", Value = patient.Address },
                new SqlParameter { ParameterName = "Email", Value = patient.Email },
                new SqlParameter { ParameterName = "DateOfBirth", Value = patient.DateOfBirth },
                new SqlParameter { ParameterName = "Gender", Value = patient.Gender}
            };

            return _dbcontext.Database.ExecuteSqlCommand(sql, parms.ToArray());
        }

        public int Delete(int id)
        {
            string sql = "exec sp_DeletePatient @Id";

            var parms = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "Id", Value = id }
            };

            return _dbcontext.Database.ExecuteSqlCommand(sql, parms.ToArray());
        }
    }
}
