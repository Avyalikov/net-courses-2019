using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulatorConsoleApp.Data;
using TradingSimulatorConsoleApp.Services;

namespace TradingSimulatorConsoleApp
{
    public class TradingSimulation
    {
        private readonly TradingSimulatorDbContext dbContext;

        public TradingSimulation(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        Random random = new Random();

        public UserEntity ChooseARandomUser()
        {
            TransactionService transactionService = new TransactionService(dbContext);
            var userList = transactionService.SelectUser();
            int count = userList.Count;
            return userList[random.Next(count)];
        }

        public UserAndSharesEntity SelectSharesAndUser(UserEntity userEntity)
        {
            TransactionService transactionService = new TransactionService(dbContext);
            var userAndSharesList = transactionService.SelectSharesToThUserEntity(userEntity);
            int count = userAndSharesList.Count;
            return userAndSharesList[random.Next(count)];
        }

        public SharesEntity ChooseShareskValue(UserAndSharesEntity addingSharesToThUserEntity)
        {
            TransactionService transactionService = new TransactionService(dbContext);
            var shares = transactionService.FindOutTheValueOfShares(addingSharesToThUserEntity);
            return shares[0];
        }

        public UserAndSharesEntity SelectStocksForUserObjectParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            TransactionService transactionService = new TransactionService(dbContext);
            var usersAndShare = transactionService.SelectStocksForUserObjectParameters(userEntity, sharesEntity);
            int count = usersAndShare.Count;
            try
            {
                return usersAndShare[0];
            }
            catch (System.ArgumentOutOfRangeException)
            {
                UserAndSharesEntity userAndSharesEntity = new UserAndSharesEntity();
                return userAndSharesEntity;
            }
        }

        public int RandomNumberGenerator(int number)
        {
            return random.Next(number);
        }

        public void StockPurchaseTransaction(UserEntity seller, UserEntity customer, int sharePrice, int numberOfSharesToSell, UserAndSharesEntity sellerSharesToThUserEntity, UserAndSharesEntity customerSharesToThUserEntity)
        {
            TransactionService transactionService = new TransactionService(dbContext);
            seller.Balance += sharePrice * numberOfSharesToSell;
            customer.Balance -= sharePrice * numberOfSharesToSell;
            sellerSharesToThUserEntity.AmountStocks -= numberOfSharesToSell;
            customerSharesToThUserEntity.AmountStocks += numberOfSharesToSell;
            transactionService.StockPurchaseTransaction(seller, customer, sharePrice, numberOfSharesToSell, sellerSharesToThUserEntity, customerSharesToThUserEntity);
        }



        public void RunTradingSimulation()
        {
            TradingSimulation tradingSimulation = new TradingSimulation(dbContext);
            UserEntity seller = tradingSimulation.ChooseARandomUser();
            UserEntity customer = tradingSimulation.ChooseARandomUser();
            while (seller.Equals(customer))
            {
                customer = tradingSimulation.ChooseARandomUser();
            }

            UserAndSharesEntity sellerSharesToThUserEntity = tradingSimulation.SelectSharesAndUser(seller);

            SharesEntity sellerShares = tradingSimulation.ChooseShareskValue(sellerSharesToThUserEntity);

            UserAndSharesEntity customerSharesToThUserEntity = tradingSimulation.SelectStocksForUserObjectParameters(customer, sellerShares);

            int Price = tradingSimulation.RandomNumberGenerator((int)sellerShares.Price);

            int NumberOfShares = tradingSimulation.RandomNumberGenerator(sellerSharesToThUserEntity.AmountStocks);

            tradingSimulation.StockPurchaseTransaction(seller, customer, Price, NumberOfShares, sellerSharesToThUserEntity, customerSharesToThUserEntity);

            TransactionHistoryEntity transactionHistoryEntity = new TransactionHistoryEntity()
            {
                DateTimeBay = DateTime.Now,
                SellerId = seller.Id,
                CustomerId = customer.Id,
                AmountShare = NumberOfShares,
                Cost = Price
            };
            TransactionService transactionService = new TransactionService(dbContext);
            transactionService.RegisterNewTransactionHistory(transactionHistoryEntity);
        }
    }
}
