using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.Repositories;

namespace Trading.WebApp.Repositories
{
    class ClientSharesRepository : DBTable, IClientsSharesRepository
    {
        private readonly TradingDBContext dbContext;
        public ClientSharesRepository(TradingDBContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ClientsSharesEntity clientsShares)
        {
            dbContext.ClientsShares.Add(clientsShares);
        }

        public IEnumerable<ClientsSharesEntity> GetAllShares()
        {
            return dbContext.ClientsShares;
        }

        public ClientsSharesEntity LoadClientsSharesByID(ClientsSharesEntity clientsShares)
        {
            return dbContext.ClientsShares.Where(x => x.ClientID == clientsShares.ClientID && x.ShareID == clientsShares.ShareID).FirstOrDefault();
        }

        public void Remove(ClientsSharesEntity clientsShares)
        {
            dbContext.ClientsShares.Remove(LoadClientsSharesByID(clientsShares));
        }

        public void Update(ClientsSharesEntity clientsShares)
        {
            var shareOld = LoadClientsSharesByID(clientsShares);
            dbContext.Entry(shareOld).CurrentValues.SetValues(clientsShares);
        }

    }
}
