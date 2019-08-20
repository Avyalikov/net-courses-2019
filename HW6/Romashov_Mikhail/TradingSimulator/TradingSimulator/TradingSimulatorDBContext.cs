using System.Data.Entity;
using TradingSimulator.Core.Models;

namespace TradingSimulator
{
    class TradingSimulatorDBContext : DbContext
    {
        public DbSet<TraderEntity> Traders { get; set; }

        public DbSet<StockEntity> Stocks { get; set; }

        public DbSet<StockToTraderEntity> TraderStocks { get; set; }

        public DbSet<HistoryEntity> TradeHistory { get; set; }

        public TradingSimulatorDBContext(string connectionString) : base(connectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<TraderEntity>()
                 .HasKey(p => p.Id)
                 .ToTable("Traders");

            modelBuilder
                .Entity<StockEntity>()
                .HasKey(p => p.Id)
                .ToTable("Stocks");

            modelBuilder
                .Entity<StockToTraderEntity>()
                .HasKey(p => p.Id)
                .ToTable("TraderStocks");

            modelBuilder
              .Entity<HistoryEntity>()
              .HasKey(p => p.Id)
              .ToTable("TradeHistory");
        }
    }
}
