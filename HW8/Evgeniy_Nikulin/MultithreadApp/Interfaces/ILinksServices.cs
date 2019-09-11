namespace MultithreadApp.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILinksServices
    {
        Task DownloadPage(string page);
        Task DownloadPages(IEnumerable<string> pages);
        List<string> SavePagesToDb(List<string> pages, int iteration);
        List<string> ParseWikiPage(string page);
        List<List<string>> ParseWikiPages(IEnumerable<string> pages);
    }
}