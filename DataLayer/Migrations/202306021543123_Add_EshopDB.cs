namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_EshopDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureID = c.Int(nullable: false, identity: true),
                        FeatureTitle = c.String(),
                    })
                .PrimaryKey(t => t.FeatureID);
            
            CreateTable(
                "dbo.Product_Features",
                c => new
                    {
                        PF_ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        FeatureID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.PF_ID)
                .ForeignKey("dbo.Features", t => t.FeatureID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.FeatureID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ShortDescription = c.String(maxLength: 500),
                        Text = c.String(),
                        Price = c.Int(nullable: false),
                        ImageName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Product_Group_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductID)
                .ForeignKey("dbo.Product_Groups", t => t.Product_Group_GroupID)
                .Index(t => t.Product_Group_GroupID);
            
            CreateTable(
                "dbo.Product_Selected_Groups",
                c => new
                    {
                        PG_ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        GroupID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PG_ID)
                .ForeignKey("dbo.Product_Groups", t => t.GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Product_Groups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false),
                        ParentID = c.Int(),
                        Product_Groups2_GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.GroupID)
                .ForeignKey("dbo.Product_Groups", t => t.Product_Groups2_GroupID)
                .Index(t => t.Product_Groups2_GroupID);
            
            CreateTable(
                "dbo.Product_Galleries",
                c => new
                    {
                        GalleryID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        ImageName = c.String(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.GalleryID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Product_Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Tag = c.String(),
                    })
                .PrimaryKey(t => t.TagID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleTitle = c.String(),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        UserName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ActiveCode = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Product_Features", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_Tags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_Galleries", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Product_Selected_Groups", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "Product_Group_GroupID", "dbo.Product_Groups");
            DropForeignKey("dbo.Product_Selected_Groups", "GroupID", "dbo.Product_Groups");
            DropForeignKey("dbo.Product_Groups", "Product_Groups2_GroupID", "dbo.Product_Groups");
            DropForeignKey("dbo.Product_Features", "FeatureID", "dbo.Features");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Product_Tags", new[] { "ProductID" });
            DropIndex("dbo.Product_Galleries", new[] { "ProductID" });
            DropIndex("dbo.Product_Groups", new[] { "Product_Groups2_GroupID" });
            DropIndex("dbo.Product_Selected_Groups", new[] { "GroupID" });
            DropIndex("dbo.Product_Selected_Groups", new[] { "ProductID" });
            DropIndex("dbo.Products", new[] { "Product_Group_GroupID" });
            DropIndex("dbo.Product_Features", new[] { "FeatureID" });
            DropIndex("dbo.Product_Features", new[] { "ProductID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Product_Tags");
            DropTable("dbo.Product_Galleries");
            DropTable("dbo.Product_Groups");
            DropTable("dbo.Product_Selected_Groups");
            DropTable("dbo.Products");
            DropTable("dbo.Product_Features");
            DropTable("dbo.Features");
        }
    }
}
