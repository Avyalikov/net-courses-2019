using System.Data.Entity;
using TradingSimulator.ConsoleApp;
using TradingSimulator.Core.Models;

namespace TradingSimulator
{
    class TradingSimulatorDBContext : DbContext
    {
        public DbSet<TraderEntityDB> Traders { get; set; }

        public DbSet<StockEntityDB> Stocks { get; set; }

        public DbSet<StockToTraderEntityDB> TraderStocks { get; set; }

        public DbSet<HistoryEntity> TradeHistory { get; set; }

        public TradingSimulatorDBContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<TradingSimulatorDBContext>(new TraidingDbInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<TraderEntityDB>()
                 .HasKey(p => p.Id)
                 .ToTable("Traders");

            modelBuilder
                .Entity<StockEntityDB>()
                .HasKey(p => p.Id)
                .ToTable("Stocks");

            modelBuilder
                .Entity<StockToTraderEntityDB>()
                .HasKey(p => p.Id)
                .ToTable("TraderStocks");

            modelBuilder
              .Entity<HistoryEntity>()
              .HasKey(p => p.Id)
              .ToTable("TradeHistory");

            //modelBuilder
            //    .Entity<TraderEntityDB>()
            //    .HasMany(e => e.StockToTraderEntity)
            //    .WithRequired(e => e.Traders)
            //    .HasForeignKey(e => e.TraderId);

            //modelBuilder
            //    .Entity<StockEntityDB>()
            //    .HasMany(e => e.StockToTraderEntity)
            //    .WithRequired(e => e.Stocks)
            //    .HasForeignKey(e => e.StockId);
        }
    }
}