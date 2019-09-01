namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;

    public class TransactionsRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUserTransactions(int userId, int top)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync($"{baseUrl}api/transaction?userId={userId}&top={top}");
            var jsonString = await responce.Content.ReadAsStringAsync();

            var transactions = JsParser.ParseTransaction(jsonString);
            foreach (var t in transactions)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
