using HW8.Intefaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class LocalStorageProvider : IStorageProvider
    {
        private ConcurrentDictionary<string, int> dictionary = new ConcurrentDictionary<string, int>();

        public bool Contains(string link)
        {
            return dictionary.ContainsKey(link);
        }

        public void Clear()
        {
            dictionary.Clear();
        }

        public void Dispose()
        {
            
        }

        public IReadOnlyDictionary<string, int> GetRecords()
        {
            return dictionary;
        }

        public void SaveChanges()
        {
            
        }

        public void TryAdd(string link, int recursionLevel)
        {
            dictionary.TryAdd(link, recursionLevel + 1);
        }

        public int Count()
        {
            return dictionary.Count;
        }
    }
}
