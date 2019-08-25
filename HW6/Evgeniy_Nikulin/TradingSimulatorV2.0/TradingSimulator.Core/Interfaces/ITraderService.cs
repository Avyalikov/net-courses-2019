namespace TradingSimulator.Core.Interfaces
{
    using Dto;
    using System.Collections.Generic;

    public interface ITraderService
    {
        List<Trader> TradersList { get; }
        List<Trader> WhiteList { get; }
        List<Trader> OrangeList { get; }
        List<Trader> BlackList { get; }

        string AddTrader(string Name, string Surname, string Phone, string money);
    }
}