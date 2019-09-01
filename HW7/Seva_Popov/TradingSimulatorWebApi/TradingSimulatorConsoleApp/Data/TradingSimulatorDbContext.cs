using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulatorConsoleApp.Data
{
    public class TradingSimulatorDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<SharesEntity> Shares { get; set; }
        public DbSet<UserAndSharesEntity> UsersAndShares { get; set; }
        public DbSet<TransactionHistoryEntity> TransactionHistories { get; set; }

        public TradingSimulatorDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAndSharesEntity>()
                .HasKey(t => new { t.UserId, t.ShareId });

            modelBuilder.Entity<UserAndSharesEntity>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UsersAndShares)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserAndSharesEntity>()
                .HasOne(sc => sc.Share)
                .WithMany(c => c.UsersAndShares)
                .HasForeignKey(sc => sc.ShareId);

            modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity[]
            {
            new UserEntity {Id = 1, Surname = "Менделеев", Name = "Дмитрий", Balance = 1000000, Phone = "777-77-77" },
            new UserEntity {Id = 2, Surname = "Циолковский", Name = "Константин", Balance = 20000000, Phone = "555-55-55" },
            new UserEntity {Id = 3, Surname = "Пирогов", Name = "Николай", Balance = 30000000, Phone = "999-99-99" },
            new UserEntity {Id = 4, Surname = "Королёв", Name = "Сергей", Balance = 40000000, Phone = "111-11-11" }
            });

            modelBuilder.Entity<SharesEntity>().HasData(
            new SharesEntity[]
            {
             new SharesEntity {Id = 1, Name = "GAZP.ME", Price = 224 },
             new SharesEntity {Id = 2, Name = "FB", Price = 180 },
             new SharesEntity {Id = 3, Name = "GOOG", Price = 180 },
             new SharesEntity {Id = 4, Name = "MAGN", Price = 3520 }
            });

            modelBuilder.Entity<UserAndSharesEntity>().HasData(
            new UserAndSharesEntity[]
            {
                new UserAndSharesEntity { ShareId = 1, UserId = 1, AmountStocks = 50 },
                new UserAndSharesEntity { ShareId = 2, UserId = 1, AmountStocks = 110 },
                new UserAndSharesEntity { ShareId = 3, UserId = 1, AmountStocks = 400 },
                new UserAndSharesEntity { ShareId = 4, UserId = 1, AmountStocks = 300 },
                new UserAndSharesEntity { ShareId = 1, UserId = 2, AmountStocks = 70 },
                new UserAndSharesEntity { ShareId = 2, UserId = 2, AmountStocks = 9 },
                new UserAndSharesEntity { ShareId = 3, UserId = 2, AmountStocks = 50 },
                new UserAndSharesEntity { ShareId = 4, UserId = 2, AmountStocks = 110 },
                new UserAndSharesEntity { ShareId = 1, UserId = 3, AmountStocks= 400 },
                new UserAndSharesEntity { ShareId = 2, UserId = 3, AmountStocks = 300 },
                new UserAndSharesEntity { ShareId = 3, UserId = 3, AmountStocks = 70 },
                new UserAndSharesEntity { ShareId = 4, UserId = 3, AmountStocks = 9 },
                new UserAndSharesEntity { ShareId = 1, UserId = 4, AmountStocks = 9 },
                new UserAndSharesEntity { ShareId = 2, UserId = 4, AmountStocks = 9 },
                new UserAndSharesEntity { ShareId = 3, UserId = 4, AmountStocks = 144 }
            });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PS4FLU8;Database=TradingSimulatorDb;Trusted_Connection=True;");
        }
    }
}
