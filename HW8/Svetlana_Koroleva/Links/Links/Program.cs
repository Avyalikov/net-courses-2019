﻿using LinkContext;
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

            LinksContext context = new LinksContext();
            ILinkRepository linkRepository = new LinkRepository(context);
            ILinkService linkService = new LinkService(linkRepository);
            Downloader downloader = new Downloader(context, linkService);

            using (context)
            {
                
                downloader.Run(3, "https://en.wikipedia.org/wiki/Pipe_(material)");

            }
        }
    }
}
