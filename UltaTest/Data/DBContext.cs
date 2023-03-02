using System.Data.Entity;
using UltaTest.Data.Domains;

namespace UltaTest.Data
{
    public class DBContext : DbContext
    {
        public DBContext() : base("UltaTestDB") { }
        public DbSet<Patient> Patients { get; set; }
    }
}