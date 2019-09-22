using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Server.DbInit;
using Microsoft.Extensions.DependencyInjection;

namespace TradeSimulator.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel((context, options) =>
                {
                })
                .UseUrls("http://localhost:228")
                .UseStartup<Startup>()
                .Build();

            var db = host.Services.GetService<TradeSimDbContext>();
            using (db)
            {
                host.Run();
            }
        }
    }
}
