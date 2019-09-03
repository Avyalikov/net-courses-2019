namespace TradingAppWebAPI
{
    using TradingApp.Core.Models;
    using System.Data.Entity;

    class DBContext : DbContext
    {
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientPortfolio> ClientsPortfolios { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
    }
}
