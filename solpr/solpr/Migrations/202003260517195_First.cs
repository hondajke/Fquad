namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Components", "SpecId", "dbo.Specs");
            DropForeignKey("dbo.Peripheries", "SpecId", "dbo.Specs");
            DropIndex("dbo.Components", new[] { "SpecId" });
            DropIndex("dbo.Peripheries", new[] { "SpecId" });
            RenameColumn(table: "dbo.Specs", name: "SpecId", newName: "Component_Id");
            RenameColumn(table: "dbo.Specs", name: "SpecId", newName: "Periphery_Id");
            CreateIndex("dbo.Specs", "Periphery_Id");
            CreateIndex("dbo.Specs", "Component_Id");
            AddForeignKey("dbo.Specs", "Component_Id", "dbo.Components", "Id");
            AddForeignKey("dbo.Specs", "Periphery_Id", "dbo.Peripheries", "Id");
            DropColumn("dbo.Components", "SpecId");
            DropColumn("dbo.Peripheries", "SpecId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Peripheries", "SpecId", c => c.Int(nullable: false));
            AddColumn("dbo.Components", "SpecId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Specs", "Periphery_Id", "dbo.Peripheries");
            DropForeignKey("dbo.Specs", "Component_Id", "dbo.Components");
            DropIndex("dbo.Specs", new[] { "Component_Id" });
            DropIndex("dbo.Specs", new[] { "Periphery_Id" });
            RenameColumn(table: "dbo.Specs", name: "Periphery_Id", newName: "SpecId");
            RenameColumn(table: "dbo.Specs", name: "Component_Id", newName: "SpecId");
            CreateIndex("dbo.Peripheries", "SpecId");
            CreateIndex("dbo.Components", "SpecId");
            AddForeignKey("dbo.Peripheries", "SpecId", "dbo.Specs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Components", "SpecId", "dbo.Specs", "Id", cascadeDelete: true);
        }
    }
}
