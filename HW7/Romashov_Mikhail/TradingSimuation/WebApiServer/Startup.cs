using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System.Configuration;
using TradingSimulator.Core.Interfaces;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;
using WebApiServer.Components;
using WebApiServer.Interfaces;
using WebApiServer.Repositories;

namespace WebApiServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureContainer(Registry registry)
        {
            registry.For<ITraderTableRepository>().Use<TraderTableRepository>();
            registry.For<IStockTableRepository>().Use<StockTableRepository>();
            registry.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            registry.For<ITraderStockTableRepository>().Use<TraderStockTableRepository>();
            registry.For<IBankruptRepository>().Use<BankruptRepository>();
            registry.For<IHistoryTableRepository>().Use<HistoryTableRepository>();
            registry.For<ITraderService>().Use<TradersService>();
            registry.For<IStockService>().Use<StockService>();
            registry.For<IBankruptService>().Use<BankruptService>();
            registry.For<IHistoryService>().Use<HistoryService>();
            registry.For<ITraderStocksService>().Use<TraderStocksService>();
            registry.For<ISaleService>().Use<SaleService>();
            registry.For<ILogger>().Use<LoggerService>();
            registry.For<IValidator>().Use<Validator>();
            registry.For<TradingSimulatorDBContext>().Use<TradingSimulatorDBContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingSimulatorConnectionString"].ConnectionString);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}