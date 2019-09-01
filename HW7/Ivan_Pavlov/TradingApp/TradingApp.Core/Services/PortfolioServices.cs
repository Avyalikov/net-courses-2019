namespace TradingApp.Core.Services
{
    using System;
    using System.Linq;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class PortfolioServices : IPortfolioServices
    {
        private readonly IPortfolioTableRepository repo;

        public PortfolioServices(IPortfolioTableRepository repo)
        {
            this.repo = repo;
        }

        public void AddNewUsersShares(PortfolioInfo args)
        {
            var PortfolioToAdd = new PortfolioEntity()
            {
                UserEntityId = args.UserId,
                ShareId = args.ShareId,
                Amount = args.Amount
            };

            repo.AddNewUsersShares(PortfolioToAdd);
            repo.SaveChanges();
        }

        public void ChangeAmountOfShares(PortfolioEntity args, int value)
        { 
            if ((args.Amount = args.Amount + value) < 0)
                throw new ArgumentException("У продавца не хватает акций");
            repo.SaveChanges();
        }      
    }
}
