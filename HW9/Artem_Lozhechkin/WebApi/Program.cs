namespace TradingApp.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;

    class Program
    {
        static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel((context, options) => { })
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
