namespace TradingSoftware.ConsoleClient
{
    using System.Data.Entity;
    using TradingSoftware.Core.Models;

    public class TradingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Share> Shares { get; set; }

        public DbSet<Transaction> TransactionHistory { get; set; }

        public DbSet<BlockOfShares> BlockOfSharesTable { get; set; }
    }
}