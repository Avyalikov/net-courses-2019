namespace TradingSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brokers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Money = c.Int(nullable: false),
                        Card_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonalCards", t => t.Card_ID)
                .Index(t => t.Card_ID);
            
            CreateTable(
                "dbo.PersonalCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Broker_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brokers", t => t.Broker_ID)
                .Index(t => t.Broker_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "Broker_ID", "dbo.Brokers");
            DropForeignKey("dbo.Brokers", "Card_ID", "dbo.PersonalCards");
            DropIndex("dbo.Shares", new[] { "Broker_ID" });
            DropIndex("dbo.Brokers", new[] { "Card_ID" });
            DropTable("dbo.Shares");
            DropTable("dbo.PersonalCards");
            DropTable("dbo.Brokers");
        }
    }
}
