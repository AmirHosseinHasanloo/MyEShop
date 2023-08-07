namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSlider : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "Url", c => c.String(nullable: false, maxLength: 450));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "Url");
        }
    }
}
