namespace Multithread.Console
{
    using Multithread.Core.Models;
    using System.Data.Entity;

    public class LinksDbContext : DbContext
    {
        static LinksDbContext()
        {
            Database.SetInitializer<LinksDbContext>(new ContextInitializer());
        }

        public LinksDbContext() : base("DBConnection")
        { }

        public DbSet<Link> Links { get; set; }
    }

    internal class ContextInitializer : DropCreateDatabaseAlways<LinksDbContext>
    { }
}
