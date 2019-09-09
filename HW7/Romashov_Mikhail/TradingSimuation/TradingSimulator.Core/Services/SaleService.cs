using System;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Interfaces;

namespace TradingSimulator.Core.Services
{
    public class SaleService : ISaleService
    {
        private readonly ITraderStockTableRepository traderStockTableRepository;
        private readonly ITraderTableRepository traderTableRepository;
        private readonly IHistoryTableRepository historyTableRepository;
        public SaleService (
            ITraderStockTableRepository traderStockTableRepository,
            ITraderTableRepository traderTableRepository,
            IHistoryTableRepository historyTableRepository)
        {
            this.traderTableRepository = traderTableRepository;
            this.traderStockTableRepository = traderStockTableRepository;
            this.historyTableRepository = historyTableRepository;
        }

        
        public void HandleBuy(BuyArguments args)
        {
            this.ValidateBuyArguments(args);
            this.SubtractStockFromSeller(args);
            this.AdditionStockToCustomer(args);
            this.SubstractBalance(args);
            this.AdditionBalance(args);
            this.SaveHistory(args);
        }

        private void ValidateBuyArguments(BuyArguments args)
        {
            if (!this.traderStockTableRepository.ContainsSeller(args))
            {
                throw new ArgumentException($"Imposible to make a sale, because seller hasn`t this stock id = {args.StockID}");
            }
            var checkEntity = traderStockTableRepository.GetStocksFromSeller(args);
            if (args.StockCount > checkEntity.StockCount)
            {
                throw new ArgumentException($"Imposible to make a sale, because seller has only {checkEntity.StockCount} stocks, but requested {args.StockCount}.");
            }
        }
        private void SubtractStockFromSeller(BuyArguments args)
        {
            traderStockTableRepository.SubtractStockFromSeller(args);
            traderStockTableRepository.SaveChanges();
        }

        public void AdditionStockToCustomer(BuyArguments args)
        {
            var entityToAdd = new StockToTraderEntityDB()
            {
                TraderId = args.CustomerID,
                StockId = args.StockID,
                StockCount = args.StockCount,
                PricePerItem = args.PricePerItem
            };
            if (traderStockTableRepository.Contains(entityToAdd))
            {
                traderStockTableRepository.AdditionalStockToCustomer(args);
            }
            else
            {
                traderStockTableRepository.Add(entityToAdd);
            }
            traderStockTableRepository.SaveChanges();
        }

        private void SubstractBalance(BuyArguments args)
        {
            if (!this.traderTableRepository.ContainsById(args.CustomerID))
            {
                throw new ArgumentException($"Cant get trader by this id = {args.CustomerID}.");
            }
            this.traderTableRepository.SubstractBalance(args.CustomerID, args.StockCount * args.PricePerItem);
            this.traderTableRepository.SaveChanges();
        }

        private void AdditionBalance(BuyArguments args)
        {
            if (!this.traderTableRepository.ContainsById(args.SellerID))
            {
                throw new ArgumentException($"Cant get trader by this id = {args.SellerID}.");
            }
            this.traderTableRepository.AdditionBalance(args.SellerID, args.StockCount * args.PricePerItem);
            this.traderTableRepository.SaveChanges();
        }

        public void SaveHistory(BuyArguments args)
        {
            var stockInSaleHistory = new HistoryEntity()
            {
                CreateAt = DateTime.Now,
                SellerID = args.SellerID,
                CustomerID = args.CustomerID,
                StockID = args.StockID,
                StockCount = args.StockCount,
                TotalPrice = args.StockCount * args.PricePerItem

            };

            this.historyTableRepository.Add(stockInSaleHistory);
            this.historyTableRepository.SaveChanges();
        }
    }
}
