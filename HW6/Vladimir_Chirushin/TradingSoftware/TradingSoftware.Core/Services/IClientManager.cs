namespace TradingSoftware.Core.Services
{
    using System.Collections.Generic;
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

        decimal GetClientBalance(int clientID);
    }
}