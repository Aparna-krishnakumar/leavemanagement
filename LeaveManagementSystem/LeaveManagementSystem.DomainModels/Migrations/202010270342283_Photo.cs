﻿namespace LeaveManagementSystem.DomainModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Photo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Photo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Photo");
        }
    }
}
