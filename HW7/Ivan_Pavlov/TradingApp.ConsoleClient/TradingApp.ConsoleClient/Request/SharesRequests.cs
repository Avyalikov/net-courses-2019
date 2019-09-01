namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsonModels;

    public static class SharesRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUsersShares(int clientid)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{baseUrl}api/shares?clientId={clientid}");
            var jsonString = await response.Content.ReadAsStringAsync();
         
            var shares = JsonParser.ParseColleciont<JsonShare>(jsonString);
            if (shares == null)
            {
                Console.WriteLine("У данного клиента акций нет");
                return;
            }
            Console.WriteLine("Данный клиент имеет следующие акции");
            foreach (var u in shares)
            {             
                Console.WriteLine(u.ToString());
            }
        }

        public static void Add()
        {
            var share = new JsonShare()
            {
                Name = "testFromClient",
                Price = 100

            };
            var client = new HttpClient();
            var response = client.PostAsJsonAsync($"{baseUrl}api/shares/add/", share);
        }

        public static void Update(int shareId)
        {
            var share = new JsonShare()
            {
                Name = "testUpdate",
                Price = 100

            };
            var client = new HttpClient();
            var response = client.PutAsJsonAsync($"{baseUrl}api/shares/update/{shareId}", share);
        }

        public static void Delete(int shareId)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync($"{baseUrl}api/shares/remove/{shareId}");
        }
    }
}
