using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Services
{
    public class DownloadService
    {
        public void DownloadHtml(string url, string filename)
        {
            using (WebClient client = new WebClient())
            {
                try
                {
                    client.DownloadFile(url, filename);
                }

                catch (WebException ex)
                {
                    if (ex.InnerException is System.IO.IOException)
                    {
                        return;//Threads conflicting
                    }
                    if (((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.NotFound)
                    {
                        return;
                    }
                }
            }
        }
    }
}
