using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.ConsoleApp.Repositories
{
    public class AddingSharesToThUserServiceTableRepository : IAddingSharesToThUserServiceTableRepository
    {
        private readonly TradingSimulatorDbContext dbContext;

        public AddingSharesToThUserServiceTableRepository(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(AddingSharesToThUserEntity addingSharesToThUserEntity)
        {
            this.dbContext.UsersAndShares.Add(addingSharesToThUserEntity);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }
    }
}
