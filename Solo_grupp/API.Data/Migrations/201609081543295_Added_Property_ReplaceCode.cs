namespace API.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Property_ReplaceCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ReplaceCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ReplaceCode");
        }
    }
}
