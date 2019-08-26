using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Core.Models;

namespace TradingApp.Core.EntityInitializers
{
    public class BalanceContextInitializer
    {
        public List<BalanceEntity> ContextInitializer(List<UserEntity> users, List<StockEntity> stocks)
        {
            Random r = new Random();
            List<BalanceEntity> Balances = new List<BalanceEntity>();
            int stockID = r.Next(1, 10);
            int stockAmount = r.Next(1, 5);
            StockEntity stock = stocks.Where(f => f.ID == stockID).First();
              
                foreach (UserEntity user in users)
            {
                BalanceEntity balance = new BalanceEntity();
                balance.UserID = user.ID;
                balance.Balance = stock.Price* stockAmount;
                balance.StockID = stock.ID;
                balance.StockAmount = stockAmount;
                balance.CreatedAt = DateTime.Now;
                Balances.Add(balance);
            }
            return Balances;

        }
    }
}
