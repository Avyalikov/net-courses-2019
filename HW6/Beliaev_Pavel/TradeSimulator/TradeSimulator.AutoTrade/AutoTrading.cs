using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TradeSimulator.Core.Dto;
using TradeSimulator.Core.Models;
using TradeSimulator.Core.Services;

namespace TradeSimulator.AutoTrade
{
    public class AutoTrading
    {
        readonly TradingService tradingService;
        readonly ShowDbInfoService showDbInfoService;

        public AutoTrading(TradingService tradingService, ShowDbInfoService showDbInfoService)
        {
            this.tradingService = tradingService;
            this.showDbInfoService = showDbInfoService;
        }

        public void Run()
        {
            Random random = new Random();
            List<int> badIdForSeller = new List<int>();
            TransactionInfo transactionInfo = new TransactionInfo();
            var clients = this.showDbInfoService.GetAllClients();

            do
            {
                transactionInfo.BuyerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;
                badIdForSeller.Add(transactionInfo.BuyerId);
                transactionInfo.SellerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;

                if (transactionInfo.BuyerId == transactionInfo.SellerId)
                {
                    do
                    {
                        transactionInfo.SellerId = clients.ElementAt(random.Next(0, clients.Count - 1)).Id;
                        if ((showDbInfoService.GetAccountByClientId(transactionInfo.SellerId) == null) && (showDbInfoService.GetAccountByClientId(transactionInfo.SellerId).Stocks.Count == 0))
                        {
                            badIdForSeller.Add(transactionInfo.SellerId);
                        }
                    } while (badIdForSeller.Contains(transactionInfo.SellerId));
                }

                var sellersAccount = this.showDbInfoService.GetAccountByClientId(transactionInfo.SellerId);

                var StockForSell = showDbInfoService.GetStocksOfClientByAccountId(sellersAccount.AccountId).ElementAt(random.Next(0, sellersAccount.Stocks.Count - 1));

                transactionInfo.TypeOfStock = StockForSell.TypeOfStocks;
                transactionInfo.QuantityOfStocks = random.Next(1, StockForSell.quantityOfStocks);

                this.tradingService.MakeATrade(transactionInfo);
                                
                Thread.Sleep(10000);
            } while (!this.showDbInfoService.GetClientsInBlackZone().Any());
        }

        public void RunTest()
        {
            this.showDbInfoService.GetAllClients().ToList().ForEach(client => Console.WriteLine($"Id:{client.Id} Name:{client.Name} Surname:{client.Surname} Phone:{client.PhoneNumber}"));
            Console.ReadLine();
        }
    }
}
