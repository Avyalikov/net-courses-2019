using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace WikipediaParser.Services
{
    public class DownloadingService
    {
        public DownloadingService()
        {
        }
        public string GetHTMLSource(string url)
        {
            string result = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = client.GetAsync(url).Result)
                {
                    using (HttpContent content = response.Content)
                    {
                        result = content.ReadAsStringAsync().Result;
                    }
                }
            }
            return result;
        }
    }
}
