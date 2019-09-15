using Microsoft.EntityFrameworkCore;
using Multithread.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.ConsoleApp.Data
{
    public class MultithreadDbContext : DbContext
    {
        public DbSet<LinksHistoryEntity> Links { get; set; }

        public MultithreadDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PS4FLU8;Database=LinkstDb;Trusted_Connection=True;");
        }
    }
}
