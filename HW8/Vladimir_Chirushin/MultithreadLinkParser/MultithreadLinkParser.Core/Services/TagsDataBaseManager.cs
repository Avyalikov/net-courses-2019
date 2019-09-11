using MultithreadLinkParser.Core.Models;
using MultithreadLinkParser.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultithreadLinkParser.Core.Services
{
    public class TagsDataBaseManager : ITagsDataBaseManager
    {
        public readonly ITagsRepository tagsRepository;

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
                    if (!tagsRepository.IsExistAsync(link).Result)
                    {
                        lock (linksToAdd)
                        {
                            tagsRepository.Insert(new LinkInfo { urlString = link, linkLayer = linkLayer });
                            linksToAdd.Add(new LinkInfo { urlString = link, linkLayer = linkLayer });
                        }
                    }
                };
                int totalAddedLinks = 0;

                totalAddedLinks = linksToAdd.Count;
                //tagsRepository.LinksInsertAsync(linksToAdd);

                Console.WriteLine($"{totalAddedLinks} added to DB");
                return true;
            }
            else
            {
                Console.WriteLine("Not enough data to put in DB");
                return false;
            }
        }
    }
}