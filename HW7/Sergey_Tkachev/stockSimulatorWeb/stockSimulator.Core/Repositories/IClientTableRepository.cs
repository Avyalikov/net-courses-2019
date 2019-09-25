using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace stockSimulator.Core.Repositories
{
    public interface IClientTableRepository
    {
        void Add(ClientEntity entity);
        void SaveChanges();
        bool Contains(ClientEntity entityToCheck);
        ClientEntity Get(int clientId);
        int GetClientId(ClientEntity entityToCheck);
        bool ContainsById(int clientId);
        string Update(UpdateClientInfo updateInfo);
        decimal GetBalance(int clientId);
        void UpdateBalance(int clientId, decimal newBalance);
        IEnumerable<ClientEntity> GetClients();
        IEnumerable<ClientEntity> GetClients(int startPoint, int amountOfSelection);
        string Remove(int clientId);
    }
}
