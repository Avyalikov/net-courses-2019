namespace MultithreadApp.DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Links", "link", c => c.String(maxLength: 2048));
            CreateIndex("dbo.Links", "link", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Links", new[] { "link" });
            AlterColumn("dbo.Links", "link", c => c.String());
        }
    }
}
