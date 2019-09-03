namespace TradingApp.Core.Interfaces
{
    using Models;
    using DTO;
    using System.Collections.Generic;
    public interface ISharesService
    {
        IEnumerable<Share> GetAllShares();
        void RegisterShare(ShareRegistrationData registrationData);
        int GetShareIDByType(string shareType);
        string GetShareType(int shareID);
        decimal GetSharePrice(int shareID);
    }
}
