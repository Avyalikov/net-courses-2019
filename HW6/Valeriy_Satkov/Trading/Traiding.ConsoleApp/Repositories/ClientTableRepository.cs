namespace Traiding.ConsoleApp.Repositories
{
    using System.Linq;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class ClientTableRepository : IClientTableRepository
    {
        private readonly StockExchangeDBContext dBContext;

        public ClientTableRepository(StockExchangeDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Add(ClientEntity entity)
        {
            this.dBContext.Clients.Add(entity);
        }

        public bool Contains(ClientEntity entity)
        {
            // return this.dBContext.Clients.Contains(entity);

            return this.dBContext.Clients.Any(c =>
            c.LastName == entity.LastName 
            && c.FirstName == entity.FirstName 
            && c.PhoneNumber == entity.PhoneNumber);
        }

        public bool ContainsById(int entityId)
        {
            return this.dBContext.Clients.Any(n => n.Id == entityId);
        }

        public ClientEntity Get(int clientId)
        {
            return this.dBContext.Clients.First(n => n.Id == clientId); // it will fall here if we can't find
        }

        public int GetClientsCount()
        {
            return 10; // need return count of Clients
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
