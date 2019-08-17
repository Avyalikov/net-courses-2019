namespace TradingApp
{
    using System.Linq;
    using TradingApp.Data.Models;

    public interface ITradeLogic
    {
        IQueryable<Share> ListStocks();
        string ChangeStockPrice(int id, decimal newPrice);
        string TransactionRun();
        IQueryable<User> ListUsers();
        IQueryable<User> OrangeZone();
        IQueryable<User> BlackZone();
        string AddUser(User user);
    }
}
