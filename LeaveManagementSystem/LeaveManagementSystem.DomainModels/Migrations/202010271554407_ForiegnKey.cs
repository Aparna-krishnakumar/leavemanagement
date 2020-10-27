
    namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForiegnKey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Projects", "ProjectManager");
            CreateIndex("dbo.LeaveDatas", "EmployeeID");
            AddForeignKey("dbo.Projects", "ProjectManager", "dbo.Employees", "EmployeeID", cascadeDelete: true);
            AddForeignKey("dbo.LeaveDatas", "EmployeeID", "dbo.Employees", "EmployeeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveDatas", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Projects", "ProjectManager", "dbo.Employees");
            DropIndex("dbo.LeaveDatas", new[] { "EmployeeID" });
            DropIndex("dbo.Projects", new[] { "ProjectManager" });
        }
    }
}
