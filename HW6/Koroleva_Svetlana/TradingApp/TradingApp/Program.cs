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
            ITableRepository clientTableRepository = new ClientTableRepository(db);
            ITableRepository clientStockTableRepository = new ClientStockTableRepository(db);
            ITableRepository issuerTableRepository = new IssuerTableRepository(db);
            ITableRepository orderTableRepository = new OrderTableRepository(db);
            ITableRepository priceHistoryTableRepository = new PriceHistoryTableRepository(db);
            ITableRepository stockTableRepository = new StockTableRepository(db);
            ITableRepository transactionHistoryTableRepository = new TransactionHistoryTableRepository(db);

            StockExchange stockExchange = new StockExchange(db,
               clientTableRepository,
              clientStockTableRepository,
              issuerTableRepository,
              orderTableRepository,
              priceHistoryTableRepository,
              stockTableRepository,
              transactionHistoryTableRepository);


            using (db)
            {
                logger.Info("Trading is started");
                stockExchange.RunTraiding();
                logger.Info("Trading is finished");

            };


        }
    }
}

