namespace MultithreadLinksApp.Core.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IURLCollectingService
    {
        Task<Dictionary<string, int>> CollectURLs(string url, int recursionDepth);
    }
}
