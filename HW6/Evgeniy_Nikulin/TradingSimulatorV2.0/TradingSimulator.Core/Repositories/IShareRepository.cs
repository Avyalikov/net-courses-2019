namespace TradingSimulator.Core.Repositories
{
    using System.Collections.Generic;
    using Dto;

    public interface IShareRepository : IRepository
    {
        int GetSharesCount(int TraderID);
        List<Share> GetShareList(int OnerId);
        Share GetShare(int ShareId);
        Share GetShare(int TraderID, string shareName);
        void Push(Share share);
        void Remove(Share share);
    }
}