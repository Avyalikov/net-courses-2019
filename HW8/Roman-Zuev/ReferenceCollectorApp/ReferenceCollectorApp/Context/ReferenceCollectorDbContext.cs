using ReferenceCollectorApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceCollectorApp.Context
{
    public class ReferenceCollectorDbContext: DbContext
    {
        public DbSet<ReferenceEntity> References { get; set; }
        public ReferenceCollectorDbContext() : base ("name=ReferenceCollectorConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ReferenceCollectorDbContext>());
        }
        //public static ReferenceCollectorDbContext GetInstance { get => new ReferenceCollectorDbContext(); }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ReferenceEntity>()
                .HasKey(p => p.Reference)
                .ToTable("References");
        }
    }
}
