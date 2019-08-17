namespace TradingApp.Data
{
    using System.Data.Entity;
    using TradingApp.Data.Models;

    public class AppDbContext : DbContext, IAppDbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new ContextInitializer());
        }

        public AppDbContext() : base("DbConnection") { }

        public virtual DbSet<Share> Share { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserShare> UserShares { get; set; }
        public virtual DbSet<TransactionStory> TransactionStory { get; set; }
    }

    class ContextInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext db)
        {
            Share s1 = new Share { Name = "Сберегаем с Газпромом", Company = "Газпром", Price = 2000};
            Share s2 = new Share { Name = "Выслуга лет в EPAM", Company = "EPAM", Price = 1500};
            Share s3 = new Share { Name = "Кофейная", Company = "Nescafe", Price = 1000 };
            Share s4 = new Share { Name = "Для любителей печенек", Company = "Pechenka Official", Price = 200};
            db.Share.Add(s1); db.Share.Add(s2);
            db.Share.Add(s3); db.Share.Add(s4);

            User u1 = new User { SurName = "Петров", Name = "Петр", Balance = 10000, Phone = "89523536454" };
            User u2 = new User { SurName = "Пупкин", Name = "Вася", Balance = 25000, Phone = "89992323265" };
            User u3 = new User { SurName = "Иванов", Name = "Семен", Balance = 30000, Phone = "85555555555" };
            User u4 = new User { SurName = "Маликов", Name = "Дмитрий", Balance = 50000, Phone = "81111111111" };
            db.Users.Add(u1); db.Users.Add(u2);
            db.Users.Add(u3); db.Users.Add(u4);

            db.SaveChanges();

            SeedUsersStocks();
        }

        internal static void SeedUsersStocks()
        {
            using (AppDbContext db = new AppDbContext())
            {
                User u = db.Users.Find(1);
                u.UserShare.Add(new UserShare { Share = db.Share.Find(2), AmountStocks = 50 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(1), AmountStocks = 30 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(4), AmountStocks = 100 });
                u = db.Users.Find(2);
                u.UserShare.Add(new UserShare { Share = db.Share.Find(2), AmountStocks = 100 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(1), AmountStocks = 50 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(3), AmountStocks = 200 });
                u = db.Users.Find(3);
                u.UserShare.Add(new UserShare { Share = db.Share.Find(4), AmountStocks = 30 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(3), AmountStocks = 50 });
                u.UserShare.Add(new UserShare { Share = db.Share.Find(1), AmountStocks = 20 });
                u = db.Users.Find(4);
                u.UserShare.Add(new UserShare { Share = db.Share.Find(1), AmountStocks = 500 });

                db.SaveChanges();
            }
        }
    }
}

