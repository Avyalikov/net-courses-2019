namespace TradingSimulator.Core.Repositories
{
    using System.Collections.Generic;
    using Dto;

    public interface ITraderRepository : IRepository
    {
        int GetTraderCount();
        Trader GetTrader(int TraderID);
        List<Trader> GetTradersList();
        void Push(Trader trader);
    }
}