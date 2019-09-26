namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Dto;
    using TradingSoftware.Core.Models;

    public interface IClientManager
    {
        void AddClient(string name, string phoneNumber, decimal balance);

        void AddClient(Client client);

        string GetClientName(int clientID);

        int GetClientID(string clientName);

        bool IsClientExist(int clientID);

        bool IsClientExist(string clientName);

        IEnumerable<Client> GetAllClients();

        void ChangeBalance(int clientID, decimal accountGain);

        int GetNumberOfClients();

        void ClientUpdate(Client client);

        decimal GetClientBalance(int clientID);

        ClientBalanceStatus GetClientBalanceStatus(int clientID);

        void DeleteClient(Client client);
    }
}