using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;

namespace WikiURLCollector.Core.Services
{
    public class PageDownloadingService : IPageDownloadingService
    {
        private int numberOfTries = 100;
        private readonly HttpClient client;

        public PageDownloadingService()
        {
            this.client = new HttpClient();
        }
        public PageDownloadingService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<string> GetPage(string address)
        {
            HttpResponseMessage response = null;
            int currentTry = 0;
            bool isResponsed = false;
            while (!isResponsed && currentTry < numberOfTries)
            {
                try
                {
                    currentTry++;
                    response = await client.GetAsync(address);
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

