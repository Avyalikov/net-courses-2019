using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSoftware.Core.Models;

namespace TradingSoftware.Core.Repositories
{
    public interface IClientRepository
    {
        bool Insert(Client client);
        string GetClientName(int ClientID);
        int GetClientID(string ClientName);
        int GetNumberOfClients();
        decimal GetClientBalance(int ClientID);
        IEnumerable<Client> GetAllClients();
        bool IsClientExist(int ClientID);
        bool IsClientExist(string ClientName);
        bool ChangeBalance(int ClientID, decimal accountGain);
    }
}
