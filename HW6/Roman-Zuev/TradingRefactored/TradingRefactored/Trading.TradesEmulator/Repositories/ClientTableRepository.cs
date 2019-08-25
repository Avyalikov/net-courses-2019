using System;
using System.Collections.Generic;
using System.Data.Entity;
using Trading.Core.Models;
using Trading.Core.Repositories;
using Trading.TradesEmulator.Models;
using System.Linq;
using System.Linq.Expressions;

namespace Trading.TradesEmulator.Repositories
{
    public class ClientTableRepository : IClientTableRepository
    {
        private readonly TradesEmulatorDbContext dbContext;

        public ClientTableRepository(TradesEmulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ClientEntity this[int i] => this.dbContext.Clients.ToList()[i];

        public int Count => this.dbContext.Clients.ToList().Count;

        public void Add(ClientEntity entity)
        {
            this.dbContext.Clients.Add(entity);
        }

        public void Change(ClientEntity changedClient)
        {
            this.dbContext.Entry(changedClient).State = EntityState.Modified;
        }

        public bool Contains(ClientEntity entity)
        {
            return dbContext.Clients.Any(c=>
            c.Name == entity.Name
            && c.Phone == entity.Phone);
        }

        public bool ContainsById(int clientId)
        {
            return this.dbContext.Clients.Any(c => c.Id == clientId);
        }

        public ClientEntity GetById(int clientId)
        {
            return this.dbContext.Clients.First(c => c.Id == clientId);
        }

        public ICollection<ClientEntity> GetClientsInBlackZone()
        {
            return this.dbContext.Clients.Where(c => c.Balance < 0).ToList();
        }

        public ICollection<ClientEntity> GetClientsInOrangeZone()
        {
            return this.dbContext.Clients.Where(c => c.Balance == 0).ToList();
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}