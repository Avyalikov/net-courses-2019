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

        public IEnumerable<TraderEntityDB> GetTradersWithNegativeBalance()
        {
            return this.dbContext.Traders.Where(t => t.Balance < 0);
        }

        public IEnumerable<TraderEntityDB> GetTradersWithZeroBalance()
        {
            return this.dbContext.Traders.Where(t => t.Balance == 0);
        }
    }
}
