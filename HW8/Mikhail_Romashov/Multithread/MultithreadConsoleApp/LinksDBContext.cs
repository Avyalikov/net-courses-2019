using Multithread.Core.Models;
using System.Data.Entity;

namespace MultithreadConsoleApp
{
    public class LinksDBContext : DbContext
    {
        public DbSet<LinkEntity> Links { get; set; }

        public LinksDBContext() : base("linksConnectionString")
        {
        
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<LinkEntity>()
                 .HasKey(p => p.Id)
                 .ToTable("Links");
        }
    }
}