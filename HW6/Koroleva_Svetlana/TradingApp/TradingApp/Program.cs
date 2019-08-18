using TradingApp.DAL;
using TradingApp.Model;
using TradingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TradingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = new Logger(log4net.LogManager.GetLogger("Logger"));

            using (ExchangeContext db = new ExchangeContext())
            {
               
                

                IClientStocksModifier clientStocksModifier = new ClientStockModifier(db);
                IOrderModifier orderModifier = new OrderModifier(db, clientStocksModifier);
                IPriceModifier priceModifier = new PriceModifier(db, orderModifier);
                ITransactionModifier transaction = new TransactionModifier(db);
               
                StockExchange stockExchange = new StockExchange(priceModifier, orderModifier, transaction, clientStocksModifier);
                logger.Info("Trading is started");
                stockExchange.RunTraiding();
                logger.Info("Trading is finished");
               
            }
        }
    }
}
