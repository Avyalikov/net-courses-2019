namespace Traiding.Core.Repositories
{
    using System.Collections.Generic;
    using Traiding.Core.Models;

    public interface IClientTableRepository
    {
        bool Contains(ClientEntity entity);
        bool ContainsById(int entityId);
        void Add(ClientEntity entity);
        void Update(ClientEntity entity);
        void SaveChanges();
        ClientEntity Get(int clientId);
        int GetClientsCount();
        void Deactivate(int clientId);
        IEnumerable<ClientEntity> Take(int number, int rank);
    }
}
