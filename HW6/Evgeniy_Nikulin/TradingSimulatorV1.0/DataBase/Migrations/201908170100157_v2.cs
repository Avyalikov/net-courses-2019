namespace DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Traders", "Reputation", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Traders", "Reputation");
        }
    }
}
