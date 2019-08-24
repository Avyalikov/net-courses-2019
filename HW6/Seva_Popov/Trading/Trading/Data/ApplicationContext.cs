using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trading.Interfaces;

namespace Trading.Data
{
    public class ApplicationContext : DbContext, IApplicationContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Share> Shares { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserShare>()
                .HasKey(t => new { t.UserId, t.ShareId });

            modelBuilder.Entity<UserShare>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserShares)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserShare>()
                .HasOne(sc => sc.Share)
                .WithMany(c => c.UserShares)
                .HasForeignKey(sc => sc.ShareId);

            modelBuilder.Entity<User>().HasData(
           new User[]
           {
           new User {Id = 1, Surname = "Менделеев", Name = "Дмитрий", Balance = 1000, Phone = "777-77-77" },
            new User {Id = 2, Surname = "Циолковский", Name = "Константин", Balance = 2000, Phone = "555-55-55" },
            new User {Id = 3, Surname = "Пирогов", Name = "Николай", Balance = 3000, Phone = "999-99-99" },
            new User {Id = 4, Surname = "Королёв", Name = "Сергей", Balance = 4000, Phone = "111-11-11" }
           });

            modelBuilder.Entity<Share>().HasData(
            new Share[]
            {
             new Share {Id = 1, Name = "GAZP.ME", Price = 224 },
             new Share {Id = 2, Name = "FB", Price = 180 },
             new Share {Id = 4, Name = "MAGN", Price = 3520 }
            });

            modelBuilder.Entity<UserShare>().HasData(
           new UserShare[]
           {
                new UserShare { ShareId = 1, UserId = 1, AmountStocks = 12200 },
                new UserShare { ShareId = 2, UserId = 1, AmountStocks = 1500 },
                new UserShare { ShareId = 4, UserId = 2, AmountStocks = 65040 },
                new UserShare { ShareId = 2, UserId = 3, AmountStocks = 37100 },
                new UserShare { ShareId = 1, UserId = 4, AmountStocks = 4400 }
           });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-PS4FLU8;Database=eTest;Trusted_Connection=True;");
        }

        public void AddInitialData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //User user1 = new User { Surname = "Менделеев", Name = "Дмитрий", Balance = 1000, Phone = "777-77-77" };
                //User user2 = new User { Surname = "Циолковский", Name = "Константин", Balance = 2000, Phone = "555-55-55" };
                //User user3 = new User { Surname = "Пирогов", Name = "Николай", Balance = 3000, Phone = "999-99-99" };
                //User user4 = new User { Surname = "Королёв", Name = "Сергей", Balance = 4000, Phone = "111-11-11" };
                //db.Users.AddRange(new List<User> { user1, user2, user3, user4 });

                //Share share1 = new Share { Name = "GAZP.ME", Price = 224 };
                //Share share2 = new Share { Name = "FB", Price = 180 };
                //db.Shares.AddRange(new List<Share> { share1, share2});

                db.SaveChanges();

                //user1.UserShares.Add(new UserShare { ShareId = share1.Id, UserId = user1.Id, AmountStocks = 12200 });
                //user1.UserShares.Add(new UserShare { ShareId = share2.Id, UserId = user1.Id, AmountStocks = 1500 });
                //user2.UserShares.Add(new UserShare { ShareId = share1.Id, UserId = user2.Id, AmountStocks = 65040 });
                //user3.UserShares.Add(new UserShare { ShareId = share1.Id, UserId = user3.Id, AmountStocks = 37100 });
                //user4.UserShares.Add(new UserShare { ShareId = share1.Id, UserId = user4.Id, AmountStocks = 4400 });

                //db.SaveChanges();

                //TransactionHistory transactionHistory1 = new TransactionHistory { SellerId = user1.Id, CustomerId = user2.Id, ShareName = share1.Id, DateTimeBay = DateTime.Now, AmountShare = 4, Cost = 220 };
               // db.TransactionHistories.Add(transactionHistory1);
                db.SaveChanges();
            }
        }
    }
}
