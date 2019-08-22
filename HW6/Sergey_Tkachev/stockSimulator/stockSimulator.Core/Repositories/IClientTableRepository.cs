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
        bool Contains(ClientEntity entityToAdd);
        ClientEntity Get(int clientId);
        bool ContainsById(int clientId);
        void Update(int clientId, ClientEntity entityToEdit);
        decimal GetBalance(int clientId);
        void UpdateBalance(int clientId, decimal newBalance);
    }
}
