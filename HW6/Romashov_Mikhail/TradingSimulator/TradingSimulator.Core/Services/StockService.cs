using System;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class StockService
    {

        private readonly IStockTableRepository stockTableRepository;
        public StockService(IStockTableRepository stockTableRepository)
        {
            this.stockTableRepository = stockTableRepository;
        }
        public StockEntity GetStock(int stockID)
        {
            if (!stockTableRepository.ContainsById(stockID))
            {
                throw new ArgumentException("Can`t get stock by this Id.");
            }
            return stockTableRepository.Get(stockID);
        }
    }
}
