namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Peripheries", "EmployeeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Peripheries", "EmployeeId");
            AddForeignKey("dbo.Peripheries", "EmployeeId", "dbo.Employees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Peripheries", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Peripheries", new[] { "EmployeeId" });
            DropColumn("dbo.Peripheries", "EmployeeId");
        }
    }
}
