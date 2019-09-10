using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;
using System.Data.Entity;


namespace TradingConsoleApp
{
    public class TradingAppDbContext : DbContext
    {
        public DbSet <BalanceEntity> Balances { get; set; }
        public DbSet <StockEntity> Stocks { get; set; }
        public DbSet <TransactionHistoryEntity> Transactions { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public TradingAppDbContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer<TradingAppDbContext>(new ContextInitializer());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<UserEntity>()
                 .HasKey(p => p.ID)
                 .ToTable("Users");

            modelBuilder
                .Entity<BalanceEntity>()
                .HasKey(p => p.BalanceID)
                .ToTable("Balances");

            modelBuilder
                .Entity<StockEntity>()
                .HasKey(p => p.ID)
                .ToTable("Stocks");

            modelBuilder
                .Entity<TransactionHistoryEntity>()
                .HasKey(p => p.TransactionID)
                .ToTable("Transactions");
        }
    }
}
