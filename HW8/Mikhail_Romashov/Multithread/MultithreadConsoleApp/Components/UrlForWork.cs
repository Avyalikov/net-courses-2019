using MultithreadConsoleApp.Dependencies;
using MultithreadConsoleApp.UrlsStrategy;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MultithreadConsoleApp.Components
{
    class UrlForWork
    {
        public static string GetUrl()
        {
            var strategyContainer = new Container(new UrlsStrategyRegistry());
            var strategies = strategyContainer.GetInstance<IEnumerable<IChooseUrl>>();
            Console.WriteLine(@"
-----------------
Please, choose one of the url:
1-https://en.wikipedia.org/wiki/The_Mummy_(1999_film)
2-https://en.wikipedia.org/wiki/The_Terminal
3-https://en.wikipedia.org/wiki/Interstate_60
4-https://en.wikipedia.org/wiki/Avatar_(2009_film)
5-https://en.wikipedia.org/wiki/The_Shawshank_Redemption
Or another for exit");
            string inputString = Console.ReadLine();
            var strategy = strategies.FirstOrDefault(s => s.CanExecute(inputString));
            return strategy?.Execute();
        }
    }
}
