using TradingApp.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Trading.Core.Services;
using Trading.Core.Repositories;
using TradingApp.Repositories;
using Trading.Core.DTO;
using Trading.Core.Model;

namespace TradingApp
{
    class Program
    {


        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = new Logger(log4net.LogManager.GetLogger("Logger"));
            ExchangeContext db = new ExchangeContext();
            ITableRepository<Client> clientTableRepository = new ClientTableRepository<Client>(db);
            ITableRepository<ClientStock> clientStockTableRepository = new ClientStockTableRepository<ClientStock>(db);
            ITableRepository<Issuer> issuerTableRepository = new IssuerTableRepository<Issuer>(db);
            ITableRepository<Order> orderTableRepository = new OrderTableRepository<Order>(db);
            ITableRepository<PriceHistory> priceHistoryTableRepository = new PriceHistoryTableRepository<PriceHistory>(db);
            ITableRepository<Stock> stockTableRepository = new StockTableRepository<Stock>(db);
            ITableRepository<TransactionHistory> transactionHistoryTableRepository = new TransactionHistoryTableRepository<TransactionHistory>(db);

            StockExchange stockExchange = new StockExchange(db,
               clientTableRepository,
              clientStockTableRepository,
              issuerTableRepository,
              orderTableRepository,
              priceHistoryTableRepository,
              stockTableRepository,
              transactionHistoryTableRepository);
              

           /* StockCopy stockExchange = new StockCopy(db,
               clientTableRepository,
              clientStockTableRepository,
              issuerTableRepository,
              orderTableRepository,
              priceHistoryTableRepository,
              stockTableRepository,
              transactionHistoryTableRepository);*/
              

            using (db)
            {
                logger.Info("Trading is started");
              stockExchange.RunTraiding();
                logger.Info("Trading is finished");

            };


        }
    }
}

