using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp.Data;
using TradingSimulatorConsoleApp.Repositories;

namespace TradingSimulatorConsoleApp.Services
{
    public class TransactionService
    {
        private readonly TradingSimulatorDbContext dbContext;

        public TransactionService(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserEntity> SelectUser()
        {
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);
            var users = transactionRepositories.GetUserList();
            return users;
        }

        public List<UserAndSharesEntity> SelectSharesToThUserEntity(UserEntity userEntity)
        {
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);
            var usersAndShare = transactionRepositories.GetUserAndShareList(userEntity);
            return usersAndShare;
        }

        public List<SharesEntity> FindOutTheValueOfShares(UserAndSharesEntity userAndSharesEntity)
        {
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);
            var shares = transactionRepositories.GetShares(userAndSharesEntity);
            return shares;
        }

        public List<UserAndSharesEntity> SelectStocksForUserObjectParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);
            var usersAndShare = transactionRepositories.GetUserAndShareParameters(userEntity, sharesEntity);
            return usersAndShare;
        }

        public void StockPurchaseTransaction(UserEntity seller, UserEntity customer, int sharePrice, int numberOfSharesToSell, UserAndSharesEntity sellerSharesToThUserEntity, UserAndSharesEntity customerSharesToThUserEntity)
        {
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);
            transactionRepositories.UpdateUserTable(seller);
            transactionRepositories.UpdateUserTable(customer);
            transactionRepositories.UpdateUserTable(sellerSharesToThUserEntity);
            transactionRepositories.UpdateUserTable(customerSharesToThUserEntity);
            transactionRepositories.SaveChanges();
        }

        public void RegisterNewTransactionHistory(TransactionHistoryEntity transactionHistoryEntity)
        {

            var entityToAdd = new TransactionHistoryEntity()
            {
                DateTimeBay = transactionHistoryEntity.DateTimeBay,
                SellerId = transactionHistoryEntity.SellerId,
                CustomerId = transactionHistoryEntity.CustomerId,
                AmountShare = transactionHistoryEntity.AmountShare,
                Cost = transactionHistoryEntity.Cost
            };
            TransactionRepositories transactionRepositories = new TransactionRepositories(dbContext);

            transactionRepositories.Add(entityToAdd);

            transactionRepositories.SaveChanges();
        }
    }
}
