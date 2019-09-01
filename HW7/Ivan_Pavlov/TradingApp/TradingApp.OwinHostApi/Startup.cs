namespace TradingApp.OwinHostApi
{
    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;
    using Owin;
    using System;
    using System.Web.Http;
    using TradingApp.Core;
    using TradingApp.Core.ProxyForServices;
    using TradingApp.Core.Repositories;
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.OwinHostApi.Repositories;

    public class Startup
    {
        public void Configuration(IAppBuilder appBuild)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "tradingApi",
                routeTemplate: "api/{controller}/{action}/{id}",             
                defaults: new { id = RouteParameter.Optional,
                action = RouteParameter.Optional});
            appBuild.UseNinjectMiddleware(CreateKernel);
            appBuild.UseNinjectWebApi(config);
            appBuild.UseWebApi(config);
        }

        private IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IUserTableRepository>().To<UserTableRepository>();
            kernel.Bind<IPortfolioTableRepository>().To<PortfolioTableRepository>();
            kernel.Bind<IShareTableRepository>().To<ShareTableRepository>();
            kernel.Bind<ITransactionTableRepository>().To<TransactionTableRepository>();
            kernel.Bind<TradingAppDb>().To<TradingAppDb>();
            kernel.Bind<IUsersService>().To<UsersProxy>();
            kernel.Bind<IShareServices>().To<ShareProxy>();
            kernel.Bind<IPortfolioServices>().To<PortfolioProxy>();
            kernel.Bind<ITransactionServices>().To<TransactionProxy>();
            return kernel;
        }
    }
}
