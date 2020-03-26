namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departments", "Name", c => c.String());
        }
    }
}
