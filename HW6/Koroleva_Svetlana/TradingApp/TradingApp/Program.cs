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

            IDTOMethodsforPriceHistory dTOMethodsforPriceHistory = new DTOMethodsforPriceHistory(db);
          
            ITableRepository<Client> clientTableRepository = new ClientTableRepository<Client>(db);
            ITableRepository<ClientStock> clientStockTableRepository = new ClientStockTableRepository<ClientStock>(db);
            ITableRepository<Issuer> issuerTableRepository = new IssuerTableRepository<Issuer>(db);
            ITableRepository<Order> orderTableRepository = new OrderTableRepository<Order>(db);
            ITableRepository<PriceHistory> priceHistoryTableRepository = new PriceHistoryTableRepository<PriceHistory>(db);
            ITableRepository<Stock> stockTableRepository = new StockTableRepository<Stock>(db);
            ITableRepository<TransactionHistory> transactionHistoryTableRepository = new TransactionHistoryTableRepository<TransactionHistory>(db);

            ClientService clientService = new ClientService(clientTableRepository);
            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            OrderService orderService = new OrderService(orderTableRepository);
            PriceHistoryService priceHistoryService = new PriceHistoryService(priceHistoryTableRepository, dTOMethodsforPriceHistory);
            TransactionHistoryService transactionHistoryService = new TransactionHistoryService(transactionHistoryTableRepository);

            

            StockExchange stockExchange = new StockExchange(
                  db,
                  clientTableRepository,
                  clientStockTableRepository,
                  issuerTableRepository,
                  orderTableRepository,
                  priceHistoryTableRepository,
                  stockTableRepository,
                  transactionHistoryTableRepository,
                  clientService,
                  clientStockService,
                  orderService,
                  priceHistoryService,
                  transactionHistoryService,
                  logger);


            using (db)
            {
                logger.Info("Trading is started");
                stockExchange.RunTraiding();
                logger.Info("Trading is finished");

            };


        }
    }
}

