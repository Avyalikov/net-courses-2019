namespace TradingSoftware.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;

    public interface ISharesRepository
    {
        bool Insert(Share share);

        string GetShareType(int shareID);

        int GetShareID(string shareType);

        int GetNumberOfShares();

        decimal GetSharePrice(int shareID);

        IEnumerable<Share> GetAllShares();

        bool IsShareExist(int shareID);

        bool IsShareExist(string shareType);

        void ChangeSharePrice(int shareID, decimal price);
    }
}