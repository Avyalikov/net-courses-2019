using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql
{
    public class IDataModel
    {
        public IQueryable<Product> Products { get; }
    }
}
