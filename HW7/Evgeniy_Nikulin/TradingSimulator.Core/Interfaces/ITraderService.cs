namespace TradingSimulator.Core.Interfaces
{
    using Dto;
    using System.Collections.Generic;

    public interface ITraderService
    {
        List<Trader> TradersList { get; }
        List<Trader> GreenList { get; }
        List<Trader> OrangeList { get; }
        List<Trader> BlackList { get; }

        List<Trader> GetTradersPerPage(int top, int page);
        int GetTraderCount();
        List<Share> GetShareList(int traderId);
        string GetTraderStatus(int TraderId);
        string AddTrader(string Name, string Surname, string Phone, string money);
        string ChangeTrader(int traderId, string newName, string newSurname, string newPhone, string newMoney);
        void Remove(int traderId);
    }
}