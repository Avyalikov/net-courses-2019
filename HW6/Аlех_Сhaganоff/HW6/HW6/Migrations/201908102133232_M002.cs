namespace HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M002 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        PricePerShare = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateTime = c.DateTime(nullable: false),
                        Buyer_TraderId = c.Int(),
                        Seller_TraderId = c.Int(),
                        Share_ShareId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Traders", t => t.Buyer_TraderId)
                .ForeignKey("dbo.Traders", t => t.Seller_TraderId)
                .ForeignKey("dbo.Shares", t => t.Share_ShareId, cascadeDelete: true)
                .Index(t => t.Buyer_TraderId)
                .Index(t => t.Seller_TraderId)
                .Index(t => t.Share_ShareId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "Share_ShareId", "dbo.Shares");
            DropForeignKey("dbo.Transactions", "Seller_TraderId", "dbo.Traders");
            DropForeignKey("dbo.Transactions", "Buyer_TraderId", "dbo.Traders");
            DropIndex("dbo.Transactions", new[] { "Share_ShareId" });
            DropIndex("dbo.Transactions", new[] { "Seller_TraderId" });
            DropIndex("dbo.Transactions", new[] { "Buyer_TraderId" });
            DropTable("dbo.Transactions");
        }
    }
}
