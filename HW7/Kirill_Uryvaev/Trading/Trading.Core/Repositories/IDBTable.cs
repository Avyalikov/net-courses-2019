using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Core.Repositories
{
    public interface IDBTable
    {
        void SaveChanges();
        void WithTransaction(Action function);
    }
}
