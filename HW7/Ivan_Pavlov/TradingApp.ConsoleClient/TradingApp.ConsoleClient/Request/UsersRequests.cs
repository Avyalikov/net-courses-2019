namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsModels;

    public static class UsersRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintAllUsers(int top, int page = 1)
        {
            var client = new HttpClient();
            var responce = await client.GetAsync($"{baseUrl}api/clients?top={top}&page={page}");
            var jsonString = await responce.Content.ReadAsStringAsync();

            var users = JsParser.ParseUsers(jsonString);
            foreach (var u in users)
            {
                Console.WriteLine(u.ToString());
                foreach(var portf in u.UsersShares)
                {
                    Console.WriteLine(portf.ToString());
                }
            }
        }

        public static void Add()
        {
            var user = new JsUser()
            {
                Name = "testFromClient",
                SurName = "rasdasdqwe",
                Balance = 2000,
                Phone = "800000000000"
            };
            var client = new HttpClient();
            var responce = client.PostAsJsonAsync($"{baseUrl}api/clients/add/", user);
        }

        public static void Update(int userId)
        {
            var user = new JsUser()
            {
                Name = "testUpdate"
            };
            var client = new HttpClient();
            var responce = client.PutAsJsonAsync($"{baseUrl}api/clients/update/{userId}", user);
        }

        public static void Delete(int userId)
        {
            var client = new HttpClient();
            var responce = client.DeleteAsync($"{baseUrl}api/clients/remove/{userId}");
        }
    }
}
