using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
{
    class BankruptRepository : IBankruptRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public BankruptRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<TraderEntityDB> GetTradersWithNegativeBalance()
        {
            List<TraderEntityDB> listItems = new List<TraderEntityDB>();
            foreach (var item in this.dbContext.Traders.Where(t => t.Balance < 0))
            {
                listItems.Add(item);
            }
            return listItems;
        }

        public List<TraderEntityDB> GetTradersWithZeroBalance()
        {
            List<TraderEntityDB> listItems = new List<TraderEntityDB>();
            foreach (var item in this.dbContext.Traders.Where(t => t.Balance == 0))
            {
                listItems.Add(item);
            }
            return listItems;
        }
    }
}
