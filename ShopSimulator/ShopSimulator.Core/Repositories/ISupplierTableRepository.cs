using ShopSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.Core.Repositories
{
    public interface ISupplierTableRepository
    {
        bool Contains(SupplierEntity entity);
        bool ContainsById(int entityId);

        void Add(SupplierEntity entity);
        void SaveChanges();
        SupplierEntity Get(int supplierId);

        T WithTransaction<T>(Func<T> function);
        void WithTransaction(Action action);
    }
}
