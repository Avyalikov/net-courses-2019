namespace HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M005 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Transactions", "Buyer_TraderId", "dbo.Traders");
            DropForeignKey("dbo.Transactions", "Seller_TraderId", "dbo.Traders");
            DropForeignKey("dbo.Transactions", "Share_ShareId", "dbo.Shares");
            DropIndex("dbo.Transactions", new[] { "Buyer_TraderId" });
            DropIndex("dbo.Transactions", new[] { "Seller_TraderId" });
            DropIndex("dbo.Transactions", new[] { "Share_ShareId" });
            AddColumn("dbo.Transactions", "SellerId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "BuyerId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "ShareId", c => c.Int(nullable: false));
            DropColumn("dbo.Transactions", "Buyer_TraderId");
            DropColumn("dbo.Transactions", "Seller_TraderId");
            DropColumn("dbo.Transactions", "Share_ShareId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Transactions", "Share_ShareId", c => c.Int(nullable: false));
            AddColumn("dbo.Transactions", "Seller_TraderId", c => c.Int());
            AddColumn("dbo.Transactions", "Buyer_TraderId", c => c.Int());
            DropColumn("dbo.Transactions", "ShareId");
            DropColumn("dbo.Transactions", "BuyerId");
            DropColumn("dbo.Transactions", "SellerId");
            CreateIndex("dbo.Transactions", "Share_ShareId");
            CreateIndex("dbo.Transactions", "Seller_TraderId");
            CreateIndex("dbo.Transactions", "Buyer_TraderId");
            AddForeignKey("dbo.Transactions", "Share_ShareId", "dbo.Shares", "ShareId", cascadeDelete: true);
            AddForeignKey("dbo.Transactions", "Seller_TraderId", "dbo.Traders", "TraderId");
            AddForeignKey("dbo.Transactions", "Buyer_TraderId", "dbo.Traders", "TraderId");
        }
    }
}
