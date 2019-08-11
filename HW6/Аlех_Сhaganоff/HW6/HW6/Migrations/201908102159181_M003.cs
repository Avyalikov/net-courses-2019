namespace HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M003 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Portfolios",
                c => new
                    {
                        TraderID = c.Int(nullable: false),
                        ShareId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TraderID, t.ShareId })
                .ForeignKey("dbo.Traders", t => t.TraderID, cascadeDelete: true)
                .Index(t => t.TraderID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Portfolios", "TraderID", "dbo.Traders");
            DropIndex("dbo.Portfolios", new[] { "TraderID" });
            DropTable("dbo.Portfolios");
        }
    }
}
