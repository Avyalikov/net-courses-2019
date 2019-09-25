namespace MultithreadLinksApp
{
    using MultithreadLinksApp.Core.Models;
    using System.Data.Entity;
    public class AppDBContext : DbContext
    {
        public AppDBContext()
            : base("name=AppDBContext")
        {
        }

        public virtual DbSet<URL> URLs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<URL>()
                 .HasKey(u => u.Url)
                 .ToTable("URLs");
        }
    }
}
