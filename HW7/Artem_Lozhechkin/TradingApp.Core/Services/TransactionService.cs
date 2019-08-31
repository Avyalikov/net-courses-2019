namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;

    public class TransactionService
    {
        protected readonly IRepository<ShareEntity> shareTableRepository;
        protected readonly IRepository<TransactionEntity> transactionTableRepository;
        protected readonly IRepository<TraderEntity> traderTableRepository;
        protected readonly IRepository<StockEntity> stockTableRepository;

        public TransactionService(
            IRepository<ShareEntity> shareTableRepository,
            IRepository<TransactionEntity> transactionTableRepository,
            IRepository<TraderEntity> traderTableRepository,
            IRepository<StockEntity> stockTableRepository)
        {
            this.shareTableRepository = shareTableRepository;
            this.transactionTableRepository = transactionTableRepository;
            this.traderTableRepository = traderTableRepository;
            this.stockTableRepository = stockTableRepository;
        }

        public virtual int MakeShareTransaction(int sellerId, int buyerId, int shareId)
        {
            ValidateDifferenceBetweenBuyerAndSeller(buyerId, sellerId);
            ValidateTraderExistenceInDataSource(buyerId);
            ValidateTraderExistenceInDataSource(sellerId);

            TraderEntity seller = this.traderTableRepository.GetById(sellerId);
            TraderEntity buyer = this.traderTableRepository.GetById(buyerId);

            ValidateShareExistence(seller, shareId);

            ShareEntity share = this.shareTableRepository.GetById(shareId);

            var shareStock = this.stockTableRepository.GetById(share.Stock.Id);
            ValidateBuyerBalance(buyer, share, shareStock);

            decimal payment = share.Amount * shareStock.PricePerUnit * share.ShareType.Multiplier;
            buyer.Balance -= payment;
            this.traderTableRepository.Save(buyer);
            share.Owner = buyer;
            this.shareTableRepository.Save(share);
            seller.Balance += payment;
            this.traderTableRepository.Save(seller);
            var transaction = new TransactionEntity()
            {
                Seller = seller,
                Buyer = buyer,
                Share = share,
                TransactionPayment = payment,
                TransactionDate = DateTime.Now
            };
            var transactionId = this.transactionTableRepository.Add(transaction);

            this.transactionTableRepository.SaveChanges();
            return transactionId;
        }
        public virtual IEnumerable<string> GetTopTransactionsByUser(int traderId, int top)
        {
            ValidateTraderExistenceInDataSource(traderId);
            return this.transactionTableRepository
                .GetAll().Where(t => t.Buyer.Id == traderId || t.Seller.Id == traderId)
                .Select(t => $"Seller: {t.Seller.FirstName} {t.Seller.LastName}, Buyer: {t.Buyer.FirstName} {t.Buyer.LastName}, " +
                $"Price: {t.TransactionPayment}, TimeStamp: {t.TransactionDate}")
                .Take(top);
        }
        private void ValidateTraderExistenceInDataSource(int traderId)
        {
            if (this.traderTableRepository.GetById(traderId) == null)
            {
                throw new Exception($"There's no user with given id in data source.");
            }
        }
        private void ValidateDifferenceBetweenBuyerAndSeller(int buyerId, int sellerId)
        {
            if (buyerId == sellerId)
            {
                throw new Exception("Cant make a transaction because buyer and seller is a same trader.");
            }
        }
        private void ValidateBuyerBalance(TraderEntity traderEntity, ShareEntity share, StockEntity stock)
        {
            if (traderEntity.Balance < share.Amount * stock.PricePerUnit * share.ShareType.Multiplier)
            {
                throw new Exception("Cant make a transaction because buyer dont have enough money.");
            }
        }
        private void ValidateShareExistence(TraderEntity traderEntity, int shareId)
        {
            if (this.shareTableRepository.GetById(shareId).Owner != traderEntity)
            {
                throw new Exception("Seller dont have shares with given Id.");
            }
        }
    }
}
