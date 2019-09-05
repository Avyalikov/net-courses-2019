using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiURLCollector.Core.Models;

namespace WikiURLCollector.Core.Services
{
    public class PageDownloadingService
    {
        private int numberOfTries = 1000;

        public async Task<string> GetPage(string adress)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = null;
                int currentTry = 0;
                bool isResponsed = false;
                while (!isResponsed && currentTry < numberOfTries)
                {
                    try
                    {
                        currentTry++;
                        response = await client.GetAsync(adress);
                        isResponsed = true;
                    }
                    catch (HttpRequestException)
                    {
                        continue;
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
            }
            return null;
        }
    }
}
