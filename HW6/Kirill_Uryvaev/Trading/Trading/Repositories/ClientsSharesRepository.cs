using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.DataTransferObjects;
using Trading.Core.Repositories;

namespace Trading.ConsoleApp.Repositories
{
    class ClientsSharesRepository: DBTable, IClientsSharesRepository
    {
        private readonly TradingDBContext dbContext;
        public ClientsSharesRepository(TradingDBContext dbContext): base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(ClientsSharesEntity clientsShares)
        {
            dbContext.ClientsShares.Add(clientsShares);
        }

        public ClientsSharesEntity LoadClientsSharesByID(ClientsSharesInfo clientsSharesInfo)
        {
            return dbContext.ClientsShares.Where(x => x.ClientID == clientsSharesInfo.ClientID && x.ShareID == clientsSharesInfo.ShareID).FirstOrDefault();
        }
    }
}
