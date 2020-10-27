namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdata : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeProjectDetails",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.EmployeeID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        PasswordHash = c.String(),
                        Designation = c.String(),
                        Address = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectID = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(),
                        ProjectManager = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeProjectDetails", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.EmployeeProjectDetails", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeProjectDetails", new[] { "ProjectID" });
            DropIndex("dbo.EmployeeProjectDetails", new[] { "EmployeeID" });
            DropTable("dbo.Projects");
            DropTable("dbo.Employees");
            DropTable("dbo.EmployeeProjectDetails");
        }
    }
}
