using MultithreadApp.Core.Services;
using System;
using System.Collections.Generic;
using StructureMap;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadApp.Dependencies;
using MultithreadApp.Core.Models;

namespace MultithreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new MultithreadAppRegistry());

            var pageService = container.GetInstance<PageService>();
            using(var dbContext = container.GetInstance<MultithreadAppDbContext>())
            {
                var Links = dbContext.Links;
                foreach(PageEntity link in Links)
                {
                    Console.WriteLine(link.Link + " " + link.IterationId);
                }
                Console.ReadLine();
            }
            

        }
    }
}
