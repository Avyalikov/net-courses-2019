namespace ReferenceCollectorApp.Services
{
    using ReferenceCollectorApp.Models;
    using ReferenceCollectorApp.Repositories;
    using System.Collections.Generic;
    public class ReferencesDbService : IReferencesDbService
    {
        private readonly IReferenceTable referenceTable;

        public ReferencesDbService(IReferenceTable referenceTable)
        {
            this.referenceTable = referenceTable;
        }

        private void FilterReferences (ref Dictionary<string, int> unfilteredRefs)
        {
            if (unfilteredRefs.Count<1)
            {
                return;
            }

            var filteredRefs = new Dictionary<string, int>();
            foreach (var item in unfilteredRefs)
            {
                if (!referenceTable.ContainsById(item.Key))
                {
                    filteredRefs.Add(item.Key, item.Value);
                }
            }
            unfilteredRefs = filteredRefs;
        }

        public void WriteRefsToDb(ref Dictionary<string, int> unfilteredRefs)
        {
            FilterReferences(ref unfilteredRefs);

            if (unfilteredRefs.Count < 1)
            {
                return;
            }

            foreach (var item in CreateEntities(unfilteredRefs))
            {
                referenceTable.Add(item);
            }
            referenceTable.SaveChanges();
        }

        private List<ReferenceEntity> CreateEntities(Dictionary<string, int> data)
        {
            var result = new List<ReferenceEntity>();
            foreach (var item in data)
            {
                result.Add(new ReferenceEntity
                {
                    Reference = item.Key.ToLowerInvariant(),
                    iterationId = item.Value
                });
            }
            return result;
        }
    }
}
