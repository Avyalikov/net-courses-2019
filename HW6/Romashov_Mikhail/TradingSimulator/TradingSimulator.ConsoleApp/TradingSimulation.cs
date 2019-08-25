using System;
using System.Linq;
using TradingSimulator.Core.Dto;
using TradingSimulator.ConsoleApp.Interfaces;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;


namespace TradingSimulator.ConsoleApp
{
    class TradeSimulation
    {

        private readonly TradersService traders;
        private readonly TraderStocksService traderStocks;
        private readonly SaleService saleService;
        private readonly ILogger logger;
        public TradeSimulation(TradersService traders, TraderStocksService traderStocks, SaleService saleService, ILogger logger)
        {
            this.traders = traders;
            this.traderStocks = traderStocks;
            this.saleService = saleService;
            this.logger = logger;

            logger.InitLogger();
        }

        public void Run()
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

            logger.Info($"Try to make a sale sellerId = {buy.SellerID}, customerId = {buy.CustomerID}, stockId = {buy.StockID}, count = {buy.StockCount}");
            try
            {
                saleService.HandleBuy(buy);
                logger.Info($"Succesfully operation for sale sellerId = {buy.SellerID}, customerId = {buy.CustomerID}, stockId = {buy.StockID}, count = {buy.StockCount}");
            }
            catch (ArgumentException e)
            {
                logger.Info($"Operation for sale sellerId = {buy.SellerID}, customerId = {buy.CustomerID}, stockId = {buy.StockID}, count = {buy.StockCount} canceled");
                logger.Error(e);
            }
        }
    }
}
