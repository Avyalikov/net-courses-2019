namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;

    public interface IShareManager
    {
        void AddShare(string shareName, decimal sharePrice);

        void AddShare(Share share);

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