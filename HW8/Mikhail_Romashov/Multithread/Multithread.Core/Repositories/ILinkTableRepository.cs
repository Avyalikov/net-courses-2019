using Multithread.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Multithread.Core.Repositories
{
    public interface ILinkTableRepository
    {
        void Add(LinkEntity entity);
        void SaveChanges();
        bool Contains(string link);
        bool ContainsById(int entityId);
        LinkEntity GetById(int linkId);
        List<LinkEntity> GetListOfLinks();
        List<LinkEntity> GetListOfLinksByIteration(int iteration);
    }
}
