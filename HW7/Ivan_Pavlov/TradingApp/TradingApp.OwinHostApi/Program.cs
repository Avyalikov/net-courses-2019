namespace TradingApp.OwinHostApi
{
    using Microsoft.Owin.Hosting;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using TradingApp.Core.Dto;

    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Server runing....");
                Console.ReadKey();
            }           
        }       
    }
}
