using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;
using TradingSimulator.Dependencies;

namespace TradingSimulator
{
    class TradeSimulation
    {
       
        private readonly TradersService traders;
        private readonly StockService stockService;
        private readonly TraderStocksService traderStocks;
        private readonly SaleService saleService;
        private readonly BankruptService bankruptService;
       public TradeSimulation(TradersService traders, StockService stockService, TraderStocksService traderStocks, SaleService saleService, BankruptService bankruptService)
        {
            this.traders = traders;
            this.stockService = stockService;
            this.traderStocks = traderStocks;
            this.saleService = saleService;
            this.bankruptService = bankruptService;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Use one of the option: 1-reg,2-add stock, 3-add stock to client,4-removestock from client");
                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        this.TraderRegistartion();
                        break;
                    case "3":
                        this.ModificationTradersStock();
                        break;
                    case "4":
                        this.RandomSales();
                        break;
                    case "5":
                        this.GetOrangeZone();
                        break;
                    case "6":
                        this.GetBlackZone();
                        break;
                    default:
                        break;
                }
            }
        }

        private void RandomSales()
        {
            var listTradersStock = traderStocks.GetListTradersStock();

            Random random = new Random();
            int randomNumber = random.Next(1, listTradersStock.Count() + 1);

            var seller = traderStocks.GetTraderStockById(randomNumber);

            var listTraders = traders.GetList();
            TraderEntity customer;
            do
            {
                randomNumber = random.Next(1, listTraders.Count() + 1);

                customer = traders.GetTraderById(randomNumber);
            } while (seller.TraderId == customer.Id);

            BuyArguments buy = new BuyArguments
            {
                SellerID = seller.TraderId,
                CustomerID = customer.Id,
                StockID = seller.StockId,
                StockCount = 2,
                PricePerItem = seller.PricePerItem
            };

            Console.WriteLine($"{buy.SellerID} --- {buy.CustomerID} --- {buy.StockID} --- {buy.StockCount}");
            try
            {
                saleService.HandleBuy(buy);
                Console.WriteLine("Succesfully");
                Console.WriteLine($"{buy.SellerID} --- {buy.CustomerID} --- {buy.StockID} --- {buy.StockCount}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
            }
            
        }

        private void TraderRegistartion()
        {
            Console.WriteLine("Please input first name:");
            string firstName = Console.ReadLine();
            bool validFirstName = firstName.All(c => char.IsLetter(c));
            if (!validFirstName)
            {
                Console.WriteLine("Wrong first name");
                return;
            }

            Console.WriteLine("Please input last name:");
            string lastName = Console.ReadLine();
            bool validLastName = lastName.All(c => char.IsLetter(c));
            if (!validLastName)
            {
                Console.WriteLine("Wrong last name");
                return;
            }

            Console.WriteLine("Please input phone:");
            string phone = Console.ReadLine();
            bool validPhone = phone.All(c => char.IsDigit(c)) && phone.Length < 9;
            if (!validPhone)
            {
                Console.WriteLine("Wrong phone");
                return;
            }

            Console.WriteLine("Please input balance:");
            string balance = Console.ReadLine();
            bool validBalance = decimal.TryParse(balance, out decimal traderBalance);
            if (!validBalance)
            {
                Console.WriteLine("Wrong balance");
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
                Console.WriteLine("Succesfully");
            }
            catch(ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
            }
        }

        private void ModificationTradersStock()
        {
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
                Console.WriteLine($"Succesfully, id = {id}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"{e.Message} Operation cancel.");
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
            tradersWithNegativeBalance.ForEach(t => Console.WriteLine(t));
        }
    }
}
