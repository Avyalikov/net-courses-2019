using System.Collections.Generic;
using Trading.Core.Models;

namespace Trading.Core.Repositories
{
    public interface IClientTableRepository
    {
        ClientEntity this[int i] { get; }

        int Count { get; }

        void Add(ClientEntity entity);
        void Change(ClientEntity changedClient);
        bool Contains(ClientEntity entity);
        bool ContainsById(int clientId);
        ClientEntity GetById(int clientId);
        ICollection<ClientEntity> GetAllInBlackZone();
        ICollection<ClientEntity> GetAllInOrangeZone();
        ICollection<ClientEntity> GetTop(int clientsAmount);
        void Remove(ClientEntity client);
        void SaveChanges();
    }
}