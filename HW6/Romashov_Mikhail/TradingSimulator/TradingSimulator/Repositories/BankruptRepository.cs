using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Repositories;


namespace TradingSimulator.Repositories
{
    class BankruptRepository : IBankruptRepository
    {
        private readonly TradingSimulatorDBContext dbContext;

        public BankruptRepository(TradingSimulatorDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<string> GetTradersWithNegativeBalance()
        {
            List<string> listItems = new List<string>();
            foreach (var item in this.dbContext.Traders.Where(t => t.Balance < 0))
            { 
                listItems.Add(string.Concat(item.Name + " " + item.Surname));
            }
            return listItems;
        }

        public List<string> GetTradersWithZeroBalance()
        {
            List<string> listItems = new List<string>();
            foreach (var item in this.dbContext.Traders.Where(t => t.Balance == 0))
            {
                listItems.Add(string.Concat(item.Name + " " + item.Surname));
            }
            return listItems;
        }
    }
}
