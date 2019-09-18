using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Trading.Core;

namespace Trading.WebApp
{
    public partial class TradingDBContext : DbContext
    {
        public TradingDBContext()
            : base("name=TradingDBContext")
        {
        }

        public virtual DbSet<ClientEntity> Clients { get; set; }
        public virtual DbSet<ClientsSharesEntity> ClientsShares { get; set; }
        public virtual DbSet<BalanceEntity> Balances { get; set; }
        public virtual DbSet<TransactionHistoryEntity> Transactions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                 .Entity<ClientEntity>()
                 .HasKey(c => c.ClientID)
                 .ToTable("Clients");

            modelBuilder
                .Entity<ClientsSharesEntity>()
                .HasKey(cs => new { cs.ClientID, cs.ShareID })
                .ToTable("ClientsShares");

            modelBuilder
                 .Entity<BalanceEntity>()
                 .HasKey(c => c.ClientID)
                 .ToTable("Balances");

            modelBuilder
                 .Entity<TransactionHistoryEntity>()
                 .HasKey(c => c.TransactionID)
                 .ToTable("TransactionsHistory");

            modelBuilder.Entity<ClientEntity>()
                .HasMany(e => e.ClientsShares)
                .WithRequired(e => e.Clients)
                .HasForeignKey(s => s.ClientID);

            modelBuilder.Entity<ClientEntity>()
                .HasRequired(c => c.ClientBalance)
                .WithRequiredPrincipal(b => b.Client);

        }
    }
}
