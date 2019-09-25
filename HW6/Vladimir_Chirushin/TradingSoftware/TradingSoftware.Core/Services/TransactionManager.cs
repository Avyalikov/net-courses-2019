namespace TradingSoftware.Core.Services
{
    using System;
    using System.Collections.Generic;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class TransactionManager : ITransactionManager
    {
        private readonly IClientManager clientManager;
        private readonly ISharesRepository sharesRepository;
        private readonly IBlockOfSharesRepository blockOfSharesRepository;
        private readonly IClientRepository clientRepository;
        private readonly ITransactionRepository transactionRepository;

        public TransactionManager(
            IClientManager clientManager,
            IBlockOfSharesRepository blockOfSharesRepository,
            IClientRepository clientRepository,
            ISharesRepository sharesRepository,
            ITransactionRepository transactionRepository)
        {
            this.clientManager = clientManager;
            this.blockOfSharesRepository = blockOfSharesRepository;
            this.clientRepository = clientRepository;
            this.sharesRepository = sharesRepository;
            this.transactionRepository = transactionRepository;
        }

        public void AddTransaction(int sellerID, int buyerID, int shareID, int shareAmount)
        {
            var transaction = new Transaction
            {
                dateTime = DateTime.Now,
                SellerID = sellerID,
                BuyerID = buyerID,
                ShareID = shareID,
                Amount = shareAmount
            };
            this.AddTransaction(transaction);
        }

        public void AddTransaction(Transaction transaction)
        {
            this.transactionRepository.Insert(transaction);
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return this.transactionRepository.GetAllTransaction();
        }

        public bool Validate(Transaction transaction)
        {
            bool isSellerAndBuyerDifferent = transaction.SellerID != transaction.BuyerID;

            bool isSellerHasEnoughStocks;
            if (this.blockOfSharesRepository.IsClientHasShareType(transaction.SellerID, transaction.ShareID))
            {
                int sellerStockAmount = this.blockOfSharesRepository.GetClientShareAmount(transaction.SellerID, transaction.ShareID);
                isSellerHasEnoughStocks = sellerStockAmount >= transaction.Amount ? true : false;
            }
            else
            {
                isSellerHasEnoughStocks = false;
            }

            bool isBuyerCanAffordStocks = this.clientRepository.GetClientBalance(transaction.BuyerID) > 0;

            if (isSellerAndBuyerDifferent &&
                isSellerHasEnoughStocks &&
                isBuyerCanAffordStocks)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void TransactionAgent(Transaction transaction)
        {
            BlockOfShares sellerBlockOfShare = new BlockOfShares
            {
                ClientID = transaction.SellerID,
                ShareID = transaction.ShareID,
                Amount = (-1) * transaction.Amount // subtract stock from seller
            };

            BlockOfShares buyerBlockOfShare = new BlockOfShares
            {
                ClientID = transaction.BuyerID,
                ShareID = transaction.ShareID,
                Amount = transaction.Amount
            };

            if (!this.blockOfSharesRepository.IsClientHasShareType(transaction.BuyerID, transaction.ShareID))
            {
                this.blockOfSharesRepository.Insert(buyerBlockOfShare);
            }
            else
            {
                this.blockOfSharesRepository.ChangeShareAmountForClient(buyerBlockOfShare);
            }

            this.blockOfSharesRepository.ChangeShareAmountForClient(sellerBlockOfShare);

            decimal sharePrice = this.sharesRepository.GetSharePrice(transaction.ShareID);

            this.clientManager.ChangeBalance(transaction.BuyerID, -1 * sharePrice * transaction.Amount);
            this.clientManager.ChangeBalance(transaction.SellerID, sharePrice * transaction.Amount);
        }
    }
}