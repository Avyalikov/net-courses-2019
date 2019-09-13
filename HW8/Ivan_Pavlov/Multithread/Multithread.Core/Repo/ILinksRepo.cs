namespace Multithread.Core.Repo
{
    using Multithread.Core.Models;
    using System.Collections.Generic;

    public interface ILinksRepo
    {
        void CheckAddSave(Link link);
        void AddRange(ICollection<Link> links);
        bool Contains(string url);
        ICollection<string> GetAllWithIteration(int iteration);
        void SaveChanges();
        void RemoveDuplicate();
    }
}
