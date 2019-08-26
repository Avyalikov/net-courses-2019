using System;
using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Services;
using TradingConsoleApp.Instruments;
using TradingConsoleApp.Interfaces;

namespace TradingConsoleApp
{
    class Trade
    {
        private readonly BalancesService balancesService;
        private readonly StocksService stocksService;
        private readonly TransactionService transactionService;
        private readonly UsersService usersService;
        private readonly ValidationOfTransactionService validationOfTransactionService;
        public Trade(BalancesService balancesService, StocksService stocksService, TransactionService transactionService, UsersService usersService, ValidationOfTransactionService validationOfTransactionService)
        {
            this.balancesService = balancesService;
            this.stocksService = stocksService;
            this.transactionService = transactionService;
            this.usersService = usersService;
            this.validationOfTransactionService = validationOfTransactionService;
        }
        Random r = new Random();
        public void Run()
        {
            
            Logger.InitLogger();
            Logger.Log.Info("Start program");

            Console.WriteLine("Welcome to TradingApp");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

            var stop = true;
            while (stop)
            {
                Console.WriteLine("If you are not registered, please type 1");
                Console.WriteLine("If you want to trade, please type 2");
                var UserInput = Console.ReadLine();
                switch (UserInput)
                {
                    case "1":
                        stop=Registration();
                        break;
                    case "2":
                        
                        break;
                }
                Console.WriteLine("Start to trade");
                int count = 0;
                int allUsersCount = usersService.Count();
                while (count != 20)
                {
                    int sellerID=1;
                    int buyerID=1;
                    while (sellerID == buyerID)
                    {
                        sellerID = r.Next(1,allUsersCount);
                        buyerID = r.Next(1,allUsersCount);
                    }
                    UserEntity seller = usersService.GetUser(sellerID);
                    UserEntity buyer = usersService.GetUser(buyerID);
                    List <BalanceEntity> sellerBalances = balancesService.GetAll(seller.ID);
                    List<BalanceEntity> buyerBalances = balancesService.GetAll(buyer.ID);
                    int stockId = sellerBalances[0].StockID;

                    TransactionInfo transactionInfo = new TransactionInfo()
                    {
                        SellerID = seller.ID,
                        BuyerID = buyer.ID,
                        SellerBalanceID = sellerBalances[0].BalanceID,
                        BuyerBalanceID = buyerBalances[0].BalanceID,
                        StockID = stockId,
                        StockAmount = 1,
                        dateTime = DateTime.Now
                    };

                    transactionService.MakeDeal(transactionInfo);
                }
                Console.WriteLine(" ");
                Console.ReadLine();
            }

        }

        public bool Registration()
        {
            bool Action=false;
            Console.WriteLine("REGISTRATION");
            Console.WriteLine(" ");
            Console.WriteLine("Please fill the registration form");
            Console.WriteLine("Name: ");
            string Name = Console.ReadLine();
            Console.WriteLine("Surname: ");
            string Surname = Console.ReadLine();
            Console.WriteLine("Phone Number: ");
            string PhoneNumber = Console.ReadLine();
            Console.WriteLine("Submit? (y/n): ");
            var UserInput = Console.ReadLine();
            switch (UserInput)
            {
                case "y":
                    UserRegistrate(Name, Surname, PhoneNumber);
                    Action = true;
                    break;
                case "n":
                    Action = false;
                    break;
            }
            return Action;
        }

        private void UserRegistrate(string name, string surname, string phoneNumber)
        {
            UserRegistrationInfo newUser = new UserRegistrationInfo()
            {
                Name = name,
                Surname = surname,
                PhoneNumber = phoneNumber,
            };
            try
            {
                usersService.RegisterNewUser(newUser);
                int ID = usersService.GetUserId(newUser);
                int stockID = r.Next(1, 10);
                int stockAmount = r.Next(1, 5);
                BalanceInfo balance = new BalanceInfo()
                {
                    UserID = ID,
                    StockID = stockID,
                    StockAmount = stockAmount,
                    Balance = stocksService.GetStock(stockID).Price * stockAmount
                };
                balancesService.CreateBalance(balance);
                Logger.Log.Info($"User registration success. New user is {name} {surname} ID: {ID}");
                Console.WriteLine("Registration successful!");
            }
            catch(ArgumentException e)
            {
                Logger.Log.Error($"Registration failed. Message: {e.Message}");
            }
           
            
        }
    }
}
