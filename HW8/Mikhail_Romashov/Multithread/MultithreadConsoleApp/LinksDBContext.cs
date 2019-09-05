using Multithread.Core.Models;
using System.Data.Entity;

namespace MultithreadConsoleApp
{
    class LinksDBContext : DbContext
    {
        public DbSet<LinkEntity> Links { get; set; }

        public LinksDBContext(string connectionString) : base(connectionString)
        {
            //Database.SetInitializer<TradingSimulatorDBContext>(new TraidingDbInitializer());
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