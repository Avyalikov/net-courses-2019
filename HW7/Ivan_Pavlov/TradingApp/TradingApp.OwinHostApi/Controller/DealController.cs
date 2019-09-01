namespace TradingApp.OwinHostApi.Controller
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Linq;
    using System.Web.Http;
    using TradingApp.Core;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;

    public class DealController : ApiController
    {
        private readonly IUsersService usersService;
        private readonly IUserTableRepository userRepo;
        private readonly IPortfolioServices portfolioService;
        private readonly ITransactionServices transaction;
        private readonly IShareServices shareServices;

        public DealController(IUsersService usersService, IUserTableRepository userRepo, IPortfolioServices portfolio, ITransactionServices transaction, IShareServices shareServices)
        {
            this.usersService = usersService;
            this.userRepo = userRepo;
            this.portfolioService = portfolio;
            this.transaction = transaction;
            this.shareServices = shareServices;
        }

        [ActionName("make")]
        public void PostMake(JObject json)
        {
            TransactionStoryInfo args = new TransactionStoryInfo()
            {
                sellerId = json.Value<int>("SellerId"),
                customerId = json.Value<int>("CustomerId"),
                shareId = json.Value<int>("ShareId"),
                DateTime = json.Value<DateTime>("DateTime"),
                AmountOfShares = json.Value<int>("AmountOfShares"),
            };
            var share = shareServices.GetShareById(args.shareId);         
            if (share == null)
                return;
           // args.Share = share; // из-за этого добавляется в табл с акциями новая акция
            args.TransactionCost = share.Price * args.AmountOfShares;
            try
            {
                transaction.AddShareInPortfolio(args); 
            }
            catch(ArgumentException)
            {
                return;
            }

            transaction.AddNewTransaction(args);
        }   
    }
}
