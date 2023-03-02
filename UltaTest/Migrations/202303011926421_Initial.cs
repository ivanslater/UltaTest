namespace UltaTest.Migrations
{
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Address = c.String(),
                    Email = c.String(),
                    DateOfBirth = c.DateTime(nullable: false),
                    Gender = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
           
            var sqlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\\Migrations\\202303011926421_Initial.sql");
            Sql(File.ReadAllText(sqlFile));
        }

        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}
