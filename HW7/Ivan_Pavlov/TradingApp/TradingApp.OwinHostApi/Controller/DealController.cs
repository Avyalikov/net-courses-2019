namespace TradingApp.OwinHostApi.Controller
{
    using Newtonsoft.Json;
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
            var args = JsonConvert.DeserializeObject<TransactionStoryInfo>(json.ToString());
            var share = shareServices.GetShareById(args.shareId);         
            if (share == null)
                return;
            //args.Share = share; // из-за этого добавляется в табл с акциями новая акция
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
