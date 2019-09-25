using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiURLCollector.Core.Repositories
{
    public interface IBaseRepository
    {
        void SaveChanges();
        void WithTransaction(Action function);
    }
}
