namespace Core.Interfaces
{
    using System.Collections.Generic;
    using Core.Model;

    public interface ITradersManager
    {
        List<Trader> BlackList { get; }
        List<Trader> OrangeList { get; }
        List<Trader> TradersList { get; }
        List<Trader> WhiteList { get; }

        string AddShare(string shareName, string price, string quantity, string OwnerId);
        string AddTrader(string Name, string Surname, string Phone, string money);
        string ChangeShare(string shareId, string newName, string newPrice, string OwnerId);
        List<Share> GetShareList(string ownerId);
    }
}