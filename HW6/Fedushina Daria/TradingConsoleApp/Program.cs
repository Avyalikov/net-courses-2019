using StructureMap;
using System;
using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Services;
using TradingConsoleApp.Dependencies;

namespace TradingConsoleApp
{
    class Program
    {
        static void Main(string[] args)

        {
            
            var container = new Container(new TradingAppRegistry());
            var balancesService = container.GetInstance<BalancesService>();
            var trade = container.GetInstance<Trade>();
            Random r = new Random();
            using (var dbContext = container.GetInstance<TradingAppDbContext>())
           {
                
                foreach (UserEntity user in dbContext.Users)
                {
                    balancesService.CreateBalance(new BalanceInfo()
                    {
                        UserID = user.ID,
                        Balance = 1000,
                        StockID = r.Next(1,10),
                        StockAmount = r.Next(1,5)
                    });
                }
                trade.Run();
            }
            

                
            
        }
    }
}
