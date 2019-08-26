using System;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Services
{
    public class StocksService
    {
        private readonly IStockTableRepository stockTableRepository;
        
        public StocksService(IStockTableRepository stockTableRepository)
        {
            this.stockTableRepository = stockTableRepository;
        }

        public StockEntity GetStock(int stockId)
        {
            if (!this.stockTableRepository.Contains(stockId))
            {
                throw new ArgumentException("Can't find stock with this Id. It might be not exists");
            }
            return this.stockTableRepository.Get(stockId);
        }

        public int RegisterNewStock(StockRegistrationInfo args)
        {
            var entityToAdd = new StockEntity()
            {
                Type = args.Type,
                Price = args.Price
            };
            if (this.stockTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This stock is already exists. Can't continue");
            }
            this.stockTableRepository.Add(entityToAdd);
            this.stockTableRepository.SaveChanges();
            return entityToAdd.ID;
        }
    }
}