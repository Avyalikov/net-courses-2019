namespace TradingApp.Services
{
    using TradingApp.Core.Models;
    using System.Data.Entity;

    class DBContext : DbContext
    {
        public DBContext() : base("name=DBContext")
        {
        }

        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ClientsPortfolios> ClientsPortfolios { get; set; }
        public virtual DbSet<Shares> Shares { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<Clients>()
                 .HasKey(c => c.ClientID)
                 .ToTable("Clients");

            modelBuilder
                 .Entity<ClientsPortfolios>()
                 .HasKey(p => new { p.ClientID, p.ShareID })
                 .ToTable("ClientsPortfolios");

            modelBuilder
                .Entity<Shares>()
                .HasKey(s => s.ShareID)
                .ToTable("Shares");

            modelBuilder
                .Entity<Transactions>()
                .HasKey(t => t.TransactionID)
                .ToTable("Transactions");

            modelBuilder
                .Entity<Clients>()
                .HasMany(c => c.ClientPortfolios)
                .WithRequired(p => p.Clients)
                .HasForeignKey(p => p.ClientID);

            modelBuilder
                .Entity<Clients>()
                .Property(c => c.Balance)
                .HasPrecision(20, 5);

            modelBuilder
                .Entity<Clients>()
                .HasMany(c => c.ClientPortfolios)
                .WithRequired(p => p.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder
                .Entity<Shares>()
                .HasMany(s => s.ClientsPortfolios)
                .WithRequired(p => p.Shares)
                .HasForeignKey(p => p.ShareID);

            modelBuilder
                .Entity<Shares>()
                .Property(s => s.ShareType)
                .IsFixedLength();

            modelBuilder
                .Entity<Shares>()
                .Property(s => s.Price)
                .HasPrecision(20, 5);

            modelBuilder
                .Entity<Shares>()
                .HasMany(s => s.ClientsPortfolios)
                .WithRequired(p => p.Shares)
                .WillCascadeOnDelete(false);
        }
    }
}
