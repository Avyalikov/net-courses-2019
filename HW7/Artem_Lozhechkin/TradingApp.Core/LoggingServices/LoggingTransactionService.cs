namespace TradingApp.Core.LoggingServices
{
    using System;
    using System.Collections.Generic;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;

    public class LoggingTransactionService : TransactionService
    {
        public LoggingTransactionService(
            IRepository<ShareEntity> shareTableRepository,
            IRepository<TransactionEntity> transactionTableRepository,
            IRepository<TraderEntity> traderTableRepository,
            IRepository<StockEntity> stockTableRepository
            ) : base(shareTableRepository, transactionTableRepository, traderTableRepository, stockTableRepository)
        {
        }
        public override int MakeShareTransaction(int sellerId, int buyerId, int shareId)
        {
            try
            {
                var transactionId = base.MakeShareTransaction(sellerId, buyerId, shareId);
                var transaction = this.transactionTableRepository.GetById(transactionId);

                Logger.FileLogger.Info($"Made a transaction between {transaction.Buyer.FirstName + " " + transaction.Buyer.LastName} " +
                    $"and trader with Id = {buyerId}");

                Logger.FileTransactionLogger.Info($"New Transaction between {transaction.Seller.FirstName + " " + transaction.Seller.LastName} (seller) " +
                    $"and {transaction.Buyer.FirstName + " " + transaction.Buyer.LastName} (buyer) " +
                    $"with payment - {transaction.TransactionPayment:0.00} at {transaction.TransactionDate}");

                return transactionId;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }

        public override IEnumerable<string> GetTopTransactionsByUser(int traderId, int top)
        {
            try
            {
                var transactions = base.GetTopTransactionsByUser(traderId, top);
                Logger.FileLogger.Info($"Request for top {top} transactions of user with id = {traderId}");
                return transactions;
            }
            catch (Exception ex)
            {
                Logger.FileLogger.Error(ex);
                throw ex;
            }
        }
    }
}
