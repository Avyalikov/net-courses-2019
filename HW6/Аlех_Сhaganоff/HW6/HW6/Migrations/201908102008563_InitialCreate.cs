namespace HW6.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.StockId);
            
            CreateTable(
                "dbo.Traders",
                c => new
                    {
                        TraderId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.TraderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Traders");
            DropTable("dbo.Stocks");
        }
    }
}
