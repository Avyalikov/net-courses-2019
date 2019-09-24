using MultithreadApp.Core.Models;
using MultithreadApp.Dependencies;
using MultithreadApp.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadApp.Core.Services;

namespace MultithreadApp
{
    class ContextInitializer : DropCreateDatabaseAlways<MultithreadAppDbContext>
    {


        protected override void Seed(MultithreadAppDbContext context)
        {
            //string url = "https://en.wikipedia.org/wiki/The_Mummy_(1999_film)";
            //PageService pageService = new PageService();
            //string fileInfo = pageService.DownLoadPage(url);
            List<string> ListOfLinks = new List<string>{ "testLink" };//pageService.ExtractHtmlTags(fileInfo);

            PageContextInitializer PageInit = new PageContextInitializer();
            List<PageEntity> Links = PageInit.ContextInitializer(ListOfLinks);
            foreach (PageEntity item in Links.Distinct())
            {
                context.Links.Add(item);
            }
            context.SaveChanges();
            base.Seed(context);
        }
    }
}