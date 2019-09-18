using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.AutoTrade.DbInit;
using TradeSimulator.AutoTrade.Repositories;
using TradeSimulator.Core;
using TradeSimulator.Core.Services;

namespace TradeSimulator.AutoTrade
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger(log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));
            TradeSimDbContext database = new TradeSimDbContext();

            //repo
            AccountTableRepository accountTableRepository = new AccountTableRepository(database);
            ClientsTableRepository clientsTableRepository = new ClientsTableRepository(database);
            StockPriceTableRepository stockPriceTableRepository = new StockPriceTableRepository(database);
            HistoryTableRepository historyTableRepository = new HistoryTableRepository(database);
            StockOfClientTableRepository stockOfClientTableRepository = new StockOfClientTableRepository(database);

            ClientsService clientService = new ClientsService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, stockOfClientTableRepository, logger);
            TradingService tradingService = new TradingService(accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository , logger);
            ShowDbInfoService showDbInfoService = new ShowDbInfoService(clientsTableRepository, accountTableRepository, stockPriceTableRepository, historyTableRepository, stockOfClientTableRepository);

            AutoTrading autoTrading = new AutoTrading(tradingService, showDbInfoService);

            using (database)
            {
                autoTrading.Run();
            };
        }
    }
}
