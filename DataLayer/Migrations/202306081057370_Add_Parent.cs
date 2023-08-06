namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Parent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product_Comments", "ParentID", c => c.Int());
            AddColumn("dbo.Product_Comments", "Product_Comments2_CommentID", c => c.Int());
            CreateIndex("dbo.Product_Comments", "Product_Comments2_CommentID");
            AddForeignKey("dbo.Product_Comments", "Product_Comments2_CommentID", "dbo.Product_Comments", "CommentID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product_Comments", "Product_Comments2_CommentID", "dbo.Product_Comments");
            DropIndex("dbo.Product_Comments", new[] { "Product_Comments2_CommentID" });
            DropColumn("dbo.Product_Comments", "Product_Comments2_CommentID");
            DropColumn("dbo.Product_Comments", "ParentID");
        }
    }
}
