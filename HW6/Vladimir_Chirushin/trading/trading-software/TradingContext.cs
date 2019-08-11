using System.Data.Entity;
namespace trading_software
{
    public class TradingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> TransactionHistory { get; set; }

    }
}
