using HW8.Intefaces;
using HW8_EF;
using HW8_EF.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW8.Classes
{
    public class EFStorageProvider : IStorageProvider
    {
        private LinksContext context;
        
        public EFStorageProvider()
        {
            context = new LinksContext();
        }

        public void Clear()
        {
            var rows = from o in context.Links
                       select o;
            foreach (var row in rows)
            {
                context.Links.Remove(row);
            }

            context.SaveChanges();
        }

        public bool Contains(string link)
        {
            var r = context.Links.Find(link);

            if(r != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Count()
        {
            return context.Links.ToList().Count;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public IReadOnlyDictionary<string, int> GetRecords()
        {
            return context.Links.Select(x => x).ToDictionary(x => x.Link, x => x.IterationId);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public void TryAdd(string link, int recursionLevel)
        {
            context.Links.Add(new Links() { Link = link, IterationId = recursionLevel });
            bool needToUndo = false;

            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                needToUndo = true;

                foreach (var error in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        error.Entry.Entity.GetType().Name, error.Entry.State);

                    foreach (var er in error.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            er.PropertyName, er.ErrorMessage);
                    }
                }
            }
            finally
            {
                if(needToUndo)
                {
                    foreach (var entry in context.ChangeTracker.Entries())
                    {
                        if (entry.State == EntityState.Added)
                        {
                            entry.State = EntityState.Detached;
                        }
                    }
                }
            }
        }
    }
}
