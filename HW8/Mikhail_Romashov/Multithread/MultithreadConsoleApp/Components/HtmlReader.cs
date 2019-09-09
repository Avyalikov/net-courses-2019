using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Components
{
    public static class HtmlReader
    {
        public static async Task<string> ReadHttp(string url)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = null;
                int iteration = 0;
                bool isResponsed = false;
                while (!isResponsed && iteration < 10)
                {
                    try
                    {
                        iteration++;
                        response = await httpClient.GetAsync(url);
                        isResponsed = true;
                    }
                    catch (HttpRequestException)
                    {
                        Thread.Sleep(10);
                    }
                    catch
                    {
                        throw;
                    }
                }
                if (response == null)
                {
                    throw new Exception("Cannot get answer from website");
                }
                if (response.IsSuccessStatusCode)
                {
                    var pageContents = await response.Content.ReadAsStringAsync();
                    return pageContents;
                }
                return null;
            }
        }
    }
}



        