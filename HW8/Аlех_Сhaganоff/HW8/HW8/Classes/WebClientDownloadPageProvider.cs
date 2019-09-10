using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class WebClientDownloadPageProvider : IPageProvider
    {
        private IClientProvider webClient;

        public WebClientDownloadPageProvider(IClientProvider webClient)
        {
            this.webClient = webClient;
        }

        public string GetPage(string url)
        {
            string data = string.Empty;
            //WebClient webClient = null;

            try
            {
                //webClient = new WebClient();
                data = webClient.DownloadString(url);
                webClient.DownloadFile(url, url + ".htm");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //if (webClient != null)
            //{
            //    webClient.Dispose();
            //}

            return data;
        }
    }
}
