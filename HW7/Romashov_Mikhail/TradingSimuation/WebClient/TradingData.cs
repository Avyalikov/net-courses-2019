using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using WebClient.Components;

namespace WebClient
{
    class TradingData
    {
        public void Run()
        {
            while (true)
            {
                Console.WriteLine(@"
-----------------
Use one of the option:
    1-Get list of traders
    2-Get traders stock
    3-Get trader balance
    4-Get traders transaction history
    5-New trader Registration
    6-Add stock to trader
    0-Exit
-----------------");
                string inputString = Console.ReadLine();
                switch (inputString)
                {
                    case "1":
                        this.GetTraders();
                        break;
                    case "2":
                        this.GetTradersStock();
                        break;
                    case "3":
                        this.GetTraderBalance();
                        break;
                    case "4":
                        this.GetTraderHistory();
                        break;
                    case "5":
                        this.TraderRegistartion();
                        break;
                    case "6":
                        this.AddStockToTrader();
                        break;
                    case "0":
                        return;
                    default:
                        break;
                }
            }
        }

        private void GetTraders()
        {
            Console.WriteLine("Please input max count of traders");
            string countOfTraders = Console.ReadLine();

            bool validCount = Int32.TryParse(countOfTraders, out int count);

            if (!validCount)
            {
                Console.WriteLine("Wrong count of traders. Operation cancel.");
                return;
            }
            Console.WriteLine(Web.Get($"http://localhost:80/clients?top={count}"));
        }

        private void GetTradersStock()
        {
            Console.WriteLine("Please input traders id");
            string traderId = Console.ReadLine();

            bool validId = Int32.TryParse(traderId, out int Id);

            if (!validId)
            {
                Console.WriteLine("Wrong id. Operation cancel.");
                return;
            }
            Console.WriteLine(Web.Get($"http://localhost:80/stocks?clientId={Id}"));
        }

        private void GetTraderBalance()
        {
            Console.WriteLine("Please input trader id");
            string traderId = Console.ReadLine();

            bool validId = Int32.TryParse(traderId, out int Id);

            if (!validId)
            {
                Console.WriteLine("Wrong id. Operation cancel.");
                return;
            }
            Console.WriteLine(Web.Get($"http://localhost:80/balance?clientId={Id}"));
        }

        private void GetTraderHistory()
        {
            Console.WriteLine("Please input trader id");
            string traderId = Console.ReadLine();

            bool validId = Int32.TryParse(traderId, out int id);

            if (!validId)
            {
                Console.WriteLine("Wrong id. Operation cancel.");
                return;
            }

            Console.WriteLine("Please input max count of deals");
            string deal = Console.ReadLine();

            bool validDeal = Int32.TryParse(deal, out int count);

            if (!validDeal)
            {
                Console.WriteLine("Wrong id. Operation cancel.");
                return;
            }
            Console.WriteLine(Web.Get($"http://localhost:80/history?clientId={id}&maxCount={count}"));
        }

        private void TraderRegistartion()
        {
            Console.WriteLine("Please input first name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Please input last name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Please input phone:");
            string phone = Console.ReadLine();

            Console.WriteLine(Web.Post($"http://localhost:80/clients/add?name={firstName}&surname={lastName}&phone={phone}"));
        }

        private void AddStockToTrader()
        {
            Console.WriteLine("Please input traders name:");
            string traderName = Console.ReadLine();
            Console.WriteLine("Please input stock name:");
            string stockName = Console.ReadLine();
            Console.WriteLine("Please input count of stocks:");
            string countStock = Console.ReadLine();

            bool validCount = Int32.TryParse(countStock, out int count);

            if (!validCount)
            {
                Console.WriteLine("Wrong count of stock. Operation cancel.");
                return;
            }

            Console.WriteLine(Web.Post($"http://localhost:80/stocks/add?traderName={traderName}&stockName={stockName}&count={count}&"));
        }
    }
}




