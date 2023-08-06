namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Features", "FeatureTitle", c => c.String(nullable: false));
            AlterColumn("dbo.Product_Features", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ShortDescription", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Products", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Product_Galleries", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Product_Tags", "Tag", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Product_Tags", "Tag", c => c.String());
            AlterColumn("dbo.Product_Galleries", "Title", c => c.String());
            AlterColumn("dbo.Products", "Text", c => c.String());
            AlterColumn("dbo.Products", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.Products", "Title", c => c.String());
            AlterColumn("dbo.Product_Features", "Value", c => c.String());
            AlterColumn("dbo.Features", "FeatureTitle", c => c.String());
        }
    }
}
