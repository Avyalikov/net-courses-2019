using Trading.Core;
using SharedContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedContext.DAL;

namespace TradingSHWebAPI.Extensions
{
    public static class ServiceExtensions
    {
            

        public static void ConfigureSqlServerContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=StockExchangeW;Trusted_Connection=True;";
            services.AddDbContext<ExchangeContext>(o => o.UseSqlServer(connectionString));
        }

        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

    }
}
