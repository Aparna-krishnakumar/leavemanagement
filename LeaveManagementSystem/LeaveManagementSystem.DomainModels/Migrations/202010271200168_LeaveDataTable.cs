namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeaveDataTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeaveDatas",
                c => new
                    {
                        LeaveID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ReasonOfAbsence = c.String(),
                        Approved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.LeaveID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LeaveDatas");
        }
    }
}
