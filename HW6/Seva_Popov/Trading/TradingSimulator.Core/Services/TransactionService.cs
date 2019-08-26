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
        Random random = new Random();

        public TransactionService(ITransactionRepositories transactionRepositories)
        {
            this.transactionRepositories = transactionRepositories;
        }

        public UserEntity SelectUser()
        {
            var users = transactionRepositories.GetUserList();
            int count = users.Count;
            return users[random.Next(count)];
        }

        
        public AddingSharesToThUserEntity SelectSharesToThUserEntity(UserEntity userEntity)
        {
            var usersAndShare = transactionRepositories.GetUserAndShareList(userEntity);
            int count = usersAndShare.Count; 
            return usersAndShare[random.Next(count)]; 
        }

        public SharesEntity FindOutTheValueOfShares(AddingSharesToThUserEntity addingSharesToThUserEntity)
        {
            var shares = transactionRepositories.GetShares(addingSharesToThUserEntity);
            return shares[0];
        }

        public AddingSharesToThUserEntity SelectStocksForUserObjectParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            var usersAndShare = transactionRepositories.GetUserAndShareParameters(userEntity, sharesEntity);
            int count = usersAndShare.Count;
            try
            {
                return usersAndShare[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                AddingSharesToThUserEntity addingSharesToThUserEntity = new AddingSharesToThUserEntity();
                return addingSharesToThUserEntity;
            }
            
        }

        public int RandomNumberGenerator (int number)
        {
           return random.Next(number);
        }

        public void StockPurchaseTransaction(UserEntity seller, UserEntity customer, int sharePrice, int numberOfSharesToSell, AddingSharesToThUserEntity sellerSharesToThUserEntity, AddingSharesToThUserEntity customerSharesToThUserEntity)
        {
            seller.Balance += sharePrice * numberOfSharesToSell;
            customer.Balance -= sharePrice * numberOfSharesToSell;
            sellerSharesToThUserEntity.AmountStocks -= numberOfSharesToSell;
            customerSharesToThUserEntity.AmountStocks += numberOfSharesToSell;
            transactionRepositories.UpdateUserTable(seller);
            transactionRepositories.UpdateUserTable(customer);
            transactionRepositories.UpdateUserTable(sellerSharesToThUserEntity);
            transactionRepositories.UpdateUserTable(customerSharesToThUserEntity);
            transactionRepositories.SaveChanges();
            Console.WriteLine("__________________________________________________________________________________");
            Console.WriteLine(DateTime.Now + " | " + seller.Id + " | " + customer.Id + " | " + sharePrice + " | " + numberOfSharesToSell +" | " + customerSharesToThUserEntity.AmountStocks+ " | " + sellerSharesToThUserEntity.AmountStocks + " | ");
            Console.WriteLine("__________________________________________________________________________________");
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
