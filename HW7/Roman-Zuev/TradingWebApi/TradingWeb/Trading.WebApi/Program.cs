using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.WebApi
{
    class Program
    {
        static void Main()
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseKestrel((context, options) =>
                {

                })
                .UseUrls("http://*:80")
                .Build();

            host.Run();
        }
    }
}
