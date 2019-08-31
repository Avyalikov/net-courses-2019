namespace TradingApp.Shared
{
    using System.Data.Entity;
    using TradingApp.Core.Models;
    public class TradingAppDbContext : DbContext
    {
        public DbSet<CompanyEntity> Companies { get; set; }
        public DbSet<ShareEntity> Shares { get; set; }
        public DbSet<ShareTypeEntity> ShareTypes { get; set; }
        public DbSet<StockEntity> Stocks { get; set; }
        public DbSet<TraderEntity> Traders { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        public TradingAppDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<CompanyEntity>()
                 .HasKey(c => c.Id)
                 .ToTable("Companies");

            modelBuilder
                 .Entity<StockEntity>()
                 .HasKey(s => s.Id)
                 .ToTable("Stocks");


            modelBuilder
                .Entity<ShareEntity>()
                .HasKey(s => s.Id)
                .ToTable("Shares");

            modelBuilder
                .Entity<ShareTypeEntity>()
                .HasKey(st => st.Id)
                .ToTable("ShareTypes");

            modelBuilder
              .Entity<TraderEntity>()
              .HasKey(t => t.Id)
              .ToTable("Traders");

            modelBuilder
              .Entity<TransactionEntity>()
              .HasKey(t => t.Id)
              .ToTable("Transactions");
        }
    }
}
