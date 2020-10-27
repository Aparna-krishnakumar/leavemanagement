using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LeaveManagementSystem.DomainModels.Migrations;

namespace LeaveManagementSystem.DomainModels
{
    public class LeaveManagementSystemDatabaseDbContext:DbContext
    {
        public LeaveManagementSystemDatabaseDbContext() : base("LeaveManagementSystemDatabaseDbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LeaveManagementSystemDatabaseDbContext, Configuration>());
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProjectDetail> EmployeeProjectDetails { get; set; }
    }
}
