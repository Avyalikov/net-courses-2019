using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedContext.DAL;
using Trading.Core.IServices;
using Trading.Core.Services;
using SharedContext;
using Trading.Core;
using Microsoft.EntityFrameworkCore;
using TradingSHWebAPI.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

namespace TradingSHWebAPI
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
            services.ConfigureSqlServerContext(Configuration);
            services.AddScoped<ExchangeContext>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IClientStockService, ClientStockService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

          
        }
      
        
    }
}
