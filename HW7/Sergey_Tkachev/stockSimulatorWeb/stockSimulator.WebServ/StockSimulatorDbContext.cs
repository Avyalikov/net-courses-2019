using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using stockSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace stockSimulator.WebServ
{
    public class StockSimulatorDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<HistoryEntity> TransactionHistory { get; set; }

        public DbSet<StockEntity> Stocks { get; set; }

        public DbSet<StockOfClientsEntity> StockOfClients { get; set; }

        public StockSimulatorDbContext(DbContextOptions<StockSimulatorDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
