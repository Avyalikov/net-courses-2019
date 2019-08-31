using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TradingSoftware.Core.Repositories;
using TradingSoftware.Core.Services;
using WebApiTradingServer.Repositories;

namespace WebApiTradingServer
{
    class Program
    {
        static void Main(string[] args)
        {   /*
             IClientRepository clientRepository = new ClientRepository();
             IClientManager clientManager = new ClientManager(clientRepository);

             ISharesRepository sharesRepository = new SharesRepository();
             IShareManager shareManager = new ShareManager(sharesRepository);

             IBlockOfSharesRepository blockOfSharesRepository = new BlockOfSharesRepository();
             IBlockOfSharesManager blockOfSharesManager = new BlockOfSharesManager(blockOfSharesRepository);

             ITransactionRepository transactionRepository = new TransactionRepository();
             ITransactionManager transactionManager = new TransactionManager(clientManager, blockOfSharesRepository, clientRepository, sharesRepository, transactionRepository);

            IDataBaseInitializer dbInitializer = new DataBaseInitializer(clientManager, shareManager, blockOfSharesManager);
            dbInitializer.Initiate();
            
            SimulationManager simManager = new SimulationManager(transactionManager, clientManager, shareManager, blockOfSharesManager);

            for (int i = 0; i < 300; i++)
            {
                simManager.MakeRandomTransaction();
            }
            */

            var host = new WebHostBuilder()
                .UseKestrel((context, options) =>
                {

                })
                .UseUrls("http://*")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }

    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientManager, ClientManager>();
            services.AddScoped<IBlockOfSharesRepository, BlockOfSharesRepository>();
            services.AddScoped<IBlockOfSharesManager, BlockOfSharesManager>();
            services.AddScoped<IBlockOfSharesRepository, BlockOfSharesRepository>();
            services.AddScoped<IShareManager, ShareManager>();
            services.AddScoped<ISharesRepository, SharesRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<ITransactionManager, TransactionManager>();

            //services.AddSingleton<ISampleBusinessService, SampleBusinessService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
