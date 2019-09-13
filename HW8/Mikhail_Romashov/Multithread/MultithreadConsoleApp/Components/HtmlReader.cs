using MultithreadConsoleApp.Interfaces;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Components
{
    public class HtmlReader : IHtmlReader
    {
        HttpClient httpClient;
        public HtmlReader(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<string> ReadHttp(string url)
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





        