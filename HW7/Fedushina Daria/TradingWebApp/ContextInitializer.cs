using System.Collections.Generic;
using System.Data.Entity;
using TradingApp.Core.Models;
using TradingApp.Core.EntityInitializers;

namespace TradingConsoleApp
{
    public class ContextInitializer : DropCreateDatabaseAlways<TradingAppDbContext>
    {
        protected override void Seed(TradingAppDbContext context)
        {
            UserContextInitializer UserInit = new UserContextInitializer();
            StockContextInitializer StockInit = new StockContextInitializer();
            List<UserEntity> Users =  UserInit.ContextInitializer();
            List<StockEntity> Stocks = StockInit.ContextInitializer();
            foreach (UserEntity item in Users)
            {
                context.Users.Add(item);
            }
            foreach (StockEntity item in Stocks)
            {
                context.Stocks.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
           
        }
    }
}
