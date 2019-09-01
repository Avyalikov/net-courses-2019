namespace TradingApp.ConsoleClient
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public static class JsonParser
    {
        public static ICollection<T> ParseColleciont<T>(string jString)
        {
            return JsonConvert.DeserializeObject<List<T>>(jString);
        }

        public static string[] ParseBalance(string jString)
        {
            return JsonConvert.DeserializeObject<string[]>(jString);
        }
    }
}
