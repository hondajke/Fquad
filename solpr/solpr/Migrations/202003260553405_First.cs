namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        ManufacturerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId);
            
            CreateTable(
                "dbo.Computers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Surname = c.String(),
                        Name = c.String(),
                        Patronymic_Name = c.String(),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Peripheries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Model = c.String(maxLength: 100),
                        ManufacturerId = c.Int(nullable: false),
                        EmployeeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId, cascadeDelete: true)
                .Index(t => t.ManufacturerId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Specs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Value = c.String(maxLength: 100),
                        PeripheryId = c.Int(),
                        ComponentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentId)
                .ForeignKey("dbo.Peripheries", t => t.PeripheryId)
                .Index(t => t.PeripheryId)
                .Index(t => t.ComponentId);
            
            CreateTable(
                "dbo.ComputerComponents",
                c => new
                    {
                        Computer_Id = c.Int(nullable: false),
                        Component_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Computer_Id, t.Component_Id })
                .ForeignKey("dbo.Computers", t => t.Computer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Components", t => t.Component_Id, cascadeDelete: true)
                .Index(t => t.Computer_Id)
                .Index(t => t.Component_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specs", "PeripheryId", "dbo.Peripheries");
            DropForeignKey("dbo.Specs", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.Peripheries", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Peripheries", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Components", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.Computers", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Employees", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.ComputerComponents", "Component_Id", "dbo.Components");
            DropForeignKey("dbo.ComputerComponents", "Computer_Id", "dbo.Computers");
            DropIndex("dbo.ComputerComponents", new[] { "Component_Id" });
            DropIndex("dbo.ComputerComponents", new[] { "Computer_Id" });
            DropIndex("dbo.Specs", new[] { "ComponentId" });
            DropIndex("dbo.Specs", new[] { "PeripheryId" });
            DropIndex("dbo.Peripheries", new[] { "EmployeeId" });
            DropIndex("dbo.Peripheries", new[] { "ManufacturerId" });
            DropIndex("dbo.Employees", new[] { "DepartmentId" });
            DropIndex("dbo.Computers", new[] { "EmployeeId" });
            DropIndex("dbo.Components", new[] { "ManufacturerId" });
            DropTable("dbo.ComputerComponents");
            DropTable("dbo.Specs");
            DropTable("dbo.Peripheries");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Departments");
            DropTable("dbo.Employees");
            DropTable("dbo.Computers");
            DropTable("dbo.Components");
        }
    }
}
