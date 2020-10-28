namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionforiegnkey : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HRtables",
                c => new
                    {
                        HRID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HRID)
                .ForeignKey("dbo.Employees", t => t.HRID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.HRID)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.ProjectManagers",
                c => new
                    {
                        ProjectManagerID = c.Int(nullable: false),
                        ProjectID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectManagerID)
                .ForeignKey("dbo.Employees", t => t.ProjectManagerID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectManagerID)
                .Index(t => t.ProjectID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectManagers", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectManagers", "ProjectManagerID", "dbo.Employees");
            DropForeignKey("dbo.HRtables", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.HRtables", "HRID", "dbo.Employees");
            DropIndex("dbo.ProjectManagers", new[] { "ProjectID" });
            DropIndex("dbo.ProjectManagers", new[] { "ProjectManagerID" });
            DropIndex("dbo.HRtables", new[] { "ProjectID" });
            DropIndex("dbo.HRtables", new[] { "HRID" });
            DropTable("dbo.ProjectManagers");
            DropTable("dbo.HRtables");
        }
    }
}
