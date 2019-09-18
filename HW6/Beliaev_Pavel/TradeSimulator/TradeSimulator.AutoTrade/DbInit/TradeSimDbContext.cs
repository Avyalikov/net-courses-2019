using System;
using System.Data.Entity;
using System.Linq;
using TradeSimulator.Core.Models;

namespace TradeSimulator.AutoTrade.DbInit
{
    public class TradeSimDbContext : DbContext
    {
        static TradeSimDbContext()
        {
            Database.SetInitializer<TradeSimDbContext>(new TradeSimDbInitializer());
        }

        public TradeSimDbContext() : base("name=TradeSimDbContext")
        {
        }

        public DbSet<AccountEntity> Account { get; set; }

        public DbSet<StockOfClientEntity> StockOfClient { get; set; }

        public DbSet<ClientEntity> Client { get; set; }

        public DbSet<StockPriceEntity> StockPrice { get; set; }

        public DbSet<HistoryEntity> History { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {           
            modelBuilder
                .Entity<AccountEntity>()
                .HasKey(p => p.AccountId)
                .ToTable("Account")
                .HasMany(s => s.Stocks).WithRequired(r => r.AccountForStock)
                .HasForeignKey<int>(k => k.AccountId);

            modelBuilder
                .Entity<ClientEntity>()
                .HasKey(p => p.Id)
                .ToTable("Client");

            modelBuilder
                .Entity<StockPriceEntity>()
                .HasKey(p => p.Id)
                .ToTable("StockPrice");

            modelBuilder
                .Entity<HistoryEntity>()
                .HasKey(p => p.Id)
                .ToTable("History");

        }
    }    
}