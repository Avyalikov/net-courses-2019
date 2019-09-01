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
        ICollection<ShareEntity> GetUsersShares(int userId);
        void Remove(int id);
        void Update(int id, ShareInfo args);
        ShareEntity GetShareById(int id);
    }
}