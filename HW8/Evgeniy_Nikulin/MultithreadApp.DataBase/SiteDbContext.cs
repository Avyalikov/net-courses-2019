namespace MultithreadApp.DataBase
{
    using System.Data.Entity;
    using Model;

    public class SiteDbContext : DbContext
    {
        public DbSet<Links> Links { get; set; }

        public SiteDbContext() : base("name=SiteDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Links>().Property(l => l.link).HasMaxLength(2048);
            modelBuilder.Entity<Links>().HasIndex(l => l.link).IsUnique();
        }
    }
}