using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingSimulator.Core.Models;
using WebClient.Components;

namespace WebClient
{
    class TradingSimulation
    {
        public void TradingSimulate()
        {
            int count = int.Parse(Web.Get("http://localhost:80/tradersstock/count"));

            Random random = new Random();
            int randomNumber = random.Next(1, count + 1);
            var seller = JsonConvert.DeserializeObject<StockToTraderEntityDB>(Web.Get($"http://localhost:80/tradersstock?id={randomNumber}"));

            var countTraders = int.Parse(Web.Get("http://localhost:80/clients/count"));
            TraderEntity customer;
            do
            {
                randomNumber = random.Next(1, countTraders + 1);
            } while (seller.TraderId == randomNumber);
            customer = JsonConvert.DeserializeObject<TraderEntityDB>(Web.Get($"http://localhost:80/clients/get?id={randomNumber}"));

            Web.Post($"http://localhost:80/deal/make?" +
                $"sellerId={seller.TraderId}&" +
                $"customerID={customer.Id}&" +
                $"stockID={seller.StockId}&" +
                $"stockCount=2&" +
                $"pricePerItem={seller.PricePerItem}");
        }
    }
}
