namespace WebApiTradingServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using TradingSoftware.Core.Repositories;
    using TradingSoftware.Core.Services;
    using WebApiTradingServer.Repositories;
    using WebApiTradingServer.Services;

    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
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
            services.AddScoped<ILoggerService, LoggerService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
