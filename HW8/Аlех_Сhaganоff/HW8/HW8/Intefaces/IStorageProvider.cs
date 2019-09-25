using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Intefaces
{
    public interface IStorageProvider
    {
        void Clear();
        int Count();
        void SaveChanges();
        void Dispose();
        bool Contains(string link);
        void TryAdd(string link, int recursionLevel);
        IReadOnlyDictionary<string, int> GetRecords();
    }
}
