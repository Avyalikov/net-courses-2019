using MultithreadApp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.Repository
{
    public interface IUrlRepository
    {
        void Add(Url url);
        IEnumerable<Url> GetAll();
        IEnumerable<Url> GetByCondition(Func<Url, bool> predicate);
    }
}
