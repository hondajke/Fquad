﻿namespace solpr.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Components", "Model", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Components", "Model");
        }
    }
}
