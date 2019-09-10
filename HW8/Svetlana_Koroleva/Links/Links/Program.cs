using LinkContext;
using LinkContext.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlLinksCore.Repository;
using UrlLinksCore.Service;
using System.Threading;

namespace Links
{
    class Program
    {
        static void Main(string[] args)
        {
            Downloader downloader = new Downloader();
            Task start=downloader.Run(3, "https://en.m.wikipedia.org/wiki/Colotomic");
            start.Wait();
        }
    }
}
