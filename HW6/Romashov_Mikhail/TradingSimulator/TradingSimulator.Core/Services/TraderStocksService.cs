﻿using System;
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
            var entityToAdd = new StockToTraderEntity()
            {
                TraderId = trader.Id,
                StockId = stock.Id,
                StockCount = stock.Count,
                PricePerItem = stock.PricePerItem
            };

            if (this.traderStockTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This stock has been added for trader.");
            }

            this.traderStockTableRepository.Add(entityToAdd);

            this.traderStockTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public List<int> GetListTradersStock()
        {
            return traderStockTableRepository.GetList();
        }

        public StockToTraderEntity GetTraderStockById(int id)
        {
            if (!this.traderStockTableRepository.ContainsById(id))
                throw new ArgumentException("Can`t find item by this id");
            return this.traderStockTableRepository.GetTraderStockById(id);
        }
    }
}