using MultithreadApp.Core.Repositories;
using System.Net;

namespace MultithreadApp.Dependencies
{
    public class DownloadWebPageRepository : IDownloadWebPageRepository
    {
        public string DownLoadPage(string url)
        {
            try
            {
                WebClient client = new WebClient();
                string Page = client.DownloadString(url);
                return Page;
            }
            catch
            {
                try
                {
                    string link = "https://en.wikipedia.org" + url;
                    WebClient client = new WebClient();
                    string Page = client.DownloadString(link);
                    return Page;
                }
                catch
                {
                    return string.Empty;
                }
            }
           
        }
    }
}