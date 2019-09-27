using MultithreadApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Console.DbInit
{
    public class MultiAppDbContext : DbContext
    {
        static MultiAppDbContext()
        {
            Database.SetInitializer<MultiAppDbContext>(new MultiAppDbInit());
        }

        public MultiAppDbContext() : base("LinksContext")
        {

        }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Url>()
                .HasKey(k => k.Id);
        }
    }
}
