namespace HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M001 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        ShareId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.ShareId);
            
            CreateTable(
                "dbo.SharePrice",
                c => new
                    {
                        ShareId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ShareId)
                .ForeignKey("dbo.Shares", t => t.ShareId)
                .Index(t => t.ShareId);
            
            CreateTable(
                "dbo.TraderBalance",
                c => new
                    {
                        TraderId = c.Int(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TraderId)
                .ForeignKey("dbo.Traders", t => t.TraderId)
                .Index(t => t.TraderId);
            
            DropTable("dbo.Stocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StockId);
            
            DropForeignKey("dbo.TraderBalance", "TraderId", "dbo.Traders");
            DropForeignKey("dbo.SharePrice", "ShareId", "dbo.Shares");
            DropIndex("dbo.TraderBalance", new[] { "TraderId" });
            DropIndex("dbo.SharePrice", new[] { "ShareId" });
            DropTable("dbo.TraderBalance");
            DropTable("dbo.SharePrice");
            DropTable("dbo.Shares");
        }
    }
}
