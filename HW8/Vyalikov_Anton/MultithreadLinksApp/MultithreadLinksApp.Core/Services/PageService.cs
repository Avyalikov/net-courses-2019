namespace MultithreadLinksApp.Core.Services
{
    using Interfaces;
    using System;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class PageService : IPageService
    {
        private readonly HttpClient httpClient;
        private int bufferSize = 0x2000 * 1024;
        private readonly Random random;

        public PageService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.random = new Random();
        }

        public async Task<string> DownloadPage(string url)
        {
            HttpResponseMessage response = null;
            int currentTry = 0;
            bool isResponsed = false;
            while (!isResponsed && currentTry < 100)
            {
                try
                {
                    currentTry++;
                    response = await httpClient.GetAsync(url);
                    isResponsed = true;
                }

                catch (HttpRequestException)
                {
                    Thread.Sleep(10);
                }
            }

            if (response is null)
            {
                throw new HttpRequestException("Impossible to reach website.");
            }

            if (response.IsSuccessStatusCode)
            {
                var filename = await SaveToFile(url, response);
                return filename;
            }

            throw new HttpRequestException($"{response.StatusCode}, {response.ReasonPhrase}");
        }

        private async Task<string> SaveToFile(string url, HttpResponseMessage response)
        {
            string filename = url + random.Next();
            filename = new string(filename.Where(c => !char.IsPunctuation(c)).ToArray());
            using (Stream stream = await response.Content.ReadAsStreamAsync(), 
                   fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                await stream.CopyToAsync(stream);
            }

            if (File.Exists(filename))
            {
                return filename;
            }

            throw new Exception("Impossible to create file.");
        }

        public string ReadPageFromFile(string path)
        {
            string page = File.ReadAllText(path);
            File.Delete(path);
            return page;
        }
    }
}
