namespace TradingApp.ConsoleClient
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using TradingApp.ConsoleClient.JsModels;
    using TradingApp.ConsoleClient.Request;

    class Program
    {
        static void Main(string[] args)
        {
            //UsersRequests.Add();

            //UsersRequests.Update(2);

            //UsersRequests.PrintAllUsers(10);

            //UsersRequests.Delete(3);

            //SharesRequests.PrintUsersShares(2);

            //BalancesRequests.PrintUsersBalance(4);

            //SharesRequests.Update(1);

            //TransactionsRequests.PrintUserTransactions(1, 10);

            //BalancesRequests.Update(2, -200);

            Random rnd = new Random();
          //  while (true)
          //  {
                //JsTransactionStory transaction = new JsTransactionStory()
                //{
                //    SellerId = rnd.Next(1, 8),
                //    CustomerId = rnd.Next(1, 8),
                //    ShareId = rnd.Next(1, 8),
                //    AmountOfShares = rnd.Next(100, 200),
                //    DateTime = DateTime.Now
                //};

                JsTransactionStory transaction = new JsTransactionStory()
                {
                    SellerId = 2,
                    CustomerId = 3,
                    ShareId = 2,
                    AmountOfShares = 1,
                    DateTime = DateTime.Now
                };

                DealMakerRequests.DealMaker(transaction);
                Console.WriteLine("+");
                Thread.Sleep(2000);
            //}

            Console.ReadKey();
        }
    }
}
