using System;
using System.Linq;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using System.Web.Http;
using TradingApp.Core.Repositories;
using System.Web;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace TradingWebApi.Controllers
{

    public class DealController : ApiController
    {
        private readonly IHistoryTableRepository transactionsService;
        private readonly IStockTableRepository stockService;


        public DealController() { }
        public DealController(IHistoryTableRepository transactionsService, IStockTableRepository stockService)
        {
            this.transactionsService = transactionsService;
            this.stockService = stockService;
        }

        // POST api/users
        //[HttpPost]
        [ActionName("make")]
        public string Post([FromBody] TransactionInfo value)
        {
            string StockName = stockService.Get(value.StockID).Type;
            decimal transactionQuantity = stockService.Get(value.StockID).Price * value.StockAmount;
            TransactionHistoryEntity deal = new TransactionHistoryEntity()
            {
                BuyerBalanceID = value.BuyerBalanceID,
                SellerBalanceID = value.SellerBalanceID,
                StockAmount = value.StockAmount,
                StockName = StockName,
                TimeOfTransaction = DateTime.Now,
                TransactionQuantity = transactionQuantity
            };
            transactionsService.Add(deal);
            transactionsService.SaveChanges();
            int id = transactionsService.GetId(value);
            return Newtonsoft.Json.JsonConvert.SerializeObject(transactionsService.Get(id));
        }
    }
}