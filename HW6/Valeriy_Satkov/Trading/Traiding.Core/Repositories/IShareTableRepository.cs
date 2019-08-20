using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IShareTableRepository
    {
        bool Contains(ShareEntity entity);
        void Add(ShareEntity entity);
        void SaveChanges();        
        bool ContainsById(int entityId);
        ShareEntity Get(int entityId);
        void SetCompanyName(int entityId, string newCompanyName);
        void SetType(int entityId, ShareTypeEntity newType);
    }
}
