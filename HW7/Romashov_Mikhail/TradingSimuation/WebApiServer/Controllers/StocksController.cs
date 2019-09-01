using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Services;
using WebApiServer.Interfaces;

namespace WebApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly ITraderStocksService traderStocksService;
        private readonly ITraderService tradersService;
        private readonly IStockService stockService;
        private readonly IValidator validator;

        public StocksController(ITraderStocksService traderStocksService, ITraderService tradersService, IStockService stockService, IValidator validator)
        {
            this.traderStocksService = traderStocksService;
            this.tradersService = tradersService;
            this.stockService = stockService;
            this.validator = validator;
        }

        // GET api/stocks
        [HttpGet]
        public string Get(int clientId)
        {
            var tradersList = traderStocksService.GetTradersStockById(clientId);
            return JsonConvert.SerializeObject(tradersList);
        }

        // POST: /stocks/add?name=_&surname=_&phone=_&
        [HttpPost("add")]
        public ActionResult AddStock(string traderName, string stockName, string count)
        {
            if (!validator.StockToTraderValidate(traderName, stockName))
            {
                return BadRequest($"Can`t add stock {stockName} to trader {traderName}");
            }

            var trader = tradersService.GetTraderByName(traderName);
            var stock = stockService.GetStockByName(stockName);


            bool validCount = Int32.TryParse(count, out int countStock);

            if (!validCount)
            {
                return BadRequest("Bad count value");
            }

          
                TraderInfo traderInfo = new TraderInfo
                {
                    Id = trader.Id,
                    Name = trader.Name,
                };
                StockInfo stockInfo = new StockInfo
                {
                    Id = stock.Id,
                    Name = stock.Name,
                    Count = countStock,
                    PricePerItem = stock.PricePerItem

                };

                try
                {
                    traderStocksService.AddNewStockToTrader(traderInfo, stockInfo);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                return Ok();
        }
    }
}