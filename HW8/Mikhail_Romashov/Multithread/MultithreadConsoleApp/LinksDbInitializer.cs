using System.Data.Entity;


namespace MultithreadConsoleApp
{
    class LinkDbInitializer : DropCreateDatabaseAlways<LinksDBContext>
    {
        protected override void Seed(LinksDBContext context)
        { 
            base.Seed(context);
        }
    }
}
