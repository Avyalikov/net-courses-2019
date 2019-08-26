using stockSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stockSimulator.Modulation
{
    class StockSimulatorDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<HistoryEntity> TransactionHistory { get; set; }

        public DbSet<StockEntity> Stocks { get; set; }

        public DbSet<StockOfClientsEntity> StockOfClients { get; set; }

        public StockSimulatorDbContext(string connectionString) : base(connectionString)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
