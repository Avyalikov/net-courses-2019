using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Components
{
    public static class HtmlReader
    {
        public static async Task<string> ReadHttp(string url)
        {
            string result = string.Empty;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    result = await httpClient.GetStringAsync(url);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return result;
        }
    }
}
   