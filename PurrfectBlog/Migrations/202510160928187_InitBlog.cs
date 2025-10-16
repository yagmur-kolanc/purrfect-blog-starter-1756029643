namespace PurrfectBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitBlog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 120),
                        Content = c.String(nullable: false),
                        Category = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Authors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.BlogPosts");
        }
    }
}
