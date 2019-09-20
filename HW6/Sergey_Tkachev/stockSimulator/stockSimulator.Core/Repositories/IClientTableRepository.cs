namespace stockSimulator.Core.Repositories
{
    using stockSimulator.Core.Models;
    using System.Linq;

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
