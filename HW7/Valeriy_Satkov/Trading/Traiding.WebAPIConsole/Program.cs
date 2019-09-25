namespace Traiding.WebAPIConsole
{
    using Microsoft.Owin.Hosting;
    using StructureMap;
    using System;
    using System.Data.Entity;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.SelfHost;
    using Traiding.Core.Services;

    class Program
    {
        static void Main(string[] args)
        {
            // https://docs.microsoft.com/ru-ru/aspnet/web-api/overview/hosting-aspnet-web-api/use-owin-to-self-host-web-api
            string baseAddress = "http://localhost:52804";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"{DateTime.Now} Server started");

                string inputString;
                do
                {
                    //Console.Clear();
                    Console.Write("Type 'e' for exit and press Enter: ");

                    inputString = Console.ReadLine();
                } while (!inputString.ToLowerInvariant().Equals("e"));
            }

            //var config = new HttpSelfHostConfiguration("http://localhost:52804");

            //config.Routes.MapHttpRoute(
            //    "ActionsAPI", "{controller}/{action}");
            //config.Routes.MapHttpRoute(
            //    "ReportsAPI", "{controller}");

            //using (HttpSelfHostServer server = new HttpSelfHostServer(config))
            //{
            //    //Console.WriteLine($"{DateTime.Now} Server started");

            //    var salesService = new Container(new Models.DependencyInjection.TraidingRegistry()).GetInstance<SalesService>();
            //    var firstOp = salesService.CreateOperation();
            //    salesService.RemoveOperation(firstOp);

            //    server.OpenAsync().Wait();   

            //    string inputString;
            //    do
            //    {
            //        Console.Clear();
            //        Console.Write("Type 'e' for exit and press Enter: ");

            //        inputString = Console.ReadLine();
            //    } while (!inputString.ToLowerInvariant().Equals("e"));



            //    //Console.WriteLine("Press Enter to quit.");
            //    //Console.ReadLine();
            //}
        }
    }
}
