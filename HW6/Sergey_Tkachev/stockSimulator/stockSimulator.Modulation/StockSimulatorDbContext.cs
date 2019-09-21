namespace stockSimulator.Modulation
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using stockSimulator.Core.Models;

    internal class StockSimulatorDbContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<HistoryEntity> TransactionHistory { get; set; }

        public DbSet<StockEntity> Stocks { get; set; }

        public DbSet<StockOfClientsEntity> StockOfClients { get; set; }

        /// <summary>
        /// Initializes an Instance of StockSimulatorDbContext class.
        /// </summary>
        public StockSimulatorDbContext() : base("name=StockSimulatorConnectionString")
        {
            Database.SetInitializer(new StockInitializer());
        }

        /// <summary>
        /// Establishes relationships between tables.
        /// </summary>
        /// <param name="modelBuilder">DbModelBuilder instance.</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockOfClientsEntity>()
                .HasRequired(sc => sc.Client)
                .WithMany(c => (ICollection<StockOfClientsEntity>)c.Stocks)
                .HasForeignKey(sc => sc.ClientID);

            modelBuilder.Entity<StockOfClientsEntity>()
               .HasRequired<StockEntity>(sc => sc.Stock)
               .WithMany(s => (ICollection<StockOfClientsEntity>)s.Clients)
               .HasForeignKey(sc => sc.StockID);

            modelBuilder.Entity<ClientEntity>()
               .HasMany<StockOfClientsEntity>(c => c.Stocks)
               .WithRequired(sc => sc.Client)
               .HasForeignKey<int>(sc => sc.ClientID);

            modelBuilder.Entity<StockEntity>()
              .HasMany<StockOfClientsEntity>(s => s.Clients)
               .WithRequired(sc => sc.Stock)
               .HasForeignKey<int>(sc => sc.StockID);
        }
    }
}
