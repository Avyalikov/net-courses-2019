using Multithread.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.Core.Repositories
{
    public interface ILinksHistoryRepositoroes
    {
        void Add(LinksHistoryEntity linksHistoryEntity);
        bool Contains(LinksHistoryEntity linksHistoryEntity);
        void SaveChanges();

    }
}
