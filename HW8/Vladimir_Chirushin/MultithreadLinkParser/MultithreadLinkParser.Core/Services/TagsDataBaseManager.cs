namespace MultithreadLinkParser.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using MultithreadLinkParser.Core.Models;
    using MultithreadLinkParser.Core.Repositories;

    public class TagsDataBaseManager : ITagsDataBaseManager
    {
        private readonly ITagsRepository tagsRepository;

        public TagsDataBaseManager(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public async Task<bool> AddLinksAsync(List<string> linkInfos, int linkLayer, CancellationToken cts)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            if (linkInfos != null)
            {
                List<LinkInfo> linksToAdd = new List<LinkInfo>();
                foreach (var link in linkInfos)
                {
                    if (!this.tagsRepository.IsExistAsync(link).Result)
                    {
                        lock (linksToAdd)
                        {
                           linksToAdd.Add(new LinkInfo { UrlString = (string) link, LinkLayer = linkLayer } );
                        }
                    }
                }

                int totalAddedLinks = 0;

                totalAddedLinks = linksToAdd.Count;
                this.tagsRepository.LinksInsertAsync(linksToAdd);

                Console.WriteLine($"{totalAddedLinks} added to DB");
                return true;
            }
            else
            {
                Console.WriteLine("There is no data to place in the DataBase");
                return false;
            }
        }
    }
}