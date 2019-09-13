namespace WikipediaParser.Services
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    using WikipediaParser.DTO;

    public class DownloadingService : IDownloadingService
    {
        private readonly HttpClient client;

        public DownloadingService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<string> DownloadSourceToFile(LinkInfo link)
        {
            Random r = new Random();
            string filename = link.Level + " " + link.Url.GetHashCode() + " " + r.Next() + ".html";
            bool isSucceded = false;
            do
            {
                try
                {
                    using (HttpResponseMessage response = await this.client.GetAsync(link.Url))
                    {
                        using (HttpContent content = response.Content)
                        {
                            using (StreamWriter file = File.CreateText(filename))
                            {
                                file.Write(await content.ReadAsStringAsync());
                                isSucceded = true;
                            }
                        }
                    }

                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Got a timeout - Trying to reconnect");
                    await Task.Delay(10000);
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException("Resource not found", ex);
                }
            } while (!isSucceded);
            return filename;
        }
        public async Task<LinkInfo> DownloadSourceAsString(LinkInfo link)
        {
            bool isSucceded = false;
            do
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        using (HttpResponseMessage response = await httpClient.GetAsync(link.Url))
                        {
                            using (HttpContent content = response.Content)
                            {
                                link.Content = await content.ReadAsStringAsync();
                                isSucceded = true;

                            }
                        }
                    }
                }
                catch (HttpRequestException)
                {
                    Console.WriteLine("Got a timeout - Trying to reconnect");
                    await Task.Delay(10000);
                }
                catch (NullReferenceException ex)
                {
                    throw new NullReferenceException("Resource not found", ex);
                }
            } while (!isSucceded);
            return link;
        }
    }
}
