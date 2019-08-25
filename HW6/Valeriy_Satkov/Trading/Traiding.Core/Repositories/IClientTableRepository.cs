namespace Traiding.Core.Repositories
{
    using Traiding.Core.Models;

    public interface IClientTableRepository
    {
        bool Contains(ClientEntity entity);
        bool ContainsById(int entityId);
        void Add(ClientEntity entity);
        void SaveChanges();
        ClientEntity Get(int clientId);
        int GetClientsCount();
    }
}
