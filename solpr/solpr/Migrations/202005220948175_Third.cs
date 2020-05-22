namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Third : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Maintenances",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComputerId = c.Int(nullable: false),
                        RepairStart = c.DateTime(nullable: false),
                        RepairFinish = c.DateTime(),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Computers", t => t.ComputerId, cascadeDelete: true)
                .Index(t => t.ComputerId);
            
            AddColumn("dbo.Computers", "ScrapDate", c => c.DateTime());
            AddColumn("dbo.Computers", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Maintenances", "ComputerId", "dbo.Computers");
            DropIndex("dbo.Maintenances", new[] { "ComputerId" });
            DropColumn("dbo.Computers", "Description");
            DropColumn("dbo.Computers", "ScrapDate");
            DropTable("dbo.Maintenances");
        }
    }
}
