using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.Repository.Context;
using Trading.Repository.Repositories;

namespace Trading.WebApi
{
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
            services.AddScoped<IClientTableRepository, ClientTableRepository>();
            services.AddScoped<IClientSharesTableRepository, ClientSharesTableRepository>();
            services.AddScoped<ISharesTableRepository, SharesTableRepository>();
            services.AddScoped<ITransactionHistoryTableRepository, TransactionHistoryTableRepository>();
            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ISharesService, SharesService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            services.AddScoped<TradesEmulatorDbContext, TradesEmulatorDbContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMvc();
        }
    }
}
