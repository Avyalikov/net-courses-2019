namespace TradingApp
{
    using System;
    using System.Linq;
    using TradingApp.Data;
    using TradingApp.Data.Models;
    using TradingApp.Logic;

    public class TradeLogic : ITradeLogic
    {
        private readonly IAppDbContext dbProvider;

        public TradeLogic(IAppDbContext dbProvider)
        {
            this.dbProvider = dbProvider;
        }

        public string AddUser(User user)
        {
            return new UserLogic(dbProvider).AddUser(user);
        }

        public IQueryable<User> BlackZone()
        {
            return new UserLogic(dbProvider).BlackZone();
        }

        public string ChangeStockPrice(int id, decimal newPrice)
        {
            try
            {
                return new ShareLogic(dbProvider).ChangeStockPrice(id, newPrice);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IQueryable<Share> ListStocks()
        {
            return new ShareLogic(dbProvider).ListStocks();
        }

        public IQueryable<User> ListUsers()
        {
            return new UserLogic(dbProvider).ListUsers();
        }

        public IQueryable<User> OrangeZone()
        {
            return new UserLogic(dbProvider).OrangeZone();
        }

        public string TransactionRun()
        {
            try
            {
                return new Transaction(dbProvider).TransactionRun();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
