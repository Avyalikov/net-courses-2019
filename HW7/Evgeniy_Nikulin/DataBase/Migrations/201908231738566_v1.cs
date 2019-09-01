namespace TradingSimulator.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShareEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Owner_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TraderEntities", t => t.Owner_ID, cascadeDelete: true)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.TraderEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Card_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CardEntities", t => t.Card_ID, cascadeDelete: true)
                .Index(t => t.Card_ID);
            
            CreateTable(
                "dbo.TransactionEntities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ShareName = c.String(),
                        SharePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShareQuantity = c.Int(nullable: false),
                        Buyer_ID = c.Int(),
                        Seller_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TraderEntities", t => t.Buyer_ID)
                .ForeignKey("dbo.TraderEntities", t => t.Seller_ID)
                .Index(t => t.Buyer_ID)
                .Index(t => t.Seller_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionEntities", "Seller_ID", "dbo.TraderEntities");
            DropForeignKey("dbo.TransactionEntities", "Buyer_ID", "dbo.TraderEntities");
            DropForeignKey("dbo.ShareEntities", "Owner_ID", "dbo.TraderEntities");
            DropForeignKey("dbo.TraderEntities", "Card_ID", "dbo.CardEntities");
            DropIndex("dbo.TransactionEntities", new[] { "Seller_ID" });
            DropIndex("dbo.TransactionEntities", new[] { "Buyer_ID" });
            DropIndex("dbo.TraderEntities", new[] { "Card_ID" });
            DropIndex("dbo.ShareEntities", new[] { "Owner_ID" });
            DropTable("dbo.TransactionEntities");
            DropTable("dbo.TraderEntities");
            DropTable("dbo.ShareEntities");
            DropTable("dbo.CardEntities");
        }
    }
}
