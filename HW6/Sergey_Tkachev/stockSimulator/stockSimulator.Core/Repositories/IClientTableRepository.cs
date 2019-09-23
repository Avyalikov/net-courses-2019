namespace stockSimulator.Core.Repositories
{
    using System.Linq;
    using stockSimulator.Core.Models;

    public interface IClientTableRepository
    {
        void Add(ClientEntity entity);
        void SaveChanges();
        bool Contains(ClientEntity entityToCheck);
        ClientEntity Get(int clientId);
        bool ContainsById(int clientId);
        void Update(int clientId, ClientEntity entityToEdit);
        decimal GetBalance(int clientId);
        void UpdateBalance(int clientId, decimal newBalance);
        IQueryable<ClientEntity> GetClients();
    }
}
