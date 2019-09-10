using System;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Services
{
    public class TransactionService
    {
        private readonly IHistoryTableRepository historyTableRepository;
        private readonly IBalanceTableRepository balanceTableRepository;
        private readonly IStockTableRepository stockTableRepository;
        
        public TransactionService(IHistoryTableRepository historyTableRepository, IBalanceTableRepository balanceTableRepository, IStockTableRepository stockTableRepository)
        {
            this.historyTableRepository = historyTableRepository;
            this.balanceTableRepository = balanceTableRepository;
            this.stockTableRepository = stockTableRepository;
        }
        public int MakeDeal(TransactionInfo args)
        {
            var _args = args;
            StockEntity stock = stockTableRepository.Get(args.StockID);

            ValidationOfTransactionService validationService = new ValidationOfTransactionService(this.balanceTableRepository);
            bool sellerValidation = validationService.CheckPermissionToSell(args.SellerID);
            bool buyerValidation = validationService.CheckPermissionToBuy(args.BuyerID);
            
            if (sellerValidation && buyerValidation)
            {
                MakeBuy(_args, stock);
                return TransactionHistoryLogger(_args,stock);
            }
            else
            {
                throw new ArgumentException($"This deal can't be realized, because seller permission to sell is {sellerValidation} and buyer permission to buy is {buyerValidation}");
            }            
       }

        public void MakeBuy(TransactionInfo args, StockEntity stock)
        {
            BalanceEntity buyerOldBalance = this.balanceTableRepository.Get(args.BuyerBalanceID);
            buyerOldBalance.StockAmount += args.StockAmount;
            buyerOldBalance.Balance -= args.StockAmount * stock.Price;
            this.balanceTableRepository.Change(buyerOldBalance);
            this.balanceTableRepository.SaveChanges();
        }
        public void MakeSell(TransactionInfo args, StockEntity stock)
        {
            BalanceEntity sellerOldBalance = this.balanceTableRepository.Get(args.SellerBalanceID);
            sellerOldBalance.StockAmount -= args.StockAmount;
            sellerOldBalance.Balance += args.StockAmount * stock.Price;
            this.balanceTableRepository.Change(sellerOldBalance);
            this.balanceTableRepository.SaveChanges();
        }
        public int TransactionHistoryLogger(TransactionInfo args, StockEntity stock)
        {
            int transactionID;
            TransactionHistoryEntity historyEntity = new TransactionHistoryEntity();

            historyEntity.SellerBalanceID = args.SellerBalanceID;
            historyEntity.BuyerBalanceID = args.BuyerBalanceID;
            historyEntity.StockName = stock.Type;
            historyEntity.StockAmount = args.StockAmount;
            historyEntity.TransactionQuantity = args.StockAmount * stock.Price;
            historyEntity.TimeOfTransaction = args.dateTime;
            transactionID = historyTableRepository.Add(historyEntity);
            historyTableRepository.SaveChanges();
            return transactionID;
        }
    }
}