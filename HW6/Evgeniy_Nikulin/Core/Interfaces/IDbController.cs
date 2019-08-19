namespace Core.Interfaces
{
    using System.Collections.Generic;
    using Core.Model;
    public interface IDbController
    {
        int GetTraderCount();

        PersonalCard GetTraderCard(int TraderID);

        int GetSharesCount(int TraderID);

        Share GetShare(int TraderID, int index);

        void SafeTransaction(Transaktion trn);

        void AddTrader(string Name, string Surname, string Phone, decimal Money, string Reputation);

        void AddShareToTrader(string shareName, decimal price, int quantity, int OwnerId);

        void ChangeShare(int shareId, string newName, decimal newPrice, int OwnerId);

        List<Trader> GetTradersList();

        List<Share> GetShareList(int OnerId);
    }
}