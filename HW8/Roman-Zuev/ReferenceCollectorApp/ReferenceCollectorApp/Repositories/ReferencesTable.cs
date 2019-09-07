using ReferenceCollectorApp.Context;
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

        public void AddBatch(Dictionary<string,string> data)
        {
            using (db)
            {
                foreach (var item in data)
                {
                    db.References.Add(new Models.ReferenceEntity
                    {
                        Reference = item.Key,
                        iterationId = item.Value
                    });
                }
                db.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}