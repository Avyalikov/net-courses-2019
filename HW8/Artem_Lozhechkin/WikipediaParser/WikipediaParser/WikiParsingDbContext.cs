namespace WikipediaParser
{
    using Microsoft.EntityFrameworkCore;
    using WikipediaParser.Models;

    public class WikiParsingDbContext : DbContext
    {
        public DbSet<LinkEntity> Links { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=WikiParsingApp;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LinkEntity>()
                .HasKey(l => l.Id);
        }
    }
}
