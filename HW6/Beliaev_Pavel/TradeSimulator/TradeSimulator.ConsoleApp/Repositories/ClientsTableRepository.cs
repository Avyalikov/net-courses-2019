using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.ConsoleApp.DbInit;
using TradeSimulator.Core.Models;
using TradeSimulator.Core.Repositories;

namespace TradeSimulator.ConsoleApp.Repositories
{
    public class ClientsTableRepository : IClientsTableRepository
    {
        private readonly TradeSimDbContext dbContext;

        public ClientsTableRepository(TradeSimDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ClientEntity entity)
        {
            dbContext.Client.Add(entity);
        }
        
        public ClientEntity GetClientById(int clientId)
        {
            return dbContext.Client.DefaultIfEmpty(null).FirstOrDefault(f => f.Id == clientId);
        }

        public ICollection<ClientEntity> GetAllClients()
        {
            return dbContext.Client.DefaultIfEmpty(null).ToList();
        }

        public ClientEntity GetClientByNameAndSurname(string Name, string Surname)
        {
            return dbContext.Client.DefaultIfEmpty(null).FirstOrDefault(f => (f.Surname == Surname) && (f.Name == Name));
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
