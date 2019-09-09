namespace TradingApp.Core.Repos
{
    using Models;
    using System.Collections.Generic;

    public interface ISharesRepository : IDBComm
    {
        void Insert(Share share);
        Share GetShareByID(int shareID);
        IEnumerable<Share> GetAllShares();
        bool DoesShareExists(int shareID);
        bool DoesShareExists(string shareType);
        string GetShareType(int shareID);
        int GetShareIDByType(string shareType);
        decimal GetSharePrice(int shareID);
    }
}
