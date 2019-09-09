namespace TradingApp.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionTableRepository repo;
        private readonly IUsersService usersService;
        private readonly IPortfolioServices portfolio;

        public TransactionServices(ITransactionTableRepository repo, IUsersService usersService, IPortfolioServices portfolio)
        {
            this.repo = repo;
            this.usersService = usersService;
            this.portfolio = portfolio;
        }

        public int AddNewTransaction(TransactionStoryInfo args)
        {
            var TransToAdd = new TransactionStoryEntity()
            {
                AmountOfShares = args.AmountOfShares,
                CustomerId = args.customerId,
                SellerId = args.sellerId,
                ShareId = args.shareId,
                DateTime = args.DateTime,
                TransactionCost = args.TransactionCost,
                Share = args.Share
            };

            repo.Add(TransToAdd);

            repo.SaveChanges();

            return TransToAdd.Id;
        }

        public List<TransactionStoryEntity> GetUsersTransaction(int userId)
        {
            return this.repo.GetTransactionsByUserId(userId);
        }

        public void AddShareInPortfolio(TransactionStoryInfo args) 
        {
            var customer = usersService.GetUserById(args.customerId);
            var seller = usersService.GetUserById(args.sellerId);
            if (seller == null || customer == null) //тут залогировано все в прокси, но сами прокси возращают null
                throw new ArgumentException("ОШИБКА ТРАНЗАКЦИИ: Неверный клиенты");

            if (seller.UsersShares.Where(us => us.ShareId == args.shareId).FirstOrDefault() == null)
                throw new ArgumentException($"ОШИБКА ТРАНЗАКЦИИ: Акция с id {args.shareId} не существует");

            var customerPortfolio = customer.UsersShares.Where(us => us.ShareId == args.shareId).FirstOrDefault();
            try
            {
                portfolio.ChangeAmountOfShares(seller.UsersShares.Where(us => us.ShareId == args.shareId).FirstOrDefault(), args.AmountOfShares * -1); 
            }
            catch (ArgumentException ex) { throw ex; } 

            if (customerPortfolio == null)
                portfolio.AddNewUsersShares(new PortfolioInfo() { UserId = args.customerId, ShareId = args.shareId, Amount = args.AmountOfShares });
            else
                portfolio.ChangeAmountOfShares(customerPortfolio, args.AmountOfShares);

            usersService.ChangeUserBalance(args.customerId, args.TransactionCost * -1); // в данной программе нет ограничения на минусовой баланс
            usersService.ChangeUserBalance(args.sellerId, args.TransactionCost);
        }
    }
}
