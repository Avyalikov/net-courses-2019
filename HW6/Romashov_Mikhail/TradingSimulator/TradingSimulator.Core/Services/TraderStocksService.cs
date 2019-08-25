using System;
using System.Collections.Generic;
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
            var entityToAdd = new StockToTraderEntityDB()
            {
                TraderId = trader.Id,
                StockId = stock.Id,
                StockCount = stock.Count,
                PricePerItem = stock.PricePerItem
            };

            if (this.traderStockTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException($"This stock {stock.Name} has been added for trader {trader.Name} {trader.Surname}.");
            }

            this.traderStockTableRepository.Add(entityToAdd);

            this.traderStockTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public List<int> GetListTradersStock()
        {
            return traderStockTableRepository.GetListOfTraderStocksIds();
        }

        public StockToTraderEntityDB GetTraderStockById(int id)
        {
            if (!this.traderStockTableRepository.ContainsById(id))
                throw new ArgumentException($"Can`t find item by this id = {id}");
            return this.traderStockTableRepository.GetTraderStockById(id);
        }
    }
}