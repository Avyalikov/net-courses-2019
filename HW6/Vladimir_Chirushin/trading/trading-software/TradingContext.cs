﻿namespace trading_software
{
    using System.Data.Entity;

    public class TradingContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> TransactionHistory { get; set; }
        public DbSet<BlockOfShares> BlockOfSharesTable { get; set; }
    }
}
