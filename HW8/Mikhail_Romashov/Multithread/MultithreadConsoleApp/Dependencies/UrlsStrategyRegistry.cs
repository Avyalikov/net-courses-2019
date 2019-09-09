using MultithreadConsoleApp.UrlsStrategy;
using StructureMap;
using System.Collections.Generic;

namespace MultithreadConsoleApp.Dependencies
{
    class UrlsStrategyRegistry : Registry
    {
        public UrlsStrategyRegistry()
        {
            this.For<IChooseUrl>().Use<MummyFilmUrl>();
            this.For<IChooseUrl>().Use<ShawshankFilmUrl>();
            this.For<IChooseUrl>().Use<AvatarFilmUrl>();
            this.For<IChooseUrl>().Use<InterstateFilmUrl>();
            this.For<IChooseUrl>().Use<TerminalFilmUrl>();
            this.For<IEnumerable<IChooseUrl>>().Use(x => x.GetAllInstances<IChooseUrl>());
        }
    }
}
