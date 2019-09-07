namespace Multithread.Console.Repo
{
    using System.Collections.Generic;
    using System.Linq;
    using Multithread.Core.Models;
    using Multithread.Core.Repo;

    public class LinksRepo : ILinksRepo
    {
        public void CheckAddSave(Link link)
        {
            using (LinksDbContext db = new LinksDbContext())
            {
                var args = db.Links.Where(item => item.Url == link.Url).FirstOrDefault();
                if (args != null)
                    return;
                db.Links.Add(link);
                db.SaveChanges();
            }
        }

        public void AddRange(ICollection<Link> links)
        {
            using (LinksDbContext db = new LinksDbContext())
                db.Links.AddRange(links);
        }

        public bool Contains(string url)
        {
            Link args = null;
            using (LinksDbContext db = new LinksDbContext())
                args = db.Links.Where(link => link.Url == url).FirstOrDefault();
            if (args == null)
                return false;
            return true;
        }

        public ICollection<string> GetAllWithIteration(int iteration)
        {
            using (LinksDbContext db = new LinksDbContext())
                return db.Links.Where(link => link.IterationId == iteration).Select(link => link.Url).ToList();
        }

        public void SaveChanges()
        {
            using (LinksDbContext db = new LinksDbContext())
                db.SaveChanges();
        }

        public void RemoveDuplicate()
        {
            using (LinksDbContext db = new LinksDbContext())
            {
                var duplicates = db.Links.GroupBy(link=>link.Url)
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key)
                    .ToList();
                foreach(var dup in duplicates)
                {
                    var link = db.Links.Where(li => li.Url == dup).First();
                    db.Links.Remove(link);
                }
                db.SaveChanges();
            }
        }
    }
}
