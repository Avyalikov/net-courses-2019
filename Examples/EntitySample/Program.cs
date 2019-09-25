using EntitySample.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SampleDbContext())
            {
                db.Database.Initialize(false);

                //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleDbContext, Configuration>());
                //db.Database.Initialize(false);


                //db.Configuration.LazyLoadingEnabled = false;
                var book = db.Books.FirstOrDefault();

                //Console.WriteLine(book.Author.Name);

                db.Entry(book).Reference(b => b.Author).Load();

                var test = db.Books.Include("Author").ToList();

                book = db.Books.FirstOrDefault();
                var author = book.Author;
                Console.WriteLine(book.Author.Name);
                Console.ReadKey();
            }
        }
    }
}
