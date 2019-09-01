namespace Traiding.WebAPIConsole.Models.Repositories
{
    using System.Collections.Generic;
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

        public void Deactivate(int clientId)
        {
            this.dBContext.Clients.First(n => n.Id == clientId).Status = false; // it will fall here if we can't find
        }

        public ClientEntity Get(int clientId)
        {
            return this.dBContext.Clients.First(n => n.Id == clientId); // it will fall here if we can't find
        }

        public int GetClientsCount()
        {
            var count = this.dBContext.Clients.Count();
            return count; // 10; // need return count of Clients
        }

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }

        public IEnumerable<ClientEntity> Take(int number, int rank)
        {
            var clients = this.dBContext.Clients.OrderBy(c => c.Id).Skip(number * (rank - 1)).Take(number).ToList();
            return clients; // 10; // need return count of Clients
        }

        public void Update(ClientEntity entity)
        {
            var clientFromDB = Get(entity.Id);

            clientFromDB.LastName = entity.LastName;
            clientFromDB.FirstName = entity.FirstName;
            clientFromDB.PhoneNumber = entity.PhoneNumber;
        }
    }
}
