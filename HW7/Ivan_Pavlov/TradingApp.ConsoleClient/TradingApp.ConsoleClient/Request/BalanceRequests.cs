namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;

    public static class BalancesRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUsersBalance(int userId)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{baseUrl}api/balances?userId={userId}");
            var jsonString = await response.Content.ReadAsStringAsync();

            var result = JsonParser.ParseBalance(jsonString);
            foreach(var res in result)
            {
                Console.Write($"{res} ");
            }
        }
      
        public static void Update(int usId, decimal value)
        {
            var client = new HttpClient();
            var response = client.PutAsJsonAsync($"{baseUrl}api/clients/update/{usId}", value);
        }
    }
}
