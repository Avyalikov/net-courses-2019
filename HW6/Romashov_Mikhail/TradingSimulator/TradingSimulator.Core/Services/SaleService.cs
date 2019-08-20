using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Repositories;
namespace TradingSimulator.Core.Services
{
    public class SaleService
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

        public void ValidateBuyArguments(BuyArguments args)
        {
            var checkEntity = traderStockTableRepository.FindStocksFromSeller(args);
            if (args.StockCount > checkEntity.StockCount)
            {
                throw new ArgumentException($"Imposible to make a sale, because seller has only {checkEntity.StockCount} stocks, but requested {args.StockCount}.");
            }
        }
        public void SubtractStockFromSeller(BuyArguments args)
        {
            traderStockTableRepository.SubtractStock(args);
            traderStockTableRepository.SaveChanges();
        }

        public void AdditionStockToCustomer(BuyArguments args)
        {
            var item = new StockToTraderEntity()
            {
                TraderId = args.SellerID,
                StockId = args.StockID
            };
            if (traderStockTableRepository.Contains(item))
            {
                traderStockTableRepository.SubtractStock(args);
            }
            else
            {
                var entityToAdd = new StockToTraderEntity()
                {
                    TraderId = args.CustomerID,
                    StockId = args.StockID,
                    StockCount = args.StockCount
                };
                traderStockTableRepository.Add(entityToAdd);
            }
            traderStockTableRepository.SaveChanges();
        }

        public void SubstractBalance(BuyArguments args)
        {
            if (!this.traderTableRepository.ContainsById(args.CustomerID))
            {
                throw new ArgumentException("Cant get trader by this id.");
            }
            this.traderTableRepository.SubstractBalance(args.CustomerID, args.StockCount * args.PricePerItem);
            this.traderTableRepository.SaveChanges();
        }

        public void AdditionBalance(BuyArguments args)
        {
            if (!this.traderTableRepository.ContainsById(args.SellerID))
            {
                throw new ArgumentException("Cant get trader by this id.");
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
