using stockSimulator.Core.DTO;
using stockSimulator.Core.Models;
using stockSimulator.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace stockSimulator.WevServer.Repositories
{
    class ClientTableRepository : IClientTableRepository
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

        public int GetClientId(ClientEntity entityToCheck)
        {
            int clientID;

            clientID = this.dbContext.Clients
               .Where(c => c.Name == entityToCheck.Name
               && c.Surname == entityToCheck.Surname
               && c.PhoneNumber == entityToCheck.PhoneNumber
               && c.AccountBalance == entityToCheck.AccountBalance)
               .Select(c => c.ID)
               .FirstOrDefault();

            return clientID;
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

        public string Remove(int clientId)
        {
            var client = this.dbContext.Clients.FirstOrDefault(c => c.ID == clientId);
            if (client != null)
            {
                this.dbContext.Clients.Remove(client);
                this.dbContext.SaveChanges();
                return "Client was deleted.";
            }
            return "Client wasn't found.";
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public string Update(UpdateClientInfo updateInfo)
        {
            int clientId = updateInfo.ID;
            
            var clientToUpdate = this.dbContext.Clients.FirstOrDefault(c => c.ID == clientId);
            if (clientToUpdate != null)
            {
                clientToUpdate.Name = updateInfo.Name;
                clientToUpdate.Surname = updateInfo.Surname;
                clientToUpdate.PhoneNumber = updateInfo.PhoneNumber;
                clientToUpdate.AccountBalance = updateInfo.AccountBalance;
                SaveChanges();
                return "Client data updated.";
            }
            return "Client wasn't found.";
        }

        public void UpdateBalance(int clientId, decimal newBalance)
        {
            var clientToUpdate = this.dbContext.Clients.First(c => c.ID == clientId);
            clientToUpdate.AccountBalance = newBalance;
        }
    }
}
