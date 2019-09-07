namespace Multithread.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Multithread.Core.Models;

    public interface ILinkTableRepository
    {
        void Add(LinkEntity linkEntity);
        bool ContainsByLink(string link);
        void SaveChanges();
        List<LinkEntity> EntityListByIterationId(int iterationId);
        Dictionary<string, int> LookingForDuplicateLinkStrings();
    }
}
