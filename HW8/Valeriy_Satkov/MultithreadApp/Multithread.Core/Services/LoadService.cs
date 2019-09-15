namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using HtmlAgilityPack;
    using Multithread.Core.Models;
    using Multithread.Core.Repositories;

    public class LoadService
    {
        private IFileManager fileManager;

        public LoadService(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public async Task<string> DownloadPage(string link, HttpMessageHandler handler, int id)
        {
            if (handler == null)
            {
                var defaultClientHandler = new HttpClientHandler();
                defaultClientHandler.UseDefaultCredentials = true;
                handler = defaultClientHandler;
            }

            string filePath = $@"LinkFiles\{id}.txt";

            using (var client = new HttpClient(handler))
            {
                using (var response = await client.GetAsync(link))
                {
                    using (var content = response.Content)
                    {
                        var jsonString = await content.ReadAsStringAsync();

                        string result = await LoadIntoFile(filePath, jsonString);

                        return filePath;                      
                    }
                }
            }
        }

        public async Task<string> LoadIntoFile(string filePath, string jsonString)
        {
            using (FileStream fstream = this.fileManager.FileStream(filePath, FileMode.OpenOrCreate))
            {
                // convert string to bytes
                byte[] array = System.Text.Encoding.Default.GetBytes(jsonString);
                // record byte array to file
                await fstream.WriteAsync(array, 0, array.Length);

                return filePath;
            }
        }

        public string LoadFromFile (string htmlContentFilePath)
        {
            string content;

            using (StreamReader sr = this.fileManager.StreamReader(htmlContentFilePath))
            {
                content = sr.ReadToEnd();
            }

            return content;
        }

        public string RemoveFile(string filePath)
        {
            FileInfo fileInf = new FileInfo(filePath);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }

            return "";
        }

    }
}
