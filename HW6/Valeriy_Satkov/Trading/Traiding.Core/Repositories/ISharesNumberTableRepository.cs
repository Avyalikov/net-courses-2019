using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface ISharesNumberTableRepository
    {
        bool Contains(SharesNumberEntity entity); // Compare ClientId && ShareId with entity props
        void Add(SharesNumberEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        SharesNumberEntity Get(int entityId);
        IEnumerable<SharesNumberEntity> GetByClient(int clientId);
        IEnumerable<SharesNumberEntity> GetByType(int shareTypeId);
        void ChangeNumber(int entityId, int newNumber);
        void Remove(int entityId);
    }
}
