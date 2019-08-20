using System;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class TraderStocksService
    {
        private readonly ITraderStockTableRepository traderStockTableRepository;

        public TraderStocksService(ITraderStockTableRepository traderStockTableRepository)
        {
            this.traderStockTableRepository = traderStockTableRepository;
        }

        public int AddNewStockToTrader(TraderInfo trader, StockInfo stock)
        {
            var entityToAdd = new StockToTraderEntity()
            {
                TraderId = trader.Id,
                StockId = stock.Id,
                StockCount = stock.Count
            };

            if (this.traderStockTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This stock has been added for trader.");
            }

            this.traderStockTableRepository.Add(entityToAdd);

            this.traderStockTableRepository.SaveChanges();

            return entityToAdd.Id;
        }
    }
}