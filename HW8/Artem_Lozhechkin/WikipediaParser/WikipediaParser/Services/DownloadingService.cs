using System;
using System.Collections.Generic;
using System.IO;
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            using (StreamWriter file = File.CreateText(url.Substring(url.Length-10, url.Length)))
                            {
                                file.Write(content.ReadAsStringAsync().Result);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}
