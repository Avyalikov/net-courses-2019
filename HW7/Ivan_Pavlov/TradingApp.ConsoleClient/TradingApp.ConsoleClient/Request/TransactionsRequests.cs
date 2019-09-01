namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsonModels;

    public class TransactionsRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUserTransactions(int userId, int top)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{baseUrl}api/transaction?userId={userId}&top={top}");
            var jsonString = await response.Content.ReadAsStringAsync();

            var transactions = JsonParser.ParseColleciont<JsonTransactionStory>(jsonString);
            foreach (var t in transactions)
            {
                Console.WriteLine(t.ToString());
            }
        }
    }
}
