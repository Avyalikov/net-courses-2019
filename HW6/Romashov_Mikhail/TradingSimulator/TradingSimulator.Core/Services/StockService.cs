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
        public StockEntity GetStockById(int stockID)
        {
            if (!stockTableRepository.ContainsById(stockID))
            {
                throw new ArgumentException($"Can`t get stock by this Id = {stockID}.");
            }
            return stockTableRepository.GetById(stockID);
        }

        public StockEntity GetStockByName(string  stockName)
        {
            if (!stockTableRepository.ContainsByName(stockName))
            {
                throw new ArgumentException($"Can`t get stock by this name = {stockName}.");
            }
            return stockTableRepository.GetByName(stockName);
        }

        public bool ContainsByName(string stockName)
        {
            return stockTableRepository.ContainsByName(stockName);
        }
    }
}
