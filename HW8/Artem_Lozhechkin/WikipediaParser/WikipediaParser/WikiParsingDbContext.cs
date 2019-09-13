namespace WikipediaParser
{
    using Microsoft.EntityFrameworkCore;
    using WikipediaParser.Models;
    using System.Configuration;

    public class WikiParsingDbContext : DbContext
    {
        public DbSet<LinkEntity> Links { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LinkEntity>()
                .HasKey(l => l.Id);
        }
    }
}
