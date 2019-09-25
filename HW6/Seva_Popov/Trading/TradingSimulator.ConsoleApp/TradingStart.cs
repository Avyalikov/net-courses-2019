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
        private readonly TradingSimulation tradingSimulation;

        public TradingStart(
            IUserTableRepository userTableRepository,
            ISharesTableRepository sharesTableRepository,
            IAddingSharesToThUserServiceTableRepository addingSharesToThUserServiceTableRepository,
            ITransactionRepositories transactionRepositories,
            TradingSimulatorDbContext tradingSimulatorDbContext,
            UserService userService,
            SharesService sharesService,
            AddingSharesToThUserService addingSharesToThUserService,
            TransactionService transactionService,
            TradingSimulation tradingSimulation)
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
            this.tradingSimulation = tradingSimulation;
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
                    UserEntity seller = tradingSimulation.ChooseARandomUser();
                    UserEntity customer = tradingSimulation.ChooseARandomUser();
                    while (seller.Equals(customer))
                    {
                        customer = tradingSimulation.ChooseARandomUser();
                    }

                    AddingSharesToThUserEntity sellerSharesToThUserEntity = tradingSimulation.SelectSharesAndUser(seller);

                    SharesEntity sellerShares = tradingSimulation.ChooseShareskValue(sellerSharesToThUserEntity);
                    
                    AddingSharesToThUserEntity customerSharesToThUserEntity = tradingSimulation.SelectStocksForUserObjectParameters(customer, sellerShares);

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
                    transactionService.RegisterNewTransactionHistory(transactionHistoryEntity);
                }      
            }
        }
    }
}
