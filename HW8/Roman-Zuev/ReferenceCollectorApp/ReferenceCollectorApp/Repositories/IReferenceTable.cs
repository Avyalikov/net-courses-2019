using System.Collections;
using System.Collections.Generic;

namespace ReferenceCollectorApp.Repositories
{
    public interface IReferenceTable
    {
        void SaveChanges();
        void AddBatch(Dictionary<string,string> data);
    }
}