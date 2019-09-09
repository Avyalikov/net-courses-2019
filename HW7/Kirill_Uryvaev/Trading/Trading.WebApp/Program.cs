using Microsoft.Owin.Hosting;
using Microsoft.Owin;
using Owin;
using System;
using System.Net.Http;
using StructureMap;
using System.Web.Http;
using System.Threading.Tasks;

namespace Trading.WebApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            string exitKey = "e";
            string userInput = "";
            // Start OWIN host 
            using (var webApp = Microsoft.Owin.Hosting.WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"{DateTime.Now} Server started");
                while (!userInput.ToLower().Equals(exitKey))
                {
                    userInput = Console.ReadLine();
                }

                
            }
        }
    }
}
