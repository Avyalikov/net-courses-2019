using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;

namespace TradingSimulator.Core.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepositories transactionRepositories;

        public TransactionService(ITransactionRepositories transactionRepositories)
        {
            this.transactionRepositories = transactionRepositories;
        }

        public List<UserEntity> SelectUser()
        {
            var users = transactionRepositories.GetUserList();
            return users;
        }
      
        public List<AddingSharesToThUserEntity> SelectSharesToThUserEntity(UserEntity userEntity)
        {
            var usersAndShare = transactionRepositories.GetUserAndShareList(userEntity);
            return usersAndShare;
        }

        public List<SharesEntity> FindOutTheValueOfShares(AddingSharesToThUserEntity addingSharesToThUserEntity)
        {
            var shares = transactionRepositories.GetShares(addingSharesToThUserEntity);
            return shares;
        }

        public List<AddingSharesToThUserEntity> SelectStocksForUserObjectParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            var usersAndShare = transactionRepositories.GetUserAndShareParameters(userEntity, sharesEntity);
            return usersAndShare;
        }

        public void StockPurchaseTransaction(UserEntity seller, UserEntity customer, int sharePrice, int numberOfSharesToSell, AddingSharesToThUserEntity sellerSharesToThUserEntity, AddingSharesToThUserEntity customerSharesToThUserEntity)
        {
            transactionRepositories.UpdateUserTable(seller);
            transactionRepositories.UpdateUserTable(customer);
            transactionRepositories.UpdateUserTable(sellerSharesToThUserEntity);
            transactionRepositories.UpdateUserTable(customerSharesToThUserEntity);
            transactionRepositories.SaveChanges();
        }

        public void RegisterNewTransactionHistory(TransactionHistoryEntity transactionHistoryEntity)
        {
            var entityToAdd = new TransactionHistoryEntity() {
                DateTimeBay = transactionHistoryEntity.DateTimeBay,
                SellerId = transactionHistoryEntity.SellerId,
                CustomerId = transactionHistoryEntity.CustomerId,
                AmountShare = transactionHistoryEntity.AmountShare,
                Cost = transactionHistoryEntity.Cost
            };

            this.transactionRepositories.Add(entityToAdd);

            this.transactionRepositories.SaveChanges();
            Console.WriteLine("The transaction was successful and added to the database.");
        }
    }
}
