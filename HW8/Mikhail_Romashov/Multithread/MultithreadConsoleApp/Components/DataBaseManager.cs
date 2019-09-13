using Multithread.Core.Dto;
using Multithread.Core.Services;
using MultithreadConsoleApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Components
{
    public class DataBaseManager : IDataBaseManager
    {
        private readonly LinkService linkService;
        public DataBaseManager(LinkService linkService)
        {
            this.linkService = linkService;
        }
        public void AddLinksToDB(List<string> collection, int iteration)
        {

            foreach (var item in collection)
            {

                if (linkService.ContainsByLink(item))
                    continue;
                var id = linkService.AddNewLink(new LinkInfo()
                {
                    Link = item,
                    Iteration = iteration
                });
            }
        }
    }
}
