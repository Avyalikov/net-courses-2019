namespace MultithreadLinkParser.Services
{
    using MultithreadLinkParser.Models;
    using MultithreadLinkParser.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    public class LinkToDBManager : ILinkToDBManager
    {
        private readonly ILinksRepository linkRepository;

        const int sizeOfListChunk = 100;

        public LinkToDBManager(ILinksRepository linkRepository)
        {
            this.linkRepository = linkRepository;
        }


        public async Task RunLinkToDBAdderAsync(List<LinkInfo> linkInfos, CancellationToken cts)
        {

            await AddLinksAsync(linkInfos, cts);
        }

        public async Task<bool> AddLinksAsync(List<LinkInfo> linkInfos, CancellationToken cts)
        {
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            int totalEntriesInList = 0;
            lock (linkInfos)
            {
                totalEntriesInList = linkInfos.Count;
            }

            if (totalEntriesInList > sizeOfListChunk)
            {
                List<LinkInfo> chunkOfLinksToCheck = null;

                lock (linkInfos)
                {
                    chunkOfLinksToCheck = linkInfos.GetRange(0, sizeOfListChunk);
                    linkInfos.RemoveRange(0, sizeOfListChunk);
                }

                List<LinkInfo> linksToAdd = new List<LinkInfo>();
                Parallel.ForEach(chunkOfLinksToCheck, l =>
                {
                    if (!linkRepository.IsExistAsync(l.urlString).Result)
                    {
                        lock (linksToAdd)
                        {
                            linksToAdd.Add(l);
                        }
                    }
                });

                linkRepository.LinksInsertAsync(linksToAdd);
                Console.WriteLine($"{linksToAdd.Count} added to DB");
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