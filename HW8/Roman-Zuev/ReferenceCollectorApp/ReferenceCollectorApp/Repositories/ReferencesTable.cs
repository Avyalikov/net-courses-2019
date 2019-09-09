using ReferenceCollectorApp.Context;
using ReferenceCollectorApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;


namespace ReferenceCollectorApp.Repositories
{
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

        public void AddBatch(List<ReferenceEntity> data)
        {
            using (db)
            {
                foreach (var item in data)
                {
                        db.References.Add(item);               
                }
                db.SaveChanges();
            }
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