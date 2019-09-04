using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiURLCollector.Core.Models;

namespace WikiURLCollector.ConsoleApp
{
    public partial class WikiUrlDbContext : DbContext
    {
        public WikiUrlDbContext()
            : base("name=WikiUrlDbContext")
        {
        }
        public virtual DbSet<UrlEntity> Urls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<UrlEntity>()
                 .HasKey(u => u.URL)
                 .ToTable("Urls");
        }
    }
}
