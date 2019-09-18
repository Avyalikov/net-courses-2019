using SiteParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SiteParser.Simulator.Repositories
{
    class DownloaderRepository : IDownloader
    {
        private readonly SiteParserDbContext dbContext;
        private string pathToFile = string.Empty;
        private string folder = "Pages";
        private static HttpClient client = new HttpClient();
        static private object locker = new object();
        private Random random;

        public DownloaderRepository(SiteParserDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.random = new Random();
        }
        public string Download(string requestUrl)
        {
            //why httpclient wasn't disposed
            //https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
           
            HttpResponseMessage response = null;
            try
            {
                response = client.GetAsync(requestUrl).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    TaskCanceledException iex = (TaskCanceledException)ex.InnerException;
                    if (iex.CancellationToken.IsCancellationRequested)
                    {
                        //Task was canceled by something
                    }
                    //the request timed out
                }
                else
                {
                    throw ex.InnerException;
                }
                
                return null;
            }

            using (HttpContent content = response.Content)
            {
                string result = content.ReadAsStringAsync().Result;
                Thread.Sleep(400);
                response.Dispose();
                return result;
            }
        }

        public string SaveIntoFile(string downloadedResult)
        {
            this.pathToFile = DirectoryCheck(this.folder);

            string fileName = random.Next(1000000).ToString();
            string fullPath = Path.Combine(pathToFile, fileName);

            lock (locker)
            {
                if (File.Exists(fullPath))
                {
                    try
                    {
                        throw new ArgumentException($"Such file {fullPath} already exists!");
                    }
                    catch(ArgumentException ex)
                    {
                        return null;
                    }
                }
           
                File.WriteAllText(fullPath, downloadedResult, Encoding.UTF8);
            }

            return fullPath;
        }

        private string DirectoryCheck(string folderName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, folderName);

            bool exists = Directory.Exists(fullpath);

            if (!exists)
                Directory.CreateDirectory(folderName);

            return fullpath;
        }
    }
}
