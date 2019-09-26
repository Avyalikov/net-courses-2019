namespace WebApiTradingServer
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    
    public class Program
    {
        public static void Main(string[] args)
        {  
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
}
