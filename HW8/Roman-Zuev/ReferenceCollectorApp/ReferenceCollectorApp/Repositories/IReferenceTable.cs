namespace ReferenceCollectorApp.Repositories
{
    using ReferenceCollectorApp.Models;
    public interface IReferenceTable
    {
        void SaveChanges();
        bool ContainsById(string id);
        void Add(ReferenceEntity referenceItem);
    }
}