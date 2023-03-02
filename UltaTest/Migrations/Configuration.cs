namespace UltaTest.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<UltaTest.Data.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(UltaTest.Data.DBContext context)
        {
            context.Patients.Add(new Data.Domains.Patient
            {
                Name = "Patient Test",
                Address = "1000 5th Avenue, New York, NY",
                DateOfBirth = new System.DateTime(1980, 01, 01),
                Email = "empirestate@usa.gov",
                Gender = Data.Domains.Gender.NotSpecified,
            });

            context.SaveChanges();
        }
    }
}
