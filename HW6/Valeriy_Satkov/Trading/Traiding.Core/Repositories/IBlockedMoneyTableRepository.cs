using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IBlockedMoneyTableRepository
    {
        bool Contains(BlockedMoneyEntity entity); // Compare by 
        void Add(BlockedMoneyEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        BlockedMoneyEntity Get(int entityId);
        void Remove(int entityId);
    }
}
