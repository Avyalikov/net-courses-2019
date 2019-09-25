namespace TradingApp.Core.ServicesInterfaces
{
    using System.Collections.Generic;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;


    public interface IShareServices
    {
        int AddNewShare(ShareInfo args);
        void ChangeSharePrice(int id, decimal newPrice);
        ICollection<ShareEntity> GetAllShares();
    }
}