namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsModels;

    public static class DealMakerRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static void DealMaker(JsTransactionStory transaction)
        {           
            var client = new HttpClient();
            var responce = client.PostAsJsonAsync($"{baseUrl}api/deal/make", transaction);
        }
    }
}
