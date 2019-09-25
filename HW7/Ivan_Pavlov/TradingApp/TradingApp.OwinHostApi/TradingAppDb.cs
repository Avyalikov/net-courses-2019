namespace TradingApp.OwinHostApi
{
    using System.Data.Entity;
    using TradingApp.Core.Models;

    public class TradingAppDb : DbContext
    {
        static TradingAppDb()
        {
            Database.SetInitializer<TradingAppDb>(new ContextInitializer());
        }

        public TradingAppDb() : base("DbConnection") { }

        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<ShareEntity> Shares { get; set; }
        public virtual DbSet<PortfolioEntity> Portfolio { get; set; }
        public virtual DbSet<TransactionStoryEntity> TransactionsStory { get; set; }
    }

    class ContextInitializer : DropCreateDatabaseIfModelChanges<TradingAppDb>
    {
        protected override void Seed(TradingAppDb db)
        {
            ShareEntity s1 = new ShareEntity { Name = "Сберегаем с Газпромом", CompanyName = "Газпром", Price = 2000 };
            ShareEntity s2 = new ShareEntity { Name = "Выслуга лет в EPAM", CompanyName = "EPAM", Price = 1500 };
            ShareEntity s3 = new ShareEntity { Name = "Кофейная", CompanyName = "Nescafe", Price = 1000 };
            ShareEntity s4 = new ShareEntity { Name = "Для любителей печенек", CompanyName = "Pechenka Official", Price = 200 };
            db.Shares.Add(s1); db.Shares.Add(s2);
            db.Shares.Add(s3); db.Shares.Add(s4);

            UserEntity u1 = new UserEntity { SurName = "Петров", Name = "Петр", Balance = 10000, Phone = "89523536454" };
            UserEntity u2 = new UserEntity { SurName = "Пупкин", Name = "Вася", Balance = 25000, Phone = "89992323265" };
            UserEntity u3 = new UserEntity { SurName = "Иванов", Name = "Семен", Balance = 30000, Phone = "85555555555" };
            UserEntity u4 = new UserEntity { SurName = "Маликов", Name = "Дмитрий", Balance = 50000, Phone = "81111111111" };
            db.Users.Add(u1); db.Users.Add(u2);
            db.Users.Add(u3); db.Users.Add(u4);

            db.SaveChanges();

            SeedUsersStocks();
        }

        internal static void SeedUsersStocks()
        {
            using (TradingAppDb db = new TradingAppDb())
            {
                UserEntity u = db.Users.Find(1);
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(2), Amount = 50, UserEntityId = u.Id, ShareId = 2 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(1), Amount = 30, UserEntityId = u.Id, ShareId = 1 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(4), Amount = 100, UserEntityId = u.Id, ShareId = 4 });
                u = db.Users.Find(2);
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(2), Amount = 100, UserEntityId = u.Id, ShareId = 2 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(1), Amount = 50, UserEntityId = u.Id, ShareId = 1 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(3), Amount = 200, UserEntityId = u.Id, ShareId = 3 });
                u = db.Users.Find(3);
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(4), Amount = 30, UserEntityId = u.Id, ShareId = 4 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(3), Amount = 50, UserEntityId = u.Id, ShareId = 3 });
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(1), Amount = 20, UserEntityId = u.Id, ShareId = 1 });
                u = db.Users.Find(4);
                u.UsersShares.Add(new PortfolioEntity { Share = db.Shares.Find(1), Amount = 500, UserEntityId = u.Id, ShareId = 1 });

                db.SaveChanges();
            }
        }
    }
}