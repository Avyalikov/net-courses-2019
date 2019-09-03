namespace TradingApp.Core.Interfaces
{
    using System.Collections.Generic;
    using Models;
    using DTO;
    public interface IClientService
    {
        void RegisterClient(ClientRegistrationData clientData);
        void ChangeBalance(int clientID, decimal cost);
        string GetClientName(int clientID);
        int GetClientIDByName(string name);
        IEnumerable<Client> GetAllClients();
        void UpdateClient(Client client);
        void RemoveClient(int clientID);
        decimal GetClientBalance(int clientID);
        ClientBalanceStatus GetClientBalanceStatus(int clientID);
    }
}
