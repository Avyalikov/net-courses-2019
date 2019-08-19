namespace TradingSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brokers", "Money", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Brokers", "Money", c => c.Int(nullable: false));
        }
    }
}
