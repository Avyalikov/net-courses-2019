using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;

namespace Trading.Repository.Context
{
    public class TradesEmulatorDbContext: DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<ClientSharesEntity> ClientShares { get; set; }
        public DbSet<SharesEntity> Shares { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHistories { get; set; }
        public TradesEmulatorDbContext() : base ("name=TradesEmulatorConnectionString")
        {
            Database.SetInitializer(new TradesEmulatorDbInitializer());
        }
        public static TradesEmulatorDbContext GetInstance { get => new TradesEmulatorDbContext(); }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ClientEntity>()
                .HasKey(p => p.Id)
                .ToTable("Clients")
                .HasMany(p=> p.Portfolio)
                .WithRequired(p=> p.Client);

            modelBuilder
                .Entity<ClientSharesEntity>()
                .HasKey(p => p.Id)
                .ToTable("ClientShares");

            modelBuilder
                .Entity<SharesEntity>()
                .HasKey(p => p.Id)
                .ToTable("Shares");

            modelBuilder
                .Entity<TransactionHistoryEntity>()
                .HasKey(p => p.Id)
                .ToTable("TransactionHistory");
        }
    }
}
