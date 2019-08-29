namespace TradingApp.Core.Repos
{
    using Models;
    using System.Collections.Generic;
    public interface IClientRepository : IDBComm
    {
        void Insert(Clients client);
        Clients GetClientByID(int clientID);
        int GetClientID(string name);
        string GetClientName(int ClientID);
        bool DoesClientExists(int clientID);
        bool DoesClientExists(string name);
        decimal GetClientBalance(int clientID);
        void ChangeBalance(int clientID, decimal money);
        IEnumerable<Clients> GetAllClients();
    }
}
