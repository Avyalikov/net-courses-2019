using System;
using System.Data.Entity;
using MultithreadApp.Core.Models;

namespace MultithreadApp.Dependencies
{
    public class MultithreadAppDbContext: DbContext
    {
        public DbSet<PageEntity> Links { get; set; }
        public MultithreadAppDbContext(string connectionString) : base(connectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<PageEntity>()
                 .HasKey(p=>p.IterationId)
                 .ToTable("Links");
        }
    }
}

