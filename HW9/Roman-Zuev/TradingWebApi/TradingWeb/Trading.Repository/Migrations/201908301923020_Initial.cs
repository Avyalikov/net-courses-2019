namespace Trading.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegistationDateTime = c.DateTime(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientShares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Shares_Id = c.Int(),
                        Client_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shares", t => t.Shares_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id, cascadeDelete: true)
                .Index(t => t.Shares_Id)
                .Index(t => t.Client_Id);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SharesType = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TransactionHistory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Buyer_Id = c.Int(),
                        SelledItem_Id = c.Int(),
                        Seller_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.Buyer_Id)
                .ForeignKey("dbo.Shares", t => t.SelledItem_Id)
                .ForeignKey("dbo.Clients", t => t.Seller_Id)
                .Index(t => t.Buyer_Id)
                .Index(t => t.SelledItem_Id)
                .Index(t => t.Seller_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionHistory", "Seller_Id", "dbo.Clients");
            DropForeignKey("dbo.TransactionHistory", "SelledItem_Id", "dbo.Shares");
            DropForeignKey("dbo.TransactionHistory", "Buyer_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientShares", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.ClientShares", "Shares_Id", "dbo.Shares");
            DropIndex("dbo.TransactionHistory", new[] { "Seller_Id" });
            DropIndex("dbo.TransactionHistory", new[] { "SelledItem_Id" });
            DropIndex("dbo.TransactionHistory", new[] { "Buyer_Id" });
            DropIndex("dbo.ClientShares", new[] { "Client_Id" });
            DropIndex("dbo.ClientShares", new[] { "Shares_Id" });
            DropTable("dbo.TransactionHistory");
            DropTable("dbo.Shares");
            DropTable("dbo.ClientShares");
            DropTable("dbo.Clients");
        }
    }
}
