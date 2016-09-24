namespace API.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_country_and_city : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Country", c => c.String());
            AddColumn("dbo.AspNetUsers", "City", c => c.String());
            AddColumn("dbo.NotActiveUsers", "Country", c => c.String());
            AddColumn("dbo.NotActiveUsers", "City", c => c.String());
            DropColumn("dbo.AspNetUsers", "Adress");
            DropColumn("dbo.NotActiveUsers", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotActiveUsers", "Adress", c => c.String());
            AddColumn("dbo.AspNetUsers", "Adress", c => c.String());
            DropColumn("dbo.NotActiveUsers", "City");
            DropColumn("dbo.NotActiveUsers", "Country");
            DropColumn("dbo.AspNetUsers", "City");
            DropColumn("dbo.AspNetUsers", "Country");
        }
    }
}
