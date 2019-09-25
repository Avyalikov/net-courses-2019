using MultithreadApp.Core.Models;
using System.Collections.Generic;

namespace MultithreadApp.Core.Repositories
{
    public interface IPageTableRepository
    {
        bool Contains(PageEntity entityToAdd);
        void Add(PageEntity entityToAdd);
        IEnumerable <PageEntity> GetPagesFromPreviousIteration(int IterationNumber);
        void SaveChanges();
    }
}