namespace TradingApp.ConsoleClient
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using TradingApp.ConsoleClient.JsonModels;
    using TradingApp.ConsoleClient.Request;

    class Program
    {
        static void Main(string[] args)
        {
            // UsersRequests.Add();

            //UsersRequests.Update(2);

            //UsersRequests.PrintAllUsers(10);

            //UsersRequests.Delete(9);

            //SharesRequests.PrintUsersShares(2);

            //SharesRequests.Delete(10);

            //BalancesRequests.PrintUsersBalance(2);

            //SharesRequests.Update(1);

            //TransactionsRequests.PrintUserTransactions(1, 10);

            //BalancesRequests.Update(2, -200);

            Random rnd = new Random();
            while (true)
            {
                JsonTransactionStory transaction = new JsonTransactionStory()
                {
                    SellerId = rnd.Next(1, 6),
                    CustomerId = rnd.Next(1, 6),
                    ShareId = rnd.Next(1, 6),
                    AmountOfShares = rnd.Next(1, 1000),
                    DateTime = DateTime.Now
                };


                DealMakerRequests.DealMaker(transaction);
                Thread.Sleep(2000);
            }

            Console.ReadKey();
        }
    }
}
