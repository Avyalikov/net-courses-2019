namespace MultithreadLinksApp.Core.Interfaces
{
    using Models;
    using System.Collections.Generic;
    public interface IURLService
    {
        void AddURL(URL url);
        void AddURLFromDict(Dictionary<string, int> urls);
        void RemoveURL(string url);
        URL GetURL(string url);
    }
}
