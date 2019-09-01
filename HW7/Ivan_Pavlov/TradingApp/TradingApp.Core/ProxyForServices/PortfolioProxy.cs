namespace TradingApp.Core.ProxyForServices
{
    using TradingApp.Core.Dto;
    using TradingApp.Core.Models;
    using TradingApp.Core.Services;
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.Core.Logger;
    using System;

    public class PortfolioProxy : IPortfolioServices
    {
        private readonly PortfolioServices portfolioServices;

        public PortfolioProxy(PortfolioServices portfolioServices)
        {
            this.portfolioServices = portfolioServices;
        }

        public void AddNewUsersShares(PortfolioInfo args)
        {
            this.portfolioServices.AddNewUsersShares(args);
            Logger.Log.Info($"У пользователя новый тип акций {args.ShareId}");
        }

        public void ChangeAmountOfShares(PortfolioEntity args, int value)
        {
            try
            {
                this.portfolioServices.ChangeAmountOfShares(args, value);
                Logger.Log.Info($"У пользователя изменено количество акций {args.Share.Name} на {value} шт.");
            }
            catch(ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
        }
    }
}
