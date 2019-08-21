namespace DataBase
{
    using Core.Interfaces;
    using Core.Model;
    using System.Data.Entity;

    public class TradingDbContext : DbContext
    {
        public DbSet<Trader> Traders { get; set; }
        public DbSet<PersonalCard> Cards { get; set; }
        public DbSet<Share> Shares { get; set; }

        public TradingDbContext() : base("name=TradingDbContext")
        {
            Database.SetInitializer(new TradingDbInitializer());
        }
    }
}