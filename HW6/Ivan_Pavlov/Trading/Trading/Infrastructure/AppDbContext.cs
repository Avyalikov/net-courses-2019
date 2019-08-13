namespace Trading.Infrastructure
{
    using System.Data.Entity;
    using Trading.Models;
    using System.Collections.Generic;

    class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(new ContextInitializer());           
        }

        public AppDbContext() : base("DbConnection") { }

        public DbSet<TypeStock> TypesStocks { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserStocks> UserStocks { get; set; }
    }

    class ContextInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext db)
        {
            TypeStock t1 = new TypeStock { Type = "Для торговли" };
            TypeStock t2 = new TypeStock { Type = "Дивидендная" };
            TypeStock t3 = new TypeStock { Type = "Персональная" };
            db.TypesStocks.Add(t1); db.TypesStocks.Add(t2);
            db.TypesStocks.Add(t3);

            Stock s1 = new Stock { Name = "Сберегаем с Газпромом", Company = "Газпром", Price = 2000, TypeStock = t2 };
            Stock s2 = new Stock { Name = "Выслуга лет в EPAM", Company = "EPAM", Price = 1500, TypeStock = t3 };
            Stock s3 = new Stock { Name = "Кофейная", Company = "Nescafe", Price = 1000, TypeStock = t1 };
            Stock s4 = new Stock { Name = "Для любителей печенек", Company = "Pechenka Official", Price = 200, TypeStock = t1 };
            db.Stocks.Add(s1); db.Stocks.Add(s2);
            db.Stocks.Add(s3); db.Stocks.Add(s4);

            User u1 = new User { SurName = "Петров", Name = "Петр", Balance = 10000 };
            User u2 = new User { SurName = "Смирнов", Name = "Алексей", Balance = 25000 };
            User u3 = new User { SurName = "Иванов", Name = "Семен", Balance = 30000 };
            User u4 = new User { SurName = "Маликов", Name = "Дмитрий", Balance = 50000 };
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
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(2), AmountStocks = 50 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(1), AmountStocks = 30 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(4), AmountStocks = 100 });
                u = db.Users.Find(2);
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(2), AmountStocks = 100 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(1), AmountStocks = 50 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(3), AmountStocks = 200 });
                u = db.Users.Find(3);
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(4), AmountStocks = 30 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(3), AmountStocks = 50 });
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(1), AmountStocks = 20 });
                u = db.Users.Find(4);
                u.UserStocks.Add(new UserStocks { Stock = db.Stocks.Find(1), AmountStocks = 500 });

                db.SaveChanges();
            }
        }
    }
}
