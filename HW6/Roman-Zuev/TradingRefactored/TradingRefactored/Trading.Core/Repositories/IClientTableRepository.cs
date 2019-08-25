using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface IClientTableRepository
    {
        void SaveChanges();
        void Add(ClientEntity entity);
        bool Contains(ClientEntity entityToAdd);
        bool ContainsById(int clientId);
        ClientEntity GetById(int clientId);
        void Change(ClientEntity changedClient);
        ICollection<ClientEntity> GetClientsInBlackZone();
        ICollection<ClientEntity> GetClientsInOrangeZone();
        int Count { get; }
        ClientEntity this[int i] { get; }
    }
}