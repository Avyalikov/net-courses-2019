using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.WebApp.Repositories
{
    class ClientRepository : DBTable, IClientRepository
    {
        private readonly TradingDBContext dbContext;
        public ClientRepository(TradingDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ClientEntity client)
        {
            dbContext.Clients.Add(client);
        }

        public IEnumerable<ClientEntity> LoadAllClients()
        {
            return dbContext.Clients;
        }

        public ClientEntity LoadClientByID(int ID)
        {
            return dbContext.Clients.Where(x => x.ClientID == ID).FirstOrDefault();
        }

        public void Remove(int ID)
        {
            dbContext.Clients.Remove(LoadClientByID(ID));
        }

        public void Update(ClientEntity client)
        {
            var clientOld = LoadClientByID(client.ClientID);
            dbContext.Entry(clientOld).CurrentValues.SetValues(client);
        }
    }
}
