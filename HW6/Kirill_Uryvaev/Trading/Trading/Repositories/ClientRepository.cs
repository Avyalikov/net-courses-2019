using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.ConsoleApp.Repositories
{
    class ClientRepository: DBTable, IClientRepository
    {
        private readonly TradingDBContext dbContext;
        public ClientRepository(TradingDBContext dbContext): base(dbContext)
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
    }
}
