namespace ReferenceCollectorApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatingDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.References",
                c => new
                    {
                        Reference = c.String(nullable: false, maxLength: 128),
                        iterationId = c.String(),
                    })
                .PrimaryKey(t => t.Reference);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.References");
        }
    }
}
