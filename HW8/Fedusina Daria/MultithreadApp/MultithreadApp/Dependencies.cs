using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultithreadApp.Core.Repositories;
using MultithreadApp.Core.Services;
using MultithreadApp.Repositories;
namespace MultithreadApp.Dependencies
{
    public class MultithreadAppRegistry : Registry
    {
        public MultithreadAppRegistry()
        {
            this.For<IPageTableRepository>().Use<PageTableRepository>();
            this.For<IDownloadWebPageRepository>().Use<DownloadWebPageRepository>();
            this.For<IExtractHtmlTags>().Use<ExtractHtmlTags>();
            this.For<PageService>().Use<PageService>();
            this.For<MultithreadAppDbContext>().Use<MultithreadAppDbContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["MultithreadAppConnectionString"].ConnectionString);
        }

    }

}