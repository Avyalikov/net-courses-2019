using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Interfaces;
using WebApiServer.Interfaces;

namespace WebApiServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DealController : ControllerBase
    {
        private readonly ISaleService saleService;
        private readonly IStockService stockService;
        public DealController(ISaleService saleService, IStockService stockService)
        {
            this.saleService = saleService;
            this.stockService = stockService;
        }

        // POST deal/make?sellerId=_&customerID=_&stockID=_&stockCount=_&pricePerItem=_
        [HttpPost("make")]
        public ActionResult MakeDeal(int sellerID, int customerID, int stockID, int stockCount, decimal pricePerItem)
        {
            try
            {
                var stock = stockService.GetStockById(stockID);

                BuyArguments buy = new BuyArguments
                {
                    SellerID = sellerID,
                    CustomerID = customerID,
                    StockID = stockID,
                    StockCount = stockCount,
                    PricePerItem = stock.PricePerItem
                };
                saleService.HandleBuy(buy);
            }
            catch(ArgumentException)
            {
                return StatusCode(400, "Operation cancel");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Transaction was succesfully");
        }
    }
}