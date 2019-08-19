namespace TradingSimulator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Shares", "Broker_ID", "dbo.Brokers");
            DropIndex("dbo.Shares", new[] { "Broker_ID" });
            RenameColumn(table: "dbo.Shares", name: "Broker_ID", newName: "Owner_ID");
            AddColumn("dbo.Shares", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.Shares", "Owner_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Shares", "Owner_ID");
            AddForeignKey("dbo.Shares", "Owner_ID", "dbo.Brokers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shares", "Owner_ID", "dbo.Brokers");
            DropIndex("dbo.Shares", new[] { "Owner_ID" });
            AlterColumn("dbo.Shares", "Owner_ID", c => c.Int());
            DropColumn("dbo.Shares", "Quantity");
            RenameColumn(table: "dbo.Shares", name: "Owner_ID", newName: "Broker_ID");
            CreateIndex("dbo.Shares", "Broker_ID");
            AddForeignKey("dbo.Shares", "Broker_ID", "dbo.Brokers", "ID");
        }
    }
}
