namespace ReferenceCollectorApp.Context
{
    using ReferenceCollectorApp.Models;
    using System.Data.Entity;
    public class ReferenceCollectorDbContext: DbContext
    {
        public DbSet<ReferenceEntity> References { get; set; }
        public ReferenceCollectorDbContext() : base ("name=ReferenceCollectorConnectionString")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ReferenceCollectorDbContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ReferenceEntity>()
                .HasKey(p => p.Reference)
                .ToTable("References")
                .Property(p => p.Reference).HasMaxLength(256);
        }
    }
}
