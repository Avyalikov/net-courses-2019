namespace TradingData
{
    using System.Linq;
    using TradingData.Models;

    public interface IDbProvider
    {
        IQueryable ListUsers();
        IQueryable ListStocks();
        IQueryable OrangeZone();   
        IQueryable BlackZone();
        void AddUser(User user);
        void ChangeStockPrice(int idStock, decimal newPrice);
        bool SelectStockId(int id);
        User ChooseUser(int LastUserId = 0);
        void Transaction(TransactionStory transaction);
    }
}
