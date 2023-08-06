namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_CommentsTB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product_Comments",
                c => new
                    {
                        CommentID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false),
                        WebSite = c.String(),
                        Comment = c.String(nullable: false, maxLength: 800),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Comments", "ProductID", "dbo.Products");
            DropIndex("dbo.Product_Comments", new[] { "ProductID" });
            DropTable("dbo.Product_Comments");
        }
    }
}
