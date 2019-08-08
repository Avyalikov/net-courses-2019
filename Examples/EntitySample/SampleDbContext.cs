namespace EntitySample
{
    using EntitySample.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SampleDbContext : DbContext
    {
        public SampleDbContext()
            : base("name=SampleDbContext")
        {
            Database.SetInitializer(new SampleDbInitializer());
        }

         public DbSet<Author> Authors { get; set; }

         public DbSet<Book> Books { get; set; }
    }
}