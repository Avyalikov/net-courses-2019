using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IShareTypeTableRepository
    {
        bool Contains(ShareTypeEntity entity);
        void Add(ShareTypeEntity entity);
        void SaveChanges();
        ShareTypeEntity Get(int entityId);
        void SetName(int entityId, string newName);
        void SetCost(int entityId, decimal newCost);
        bool ContainsById(int shareTypeId);
    }
}
