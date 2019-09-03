namespace TradingAppWebAPI
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:9000/";

            string exitKey = "e";
            string userInput = "";
            
            using (var webApp = Microsoft.Owin.Hosting.WebApp.Start<StartUp>(url: baseAddress))
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