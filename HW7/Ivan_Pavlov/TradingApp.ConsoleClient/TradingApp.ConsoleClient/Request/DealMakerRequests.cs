namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsonModels;

    public static class DealMakerRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static void DealMaker(JsonTransactionStory transaction)
        {           
            var client = new HttpClient();
            var response = client.PostAsJsonAsync($"{baseUrl}api/deal/make", transaction);
        }
    }
}
