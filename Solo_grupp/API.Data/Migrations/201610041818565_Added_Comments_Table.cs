namespace API.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Comments_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Author_Id = c.String(maxLength: 128),
                        News_Id = c.Guid(),
                        Comment_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.News", t => t.News_Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.News_Id)
                .Index(t => t.Comment_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "News_Id", "dbo.News");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropIndex("dbo.Comments", new[] { "News_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropTable("dbo.Comments");
        }
    }
}
