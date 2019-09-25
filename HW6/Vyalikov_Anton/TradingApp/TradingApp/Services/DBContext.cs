namespace TradingApp.Services
{
    using TradingApp.Core.Models;
    using System.Data.Entity;

    class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientPortfolio> ClientsPortfolios { get; set; }
        public virtual DbSet<Share> Shares { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<Client>()
                 .HasKey(c => c.ClientID)
                 .ToTable("Clients");

            modelBuilder
                 .Entity<ClientPortfolio>()
                 .HasKey(p => new { p.ClientID, p.ShareID })
                 .ToTable("ClientsPortfolios");

            modelBuilder
                .Entity<Share>()
                .HasKey(s => s.ShareID)
                .ToTable("Shares");

            modelBuilder
                .Entity<Transaction>()
                .HasKey(t => t.TransactionID)
                .ToTable("Transactions");

            modelBuilder
                .Entity<Client>()
                .HasMany(c => c.ClientPortfolios)
                .WithRequired(p => p.Clients)
                .HasForeignKey(p => p.ClientID);

            modelBuilder
                .Entity<Client>()
                .Property(c => c.Balance)
                .HasPrecision(20, 5);

            modelBuilder
                .Entity<Client>()
                .HasMany(c => c.ClientPortfolios)
                .WithRequired(p => p.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<Share>()
                .HasMany(s => s.ClientsPortfolios)
                .WithRequired(p => p.Shares)
                .HasForeignKey(p => p.ShareID);

            modelBuilder
                .Entity<Share>()
                .Property(s => s.ShareType)
                .IsFixedLength();

            modelBuilder
                .Entity<Share>()
                .Property(s => s.Price)
                .HasPrecision(20, 5);

            modelBuilder
                .Entity<Share>()
                .HasMany(s => s.ClientsPortfolios)
                .WithRequired(p => p.Shares)
                .WillCascadeOnDelete(false);
        }
    }
}
