namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SiteVisits",
                c => new
                    {
                        VisitID = c.Int(nullable: false, identity: true),
                        IP = c.String(maxLength: 150),
                        VisitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VisitID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SiteVisits");
        }
    }
}
