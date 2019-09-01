namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsModels;

    public static class SharesRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintUsersShares(int clientid)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync($"{baseUrl}api/shares?clientId={clientid}");
            var jsonString = await responce.Content.ReadAsStringAsync();
         
            var shares = JsParser.ParseShares(jsonString);
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
            var share = new JsShare()
            {
                Name = "testFromClient",
                Price = 100

            };
            var client = new HttpClient();
            var responce = client.PostAsJsonAsync($"{baseUrl}api/shares/add/", share);
        }

        public static void Update(int shareId)
        {
            var share = new JsShare()
            {
                Name = "testUpdate",
                Price = 100

            };
            var client = new HttpClient();
            var responce = client.PutAsJsonAsync($"{baseUrl}api/shares/update/{shareId}", share);
        }

        public static void Delete(int shareId)
        {
            var client = new HttpClient();
            var responce = client.DeleteAsync($"{baseUrl}api/shares/remove/{shareId}");
        }
    }
}
