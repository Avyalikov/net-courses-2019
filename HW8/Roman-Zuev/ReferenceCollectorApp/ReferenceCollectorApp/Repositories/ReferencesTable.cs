namespace ReferenceCollectorApp.Repositories
{
    using ReferenceCollectorApp.Context;
    using ReferenceCollectorApp.Models;
    using System.Linq;
    public class ReferencesTable : IReferenceTable
    {
        private readonly ReferenceCollectorDbContext db;

        public ReferencesTable(ReferenceCollectorDbContext db)
        {
            this.db = db;
        }

        public void Add(ReferenceEntity referenceItem)
        {
            db.References.Add(referenceItem);
        }

        public bool ContainsById (string id)
        {
            return db.References.Any(r => r.Reference == id);
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}