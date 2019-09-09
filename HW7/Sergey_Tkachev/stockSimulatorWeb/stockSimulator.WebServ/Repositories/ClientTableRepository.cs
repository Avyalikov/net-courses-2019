using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace stockSimulator.WebServ.Repositories
{
    public class ClientTableRepository : IClientTableRepository
    {
        private readonly StockSimulatorDbContext dbContext;

        public ClientTableRepository(StockSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ClientEntity entity)
        {
            this.dbContext.Clients.Add(entity);
        }

        public bool Contains(ClientEntity entityToCheck)
        {
            return this.dbContext.Clients
               .Any(c => c.Name == entityToCheck.Name
               && c.Surname == entityToCheck.Surname
               && c.PhoneNumber == entityToCheck.PhoneNumber
               && c.AccountBalance == entityToCheck.AccountBalance);
        }

        public bool ContainsById(int clientId)
        {
            return this.dbContext.Clients
               .Any(c => c.ID == clientId);
        }

        public ClientEntity Get(int clientId)
        {
            return this.dbContext.Clients
                .Where(c => c.ID == clientId)
                .FirstOrDefault();
        }

        public decimal GetBalance(int clientId)
        {
            return this.dbContext.Clients
                .Where(c => c.ID == clientId)
                .Select(c => c.AccountBalance)
                .FirstOrDefault();
        }

        public IEnumerable<ClientEntity> GetClients()
        {
            var retListOfClients = this.dbContext.Clients.ToList();

            return retListOfClients;
        }

        public IEnumerable<ClientEntity> GetClients(int startPoint, int amountOfClients)
        {
            var retListOfClients = this.dbContext.Clients.OrderBy(c => c.ID).Skip(startPoint).Take(amountOfClients);

            return retListOfClients;
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Update(int clientId, ClientEntity entityToEdit)
        {
            var clientToUpdate = this.dbContext.Clients.First(c => c.ID == clientId);
            clientToUpdate = entityToEdit;
        }

        public void UpdateBalance(int clientId, decimal newBalance)
        {
            var clientToUpdate = this.dbContext.Clients.First(c => c.ID == clientId);
            clientToUpdate.AccountBalance = newBalance;
        }
    }
}
