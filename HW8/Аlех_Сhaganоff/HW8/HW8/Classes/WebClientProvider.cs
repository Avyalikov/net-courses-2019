using HW8.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class WebClientProvider : IClientProvider
    {
        WebClient webClient = new WebClient();

        int pageDownloadCounter = 0;

        public int PageDownloadCounter { get => pageDownloadCounter; set => pageDownloadCounter = value; }

        public void DownloadFile(string url, string filename)
        {
            webClient.DownloadFile(url, filename);
            PageDownloadCounter++;
        }

        public string DownloadString(string url)
        {
            return webClient.DownloadString(url);
        }
    }
}
