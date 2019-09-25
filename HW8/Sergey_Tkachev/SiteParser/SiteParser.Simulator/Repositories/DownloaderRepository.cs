namespace SiteParser.Simulator.Repositories
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using System.Threading;
    using SiteParser.Core.Repositories;

    internal class DownloaderRepository : IDownloader
    {
        private static readonly object Locker = new object();
        private static readonly HttpClient Client = new HttpClient();
        private readonly string folder = "Pages";
        private readonly Random random;
        private string pathToFile = string.Empty;

        public DownloaderRepository()
        {
            this.random = new Random();
        }

        /// <summary>
        /// Downloads page content by Url adress.
        /// </summary>
        /// <param name="requestUrl">Page adress to download.</param>
        /// <returns></returns>
        public string Download(string requestUrl)
        {
            // why httpClient wasn't disposed
            // https://aspnetmonsters.com/2016/08/2016-08-27-httpClientwrong/
           
            HttpResponseMessage response = null;
            try
            {
                response = Client.GetAsync(requestUrl).Result;
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException iex)
                {
                    if (iex.CancellationToken.IsCancellationRequested)
                    {
                        // Task was canceled by something
                    }

                    // the request timed out
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

        /// <summary>
        /// Saves the string into file with random name.
        /// </summary>
        /// <param name="downloadedResult">String to save.</param>
        /// <returns></returns>
        public string SaveIntoFile(string downloadedResult)
        {
            this.pathToFile = this.DirectoryCheck(this.folder);

            string fileName = this.random.Next(1000000).ToString();
            string fullPath = Path.Combine(this.pathToFile, fileName);

            lock (Locker)
            {
                if (File.Exists(fullPath))
                {
                    try
                    {
                        throw new ArgumentException($"Such file {fullPath} already exists!");
                    }
                    catch (ArgumentException ex)
                    {
                        return null;
                    }
                }
           
                File.WriteAllText(fullPath, downloadedResult, Encoding.UTF8);
            }

            return fullPath;
        }

        /// <summary>
        /// Checks existing of directory. Creates if not. Returns its full path.
        /// </summary>
        /// <param name="folderName">Name of directory</param>
        /// <returns></returns>
        private string DirectoryCheck(string folderName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string fullpath = Path.Combine(path, folderName);

            bool exists = Directory.Exists(fullpath);

            if (!exists)
            {
                Directory.CreateDirectory(folderName);
            }

            return fullpath;
        }
    }
}
