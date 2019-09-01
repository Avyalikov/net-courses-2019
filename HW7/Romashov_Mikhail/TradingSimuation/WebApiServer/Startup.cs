using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;
using System.Configuration;
using TradingSimulator.Core.Repositories;
using TradingSimulator.Core.Services;
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
            registry.For<ITraderService>().Use<TradersService>();
            registry.For<TradingSimulatorDBContext>().Use<TradingSimulatorDBContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["tradingSimulatorConnectionString"].ConnectionString);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}