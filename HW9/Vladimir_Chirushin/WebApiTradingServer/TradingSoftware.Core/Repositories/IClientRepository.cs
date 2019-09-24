namespace TradingSoftware.Core.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;

    public interface IClientRepository
    {
        bool Insert(Client client);

        string GetClientName(int clientID);

        int GetClientID(string clientName);

        int GetNumberOfClients();

        decimal GetClientBalance(int clientID);

        IEnumerable<Client> GetAllClients();

        bool IsClientExist(int clientID);

        bool IsClientExist(string clientName);

        bool ChangeBalance(int clientID, decimal accountGain);

        void Remove(Client client);

        void ClientUpdate(Client client);
    }
}
