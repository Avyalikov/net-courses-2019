namespace Trading.ConsoleApp
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Trading.Core;

    public partial class TradingDBContext : DbContext
    {
        public TradingDBContext()
            : base("name=TradingDBContext")
        {
        }

        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<ClientsSharesEntity> ClientsShares { get; set; }
        public virtual DbSet<ShareEntity> Shares { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<ClientEntity>()
                 .HasKey(c => c.ClientID)
                 .ToTable("Clients");

            modelBuilder
                .Entity<ShareEntity>()
                .HasKey(s => s.ShareID)
                .ToTable("Shares");

            modelBuilder
                .Entity<ClientsSharesEntity>()
                .HasKey(cs => new { cs.ClientID, cs.ShareID })
                .ToTable("ClientsShares");

            modelBuilder.Entity<ClientEntity>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Clients)
                .HasForeignKey(s => s.ClientID);

            modelBuilder.Entity<ShareEntity>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Shares)
                .HasForeignKey(s => s.ShareID);

            modelBuilder.Entity<ClientEntity>()
                .Property(e => e.ClientBalance)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ClientEntity>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Clients)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ShareEntity>()
                .Property(e => e.ShareName)
                .IsFixedLength();

            modelBuilder.Entity<ShareEntity>()
                .Property(e => e.ShareCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ShareEntity>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Shares)
                .WillCascadeOnDelete(false);
        }
    }
}
