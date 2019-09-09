namespace ReferenceCollectorApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intfield : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.References", "iterationId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.References", "iterationId", c => c.String());
        }
    }
}
