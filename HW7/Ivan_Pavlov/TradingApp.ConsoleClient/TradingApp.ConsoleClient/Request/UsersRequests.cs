namespace TradingApp.ConsoleClient.Request
{
    using System;
    using System.Net.Http;
    using TradingApp.ConsoleClient.JsonModels;

    public static class UsersRequests
    {
        private readonly static string baseUrl = "http://localhost:9000/";

        public static async void PrintAllUsers(int top, int page = 1)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{baseUrl}api/clients?top={top}&page={page}");
            var jsonString = await response.Content.ReadAsStringAsync();

            var users = JsonParser.ParseColleciont<JsonUser>(jsonString);
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
            var user = new JsonUser()
            {
                Name = "testFromClient",
                SurName = "rasdasdqwe",
                Balance = 2000,
                Phone = "800000000000"
            };
            var client = new HttpClient();
            var response = client.PostAsJsonAsync($"{baseUrl}api/clients/add/", user);
        }

        public static void Update(int userId)
        {
            var user = new JsonUser()
            {
                Name = "testUpdate"
            };
            var client = new HttpClient();
            var response = client.PutAsJsonAsync($"{baseUrl}api/clients/update/{userId}", user);
        }

        public static void Delete(int userId)
        {
            var client = new HttpClient();
            var response = client.DeleteAsync($"{baseUrl}api/clients/remove/{userId}");
        }
    }
}
