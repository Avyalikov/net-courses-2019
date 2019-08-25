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
        public TradingData(TradersService traders, StockService stockService, TraderStocksService traderStocks, BankruptService bankruptService, ILogger logger)
        {
            this.traders = traders;
            this.stockService = stockService;
            this.traderStocks = traderStocks;
            this.bankruptService = bankruptService;
            this.logger = logger;
        }

        public void Run()
        {
            logger.InitLogger();
            logger.Info("Start trading");
            while (true)
            {
                Console.WriteLine(@"Use one of the option:
    1-Registration ew trader
    2-Add stock to trader
    3-Get traders from orange zone
    4-Get traders from black zone");
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
            logger.Info("Registration new trader");
            Console.WriteLine("Please input first name:");
            string firstName = Console.ReadLine();
            bool validFirstName = firstName.All(c => char.IsLetter(c));
            if (!validFirstName)
            {
                Console.WriteLine("Wrong first name. Operation cancel.");
                return;
            }

            Console.WriteLine("Please input last name:");
            string lastName = Console.ReadLine();
            bool validLastName = lastName.All(c => char.IsLetter(c));
            if (!validLastName)
            {
                Console.WriteLine("Wrong last name. Operation cancel.");
                return;
            }

            Console.WriteLine("Please input phone:");
            string phone = Console.ReadLine();
            bool validPhone = phone.All(c => char.IsDigit(c)) && phone.Length < 9;
            if (!validPhone)
            {
                Console.WriteLine("Wrong phone. Operation cancel.");
                return;
            }

            Console.WriteLine("Please input balance:");
            string balance = Console.ReadLine();
            bool validBalance = decimal.TryParse(balance, out decimal traderBalance);
            if (!validBalance)
            {
                Console.WriteLine("Wrong balance. Operation cancel.");
                return;
            }
            try
            {
                traders.RegisterNewTrader(new TraderInfo()
                {
                    Name = firstName,
                    Surname = lastName,
                    PhoneNumber = phone,
                    Balance = traderBalance
                });
                logger.Info("Registration new trader succesfully");
                logger.Info($"New trader name = {firstName}, surname = {lastName}");
                Console.WriteLine("Registration was succesfully");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
                logger.Error(e);
            }
        }

        private void ModificationTradersStock()
        {
            logger.Info("Adding stock to trader");
            Console.WriteLine("Please input traders name:");
            string traderName = Console.ReadLine();

            TraderEntity trader;
            try
            {
                trader = traders.GetTraderByName(traderName);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message}. Operation cancel.");
                logger.Error(e);
                return;
            }

            Console.WriteLine("Please input stock name:");
            string stockName = Console.ReadLine();

            StockEntity stock;
            try
            {
                stock = stockService.GetStockByName(stockName);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message}. Operation cancel.");
                logger.Error(e);
                return;
            }

            Console.WriteLine("Please input count of stocks:");
            string countStock = Console.ReadLine();

            bool validCount = Int32.TryParse(countStock, out int count);

            if (!validCount)
            {
                Console.WriteLine("Wrong count of stock. Operation cancel.");
            }


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
