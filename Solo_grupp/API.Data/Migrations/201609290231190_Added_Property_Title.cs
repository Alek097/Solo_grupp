namespace API.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Property_Title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Title", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Title");
        }
    }
}
