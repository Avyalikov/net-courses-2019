namespace TradingApp.Data
{
    using System.Data.Entity;
    using TradingApp.Data.Models;

    public interface IAppDbContext
    {
        DbSet<Share> Share { get; set; }
        DbSet<TransactionStory> TransactionStory { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserShare> UserShares { get; set; }
        int SaveChanges();
    }
}