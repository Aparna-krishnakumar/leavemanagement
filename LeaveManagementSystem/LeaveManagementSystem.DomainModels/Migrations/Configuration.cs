namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LeaveManagementSystem.DomainModels.LeaveManagementSystemDatabaseDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LeaveManagementSystem.DomainModels.LeaveManagementSystemDatabaseDbContext context)
        {
            context.Employees.AddOrUpdate(new DomainModels.Employee() { EmployeeID = 1, Name = "Admin", Email = "admin@gmail.com", PasswordHash = "240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9", Designation = "Admin", Address = "Abc Street", Mobile = "000000000" });
            context.Projects.AddOrUpdate(new DomainModels.Project() { ProjectID = 1, ProjectName = "MVC", ProjectManager = 1 });

        }
    }
}
