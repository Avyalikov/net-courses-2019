using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    public class TradingSimulation
    {
        private readonly TransactionService transactionService;
        
        public TradingSimulation(TransactionService transactionService)
        {
            this.transactionService = transactionService;
        }
        Random random = new Random();

        public UserEntity ChooseARandomUser()
        {
           var userList = transactionService.SelectUser();
           int count = userList.Count;
           return userList[random.Next(count)];
        }

        public AddingSharesToThUserEntity SelectSharesAndUser(UserEntity userEntity)
        {
           var userAndSharesList = transactionService.SelectSharesToThUserEntity(userEntity);
           int count = userAndSharesList.Count;
           return userAndSharesList[random.Next(count)];
        }

        public SharesEntity ChooseShareskValue(AddingSharesToThUserEntity addingSharesToThUserEntity)
        {
            var shares = transactionService.FindOutTheValueOfShares(addingSharesToThUserEntity);
            return shares[0];
        }

        public AddingSharesToThUserEntity SelectStocksForUserObjectParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            var usersAndShare = transactionService.SelectStocksForUserObjectParameters(userEntity, sharesEntity);
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

        public int RandomNumberGenerator(int number)
        {
            return random.Next(number);
        }

        public void StockPurchaseTransaction(UserEntity seller, UserEntity customer, int sharePrice, int numberOfSharesToSell, AddingSharesToThUserEntity sellerSharesToThUserEntity, AddingSharesToThUserEntity customerSharesToThUserEntity)
        {
            seller.Balance += sharePrice * numberOfSharesToSell;
            customer.Balance -= sharePrice * numberOfSharesToSell;
            sellerSharesToThUserEntity.AmountStocks -= numberOfSharesToSell;
            customerSharesToThUserEntity.AmountStocks += numberOfSharesToSell;
            transactionService.StockPurchaseTransaction(seller, customer, sharePrice, numberOfSharesToSell, sellerSharesToThUserEntity, customerSharesToThUserEntity);
            Console.WriteLine("__________________________________________________________________________________");
            Console.WriteLine(DateTime.Now + " | " + seller.Id + " | " + customer.Id + " | " + sharePrice + " | " + numberOfSharesToSell + " | " + customerSharesToThUserEntity.AmountStocks + " | " + sellerSharesToThUserEntity.AmountStocks + " | ");
            Console.WriteLine("__________________________________________________________________________________");
        }
    }
}
