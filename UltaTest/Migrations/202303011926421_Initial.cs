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

            var assembly = Assembly.GetExecutingAssembly();
            var type = GetType();
            var regex = new Regex($@"{Regex.Escape(type.Namespace)}\.\d{{14}}_{Regex.Escape(type.Name)}\.sql");

            var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => regex.IsMatch(x));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                var sqlResult = reader.ReadToEnd();
                Sql(sqlResult);
            }
        }

        public override void Down()
        {
            DropTable("dbo.Patients");
        }
    }
}
