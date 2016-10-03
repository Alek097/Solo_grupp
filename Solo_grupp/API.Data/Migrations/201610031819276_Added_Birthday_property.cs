namespace API.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Birthday_property : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Birthday", c => c.DateTime(nullable: false));
            AddColumn("dbo.NotActiveUsers", "Birthday", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NotActiveUsers", "Birthday");
            DropColumn("dbo.AspNetUsers", "Birthday");
        }
    }
}
