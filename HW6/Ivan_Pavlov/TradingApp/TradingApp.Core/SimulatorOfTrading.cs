namespace TradingApp.Core
{
    using System;
    using System.Linq;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;

    public class SimulatorOfTrading
    {
        private readonly IUsersService usersService;
        private readonly IUserTableRepository userRepo;
        private readonly IPortfolioServices portfolioService;
        private readonly ITransactionServices transaction;

        public SimulatorOfTrading(IUsersService usersService, IUserTableRepository userRepo, IPortfolioServices portfolio, ITransactionServices transaction)
        {
            this.usersService = usersService;
            this.userRepo = userRepo;
            this.portfolioService = portfolio;
            this.transaction = transaction;
        }

        public void StartTrading()
        {
            Random random = new Random();
            var seller = usersService.GetSeller(random.Next(1, userRepo.Count()));
            var customer = usersService.GetCustomer(random.Next(1, userRepo.Count()), seller.Id);

            if (seller == null || customer == null) //тут проблема что исключение обрабатывается в прокси, а сам прокси в таком случае кидает null, знаю что костыль
                return;

            var sellerPortfolioForTrade = seller.UsersShares.ToList()[random.Next(0, seller.UsersShares.Count())];
            var amountSharesForTrade = random.Next(1, sellerPortfolioForTrade.Amount / 5);
            var shareForTrade = sellerPortfolioForTrade.Share;
            decimal CostOfTransaction = amountSharesForTrade * shareForTrade.Price;

            usersService.ChangeUserBalance(seller.Id, CostOfTransaction);
            usersService.ChangeUserBalance(customer.Id, CostOfTransaction * -1);

            PortfolioEntity customerPortfolio;
            if ((customerPortfolio = customer.UsersShares.Where(us => us.ShareId == shareForTrade.Id).FirstOrDefault()) == null)
            {
                PortfolioInfo customerPortfolioToAdd = new PortfolioInfo()
                {
                    UserId = customer.Id,
                    ShareId = shareForTrade.Id,
                    Amount = amountSharesForTrade
                };
                portfolioService.AddNewUsersShares(customerPortfolioToAdd);
            }
            else
                portfolioService.ChangeAmountOfShares(customerPortfolio, amountSharesForTrade);
            portfolioService.ChangeAmountOfShares(sellerPortfolioForTrade, amountSharesForTrade * -1);

            TransactionStoryInfo transactionOperation = new TransactionStoryInfo()
            {
                customerId = customer.Id,
                sellerId = seller.Id,
                shareId = shareForTrade.Id,
                Share = shareForTrade, 
                AmountOfShares = amountSharesForTrade,
                DateTime = DateTime.Now,
                TransactionCost = CostOfTransaction
            };

            transaction.AddNewTransaction(transactionOperation);
        }
    }
}
