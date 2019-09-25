namespace TradingSimulator.DataBase
{
    using Model;
    using System.Data.Entity;

    public class TradingDbContext : DbContext
    {
        public DbSet<TraderEntity> Traders { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<ShareEntity> Shares { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        public static TradingDbContext GetInstance { get => new TradingDbContext(); }

        public TradingDbContext() : base("TradingDbContext")
        {            
            Database.SetInitializer(new TradingDbInitializer());
            this.Database.Initialize(false);
        }
    }
}