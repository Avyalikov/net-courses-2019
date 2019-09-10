using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class WebClinetPageProvider : IPageProvider
    {
        private IClientProvider webClient;

        public WebClinetPageProvider(IClientProvider webClient)
        {
            this.webClient = webClient;
        }

        public string GetPage(string url)
        {
            string data = string.Empty;
            WebClient webClient = null;
            
            try
            {
                webClient = new WebClient();
                data = webClient.DownloadString(url);
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return data;
        }
    }
}
