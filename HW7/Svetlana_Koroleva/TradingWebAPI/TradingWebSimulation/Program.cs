using Microsoft.EntityFrameworkCore;
using SharedContext;
using SharedContext.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core;
using Trading.Core.IServices;
using Trading.Core.Services;

namespace TradingWebSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = new Logger(log4net.LogManager.GetLogger("Logger"));

            var optionsBuilder = new DbContextOptionsBuilder<ExchangeContext>();
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=StockExchangeW;Trusted_Connection=True;");
             IUnitOfWork unitOfWork = new UnitOfWork(new ExchangeContext(optionsBuilder.Options));

           /* ITableRepository<Client> clientTableRepository = new ClientTableRepository<Client>(db);
            ITableRepository<ClientStock> clientStockTableRepository = new ClientStockTableRepository<ClientStock>(db);
            ITableRepository<Issuer> issuerTableRepository = new IssuerTableRepository<Issuer>(db);
            ITableRepository<Order> orderTableRepository = new OrderTableRepository<Order>(db);
            ITableRepository<PriceHistory> priceHistoryTableRepository = new PriceHistoryTableRepository<PriceHistory>(db);
            ITableRepository<Stock> stockTableRepository = new StockTableRepository<Stock>(db);
            ITableRepository<TransactionHistory> transactionHistoryTableRepository = new TransactionHistoryTableRepository<TransactionHistory>(db);*/

            IClientService clientService = new ClientService(unitOfWork);
            IClientStockService clientStockService = new ClientStockService(unitOfWork);
            IOrderService orderService = new OrderService(unitOfWork);
            IStockService stockService = new StockService(unitOfWork);
            ITransactionHistoryService transactionHistoryService = new TransactionHistoryService(unitOfWork);



            StockExchange stockExchange = new StockExchange(
                  unitOfWork,
                  clientService,
                  clientStockService,
                  orderService,
                  stockService,
                  transactionHistoryService,
                  logger);


           
                logger.Info("Trading is started");
                stockExchange.RunTraiding();
                logger.Info("Trading is finished");

            

        }
    }
}
