using System;
using System.Collections.Generic;
using System.Linq;
using TradingSimulator.Core.Dto;
using TradingSimulator.ConsoleApp.Interfaces;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    class TradingData
    {

        private readonly TradersService traders;
        private readonly StockService stockService;
        private readonly TraderStocksService traderStocks;
        private readonly BankruptService bankruptService;
        private readonly ILogger logger;
        private readonly IValidator validator;
        public TradingData(TradersService traders, StockService stockService, TraderStocksService traderStocks, BankruptService bankruptService, ILogger logger, IValidator validator)
        {
            this.traders = traders;
            this.stockService = stockService;
            this.traderStocks = traderStocks;
            this.bankruptService = bankruptService;
            this.logger = logger;
            this.validator = validator;
        }

        public void Run()
        {
            logger.InitLogger();
            logger.Info("Start program");
            while (true)
            {
                Console.WriteLine(@"
-----------------
Use one of the option:
    1-Registration new trader
    2-Add stock to trader
    3-Get traders from orange zone
    4-Get traders from black zone
-----------------");
                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        this.TraderRegistartion();
                        break;
                    case "2":
                        this.ModificationTradersStock();
                        break;
                    case "3":
                        this.GetOrangeZone();
                        break;
                    case "4":
                        this.GetBlackZone();
                        break;
                    default:
                        break;
                }
            }
        }
        private void TraderRegistartion()
        {
            logger.Info("Try to registration new trader");
            Console.WriteLine("Please input first name:");
            string firstName = Console.ReadLine();
            
            Console.WriteLine("Please input last name:");
            string lastName = Console.ReadLine();
            
            Console.WriteLine("Please input phone:");
            string phone = Console.ReadLine();

            var newTrader = new TraderInfo()
            {
                Name = firstName,
                Surname = lastName,
                PhoneNumber = phone,
                Balance = 1000M
            };

            if (!validator.TraderInfoValidate(newTrader))
            {
                logger.Info($"Registration trader name = {newTrader.Name} surname = {newTrader.Surname} phone = {newTrader.PhoneNumber} cancel");
                return;
            }

            try
            {
                traders.RegisterNewTrader(newTrader);
                logger.Info("Registration new trader was succesfully");
                logger.Info($"New trader name = {firstName}, surname = {lastName}");
                Console.WriteLine("Registration was succesfully");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
                logger.Info($"New trader name = {firstName}, surname = {lastName} wasnt registration");
                logger.Error(e);
            }
        }

        private void ModificationTradersStock()
        {
            logger.Info("Adding stock to trader");
            Console.WriteLine("Please input traders name:");
            string traderName = Console.ReadLine();
            Console.WriteLine("Please input stock name:");
            string stockName = Console.ReadLine();
            
            if (!validator.StockToTraderValidate(traderName, stockName))
            {
                Console.WriteLine($"Can`t add stock {stockName} to trader {traderName}");
                logger.Info($"Can`t add stock {stockName} to trader {traderName}");
                return;
            }

            var trader = traders.GetTraderByName(traderName);
            var stock = stockService.GetStockByName(stockName);
          
            Console.WriteLine("Please input count of stocks:");
            string countStock = Console.ReadLine();

            bool validCount = Int32.TryParse(countStock, out int count);

            if (!validCount)
            {
                Console.WriteLine("Wrong count of stock. Operation cancel.");
                return;
            }

            logger.Info($"Try to add stock = {stock.Name} to trader {trader.Name}");
            try
            {
                TraderInfo traderInfo = new TraderInfo
                {
                    Id = trader.Id,
                    Name = trader.Name,
                };
                StockInfo stockInfo = new StockInfo
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    Count = count,
                    PricePerItem = stock.PricePerItem

                };

                var id = traderStocks.AddNewStockToTrader(traderInfo, stockInfo);
                Console.WriteLine("Stock added to trader succesfully");
                logger.Info($"Stock {stockInfo.Name} added to trader {traderInfo.Name} succesfully");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
                logger.Info("Stock has not been added");
                logger.Error(e);
            }
        }

        private void GetOrangeZone()
        {
            List<string> tradersWithZeroBalance = new List<string>();
            tradersWithZeroBalance = this.bankruptService.GetListTradersFromOrangeZone();

            if (tradersWithZeroBalance.Count() == 0)
            {
                Console.WriteLine("Traders with zero balance not found.");
                return;
            }
            Console.WriteLine("Traders with zero balance:");
            tradersWithZeroBalance.ForEach(t => Console.WriteLine(t));
        }
        private void GetBlackZone()
        {
            List<string> tradersWithNegativeBalance = new List<string>();
            tradersWithNegativeBalance = this.bankruptService.GetListTradersFromBlackZone();

            if (tradersWithNegativeBalance.Count() == 0)
            {
                Console.WriteLine("Traders with negative balance not found.");
                return;
            }
            Console.WriteLine("Traders with negative balance:");
            tradersWithNegativeBalance.ForEach(t => Console.WriteLine(t));
        }
    }
}
