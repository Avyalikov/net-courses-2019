using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HW7.Core;
using HW7.Core.Repositories;
using HW7.Core.Services;
using HW7.Server.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HW7.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        #region snippet_ConfigureServices
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IContextProvider>(_ => new TradingContext(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<ITradersRepository>(_ => new TradersRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSingleton<ISharesRepository>(_ => new SharesRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSingleton<ITransactionsRepository>(_ => new TransactionsRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddSingleton<TradersService>(_ => new TradersService(new TradersRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection")))));
            services.AddSingleton<SharesService>(_ => new SharesService(new SharesRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection")))));
            services.AddSingleton<TransactionsService>(_ => new TransactionsService(
                new TransactionsRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))),
                new TradersRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))),
                new SharesRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection"))),
                new PortfoliosRepository(new TradingContext(Configuration.GetConnectionString("DefaultConnection")))
                ));
        }
        #endregion 
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
