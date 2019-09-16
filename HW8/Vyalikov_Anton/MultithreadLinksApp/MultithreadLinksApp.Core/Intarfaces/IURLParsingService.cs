namespace MultithreadLinksApp.Core.Interfaces
{
    using Models;
    using System.Collections.Generic;
    public interface IURLParsingService
    {
        IEnumerable<URL> GetAllURLsFromPage(string page, int iteration);
    }
}
