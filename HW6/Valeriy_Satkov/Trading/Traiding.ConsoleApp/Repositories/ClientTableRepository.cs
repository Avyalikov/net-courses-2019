using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.ConsoleApp.Repositories
{
    public class ClientTableRepository : IClientTableRepository
    {
        private readonly TraidingDBContext dBContext;

        public ClientTableRepository(TraidingDBContext dBContext)
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

        public void SaveChanges()
        {
            this.dBContext.SaveChanges();
        }
    }
}
