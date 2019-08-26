using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    public class TradingStart : ITradingStart
    {
        private readonly IUserTableRepository userTableRepository;
        private readonly ISharesTableRepository sharesTableRepository;
        private readonly IAddingSharesToThUserServiceTableRepository addingSharesToThUserServiceTableRepository;
        private readonly ITransactionRepositories transactionRepositories;
        private readonly TradingSimulatorDbContext tradingSimulatorDbContext;
        private readonly UserService userService;
        private readonly SharesService sharesService;
        private readonly AddingSharesToThUserService addingSharesToThUserService;
        private readonly TransactionService transactionService;

        public TradingStart(
            IUserTableRepository userTableRepository,
            ISharesTableRepository sharesTableRepository,
            IAddingSharesToThUserServiceTableRepository addingSharesToThUserServiceTableRepository,
            ITransactionRepositories transactionRepositories,
            TradingSimulatorDbContext tradingSimulatorDbContext,
            UserService userService, 
            SharesService sharesService,
            AddingSharesToThUserService addingSharesToThUserService,
            TransactionService transactionService)
        {
            this.userTableRepository = userTableRepository;
            this.sharesTableRepository = sharesTableRepository;
            this.addingSharesToThUserServiceTableRepository = addingSharesToThUserServiceTableRepository;
            this.transactionRepositories = transactionRepositories;
            this.tradingSimulatorDbContext = tradingSimulatorDbContext;
            this.userService = userService;
            this.sharesService = sharesService;
            this.addingSharesToThUserService = addingSharesToThUserService;
            this.transactionService = transactionService;

        }

        public void Run()
        {
            Console.WriteLine("#################################");
            Console.WriteLine("Welcome to the: Trading Simulator");
            Console.WriteLine("#################################");
            Console.WriteLine(@"The database already contains some values necessary to run the application!");
            Console.WriteLine("-----------------------------------------------------------------------------");
            Console.WriteLine("press '1' to start bidding!");
            int userSelectedNumber = int.Parse(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Bidding start:");
            while (true)
            {
                if (userSelectedNumber == 1)
                {
                    Thread.Sleep(5000);
                    UserEntity seller = transactionService.SelectUser();
                    UserEntity customer = transactionService.SelectUser();
                    while (seller.Equals(customer))
                    {
                        customer = transactionService.SelectUser();
                    }

                    AddingSharesToThUserEntity sellerSharesToThUserEntity = transactionService.SelectSharesToThUserEntity(seller);

                    SharesEntity sellerShares = transactionService.FindOutTheValueOfShares(sellerSharesToThUserEntity);

                    AddingSharesToThUserEntity customerSharesToThUserEntity = transactionService.SelectStocksForUserObjectParameters(customer, sellerShares);

                    int Price = transactionService.RandomNumberGenerator((int)sellerShares.Price);

                    int NumberOfShares = transactionService.RandomNumberGenerator(sellerSharesToThUserEntity.AmountStocks);

                    transactionService.StockPurchaseTransaction(seller, customer, Price, NumberOfShares, sellerSharesToThUserEntity, customerSharesToThUserEntity);

                    TransactionHistoryEntity transactionHistoryEntity = new TransactionHistoryEntity() {
                        DateTimeBay = DateTime.Now,
                        SellerId = seller.Id,
                        CustomerId = customer.Id,
                        AmountShare = NumberOfShares,
                        Cost = Price};
                    transactionService.RegisterNewTransactionHistory(transactionHistoryEntity);
                }      
            }
        }
    }
}
