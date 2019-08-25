using StructureMap;
using System;
using System.Linq;
using TradingSimulator.ConsoleApp.Dependency;
using TradingSimulator.ConsoleApp.Repositories;
using TradingSimulator.Core.Dto;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Services;

namespace TradingSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new TradingSimulatorRegistry());
            var userService = container.GetInstance<UserService>();


            //userService.RegisterNewUser(new UserRegistrationInfo()
            //{
            //    Name = "Юрий",
            //    Surname = "Гагарин",
            //    Phone = "444-44-44"
            //});

            //var sharesService = container.GetInstance<SharesService>();

            //sharesService.RegisterNewShares(new SharesRegistrationInfo()
            //{
            //    Name = "ROSN",
            //    Price = 410
            //});

            //var addingSharesToThUserService = container.GetInstance<AddingSharesToThUserService>();

            //addingSharesToThUserService.RegisterNewSharesToTheUser(new AddingSharesToThUserEntity()
            //{
            //    UserId = 4,
            //    ShareId = 4,
            //    AmountStocks = 666
            //});


            var transactionService = container.GetInstance<TransactionService>();
            UserEntity seller =  transactionService.SelectUser();
            UserEntity customer = transactionService.SelectUser();
            while (seller.Equals(customer))
            {
                customer = transactionService.SelectUser();
            }

            AddingSharesToThUserEntity sellerSharesToThUserEntity = transactionService.SelectSharesToThUserEntity(seller);
                   
            SharesEntity sellerShares =  transactionService.FindOutTheValueOfShares(sellerSharesToThUserEntity);

            AddingSharesToThUserEntity customerSharesToThUserEntity = transactionService.SelectStocksForUserObjectParameters(customer, sellerShares);

            transactionService.StockPurchaseTransaction(seller, customer, (int)sellerShares.Price, sellerSharesToThUserEntity.AmountStocks, sellerSharesToThUserEntity, customerSharesToThUserEntity);
        }
    }
}
