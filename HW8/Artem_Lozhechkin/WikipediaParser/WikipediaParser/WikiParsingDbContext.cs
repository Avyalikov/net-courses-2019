using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WikipediaParser.Models;

namespace WikipediaParser
{
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
