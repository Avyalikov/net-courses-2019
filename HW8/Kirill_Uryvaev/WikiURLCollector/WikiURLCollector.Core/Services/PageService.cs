using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WikiURLCollector.Core.Interfaces;

namespace WikiURLCollector.Core.Services
{
    public class PageService : IPageService
    {
        private int numberOfTries = 100;
        private readonly HttpClient client;
        private readonly Random uniformRandom;
        private int bufferSize = 0x2000 * 1024;

        public PageService(HttpClient client)
        {
            this.client = client;
            uniformRandom = new Random();
        }
        public async Task<string> DownloadPageIntoFile(string address)
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
                throw new HttpRequestException("Cannot get answer from website");
            }
            if (response.IsSuccessStatusCode)
            {
                var filename = await saveToFile(address, response);
                return filename;
            }
            throw new HttpRequestException($"{response.StatusCode} {response.ReasonPhrase}");
        }

        public string ReadPageFile(string filePath)
        {
            string page = File.ReadAllText(filePath);
            File.Delete(filePath);
            return page;
        }

        private async Task<string> saveToFile(string address, HttpResponseMessage response)
        {
            string filename = address + uniformRandom.Next();
            filename = new string(filename.Where(c => !char.IsPunctuation(c)).ToArray());
            using (Stream contentStream = await response.Content.ReadAsStreamAsync(),
                    stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true))
            {
                await contentStream.CopyToAsync(stream);
            }
            if (File.Exists(filename))
            {
                return filename;
            }
            throw new Exception("Cannot create file");
        }
    }
}

