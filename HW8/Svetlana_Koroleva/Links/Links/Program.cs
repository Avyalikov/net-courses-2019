using LinkContext;
using LinkContext.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlLinksCore.Repository;
using UrlLinksCore.Service;
using LinkContext;
using System.Threading;

namespace Links
{
    class Program
    {
        static void Main(string[] args)
        {
            //  private readonly string url="";
            //   private readonly string filename="";
            LinksContext context = new LinksContext();
            ILinkRepository linkRepository = new LinkRepository(context);
            ILinkService linkService = new LinkService(linkRepository);
            Downloader downloader = new Downloader(
                "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)",
                @"C:\***",
                context,
                linkService
                );

            Thread thread1 = new Thread(new ThreadStart(downloader.Run));
            thread1.Start();
        }
    }
}
