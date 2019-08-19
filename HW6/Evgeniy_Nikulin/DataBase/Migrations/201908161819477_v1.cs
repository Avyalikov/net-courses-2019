namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PersonalCards",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Surname = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Shares",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Owner_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Traders", t => t.Owner_ID, cascadeDelete: true)
                .Index(t => t.Owner_ID);
            
            CreateTable(
                "dbo.Traders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Money = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Card_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PersonalCards", t => t.Card_ID, cascadeDelete: true)
                .Index(t => t.Card_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "Owner_ID", "dbo.Traders");
            DropForeignKey("dbo.Traders", "Card_ID", "dbo.PersonalCards");
            DropIndex("dbo.Traders", new[] { "Card_ID" });
            DropIndex("dbo.Shares", new[] { "Owner_ID" });
            DropTable("dbo.Traders");
            DropTable("dbo.Shares");
            DropTable("dbo.PersonalCards");
        }
    }
}
