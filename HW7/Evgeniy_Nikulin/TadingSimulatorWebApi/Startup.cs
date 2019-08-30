namespace TadingSimulatorWebApi
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using StructureMap;
    using TradingSimulator.Core;
    using TradingSimulator.Core.Interfaces;
    using TradingSimulator.Core.Repositories;
    using TradingSimulator.Core.Services;
    using TradingSimulator.DataBase;
    using TradingSimulator.DataBase.Repositories;
    using TradingSimulatorWebApi.Components;
    using Microsoft.Extensions.Logging;
    using log4net;

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
            services.AddControllers();
        }

        // Use StructureMap-specific APIs to register services in the registry.
        public void ConfigureContainer(Registry registry)
        {
            registry.For<IPhraseProvider>().Use<JsonPhraseProvider>();
            registry.For<ISettingsProvider>().Use<SettingsProvider>();
            registry.For<ILoggerService>().Use<LoggerService>();
            registry.For<ITraderRepository>().Use<TraderRepository>();
            registry.For<IShareRepository>().Use<ShareRepository>();
            registry.For<ITransactionRepository>().Use<TransactionRepository>();
            registry.For<ITraderService>().Use<TraderService>();
            registry.For<IShareService>().Use<ShareService>();
            registry.For<ITransactionService>().Use<TransactionService>();
            registry.For<TradingDbContext>().Use<TradingDbContext>();

            registry.For<GameSettings>().Use(context => context.GetInstance<ISettingsProvider>().Get());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddLog4Net();
        }
    }
}