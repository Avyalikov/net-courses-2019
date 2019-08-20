using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IBlockedSharesNumberTableRepository
    {
        bool Contains(BlockedSharesNumberEntity entity); // Compare by 
        void Add(BlockedSharesNumberEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        BlockedSharesNumberEntity Get(int entityId);
        void Remove(int entityId);
    }
}
