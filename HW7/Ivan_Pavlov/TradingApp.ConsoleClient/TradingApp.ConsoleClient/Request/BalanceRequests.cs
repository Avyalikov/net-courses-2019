namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsModels;

    public static class BalancesRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUsersBalance(int userId)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync($"{baseUrl}api/balances?userId={userId}");
            var jsonString = await responce.Content.ReadAsStringAsync();

            var result = JsParser.ParseBalance(jsonString);
            Console.WriteLine(result);
        }
      
        public static void Update(int usId, decimal value)
        {
            var bal = new JsBalance()
            {
                Balance = value
            };
            var client = new HttpClient();
            var responce = client.PutAsJsonAsync($"{baseUrl}api/clients/update/{usId}", bal);
        }
    }
}
