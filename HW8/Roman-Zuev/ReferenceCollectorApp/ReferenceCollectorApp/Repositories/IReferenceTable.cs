using ReferenceCollectorApp.Models;
using System.Collections;
using System.Collections.Generic;

namespace ReferenceCollectorApp.Repositories
{
    public interface IReferenceTable
    {
        void SaveChanges();
        void AddBatch(List<ReferenceEntity> data);
        bool ContainsById(string id);
        void Add(ReferenceEntity referenceItem);
    }
}