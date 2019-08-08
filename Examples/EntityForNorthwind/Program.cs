using EntityForNorthwind.Model;
using System;
using System.Linq;
using System.Data.Entity;
using EntityForNorthwind.Services;

namespace EntityForNorthwind
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup log4net:
            log4net.Config.XmlConfigurator.Configure();
            var logger = new LoggerService(log4net.LogManager.GetLogger("SampleLogger"));

            using (var db = new MyDbContext())
            {
                // use log4net:
                logger.Info("Start Get Elements");
                logger.RunWithExceptionLogging(
                    () => GetElements(db)
                );

                // examples of elements retrieval:
                GetElements(db);
                
                // examples of lazy/eager/explicit loading
                LoadRelated(db);

                // examples pf db contents update:
                UnitOfWork(db);

                Console.ReadKey();
            }
        }

        static void GetElements(MyDbContext db)
        {
            var product = db.Products.First();

            // find by key
            var product1 = db.Products.Find(1);

            Console.WriteLine(product1.ProductName);

            // linq request
            var products =
                from p in db.Products
                where p.Category.CategoryName == "Beverages"
                select p;

            foreach (var p in products) {
                Console.WriteLine(p.ProductName);
            }

            var prod = db.Set<Product>().Where(p => p.ProductName.StartsWith("c"));
            foreach (var p in prod)
            {
                Console.WriteLine(p.ProductName);
            }

            db.Configuration.LazyLoadingEnabled = false;
            foreach (var p in db.Products.Include(t => t.Category))
            {
                Console.WriteLine(p.ProductName + " | " + p.Category.CategoryName);
            }
        }

        static void LoadRelated(MyDbContext db)
        {
            // Lazy
            /*foreach (var p in db.Products)
            {
                Console.WriteLine(p.ProductName + " | " + p.Category.CategoryName);
            }*/

            // Eager
            /*db.Configuration.LazyLoadingEnabled = false;
            foreach (var p in db.Products.Include(t => t.Category))
            {
                Console.WriteLine(p.ProductName + " | " + p.Category.CategoryName);
            }*/

            // Explicit
            db.Configuration.LazyLoadingEnabled = false;
            foreach (var p in db.Products)
            {
                db.Entry(p).Reference(t => t.Category).Load();
            }

            // Explicit for collections:
            db.Configuration.LazyLoadingEnabled = false;
            foreach (var с in db.Categories) {
                db.Entry(с).Collection(c => c.Products).Load(); // load all
                db.Entry(с).Collection(c => c.Products).Query() // select what to load
                    .Where(p => p.Discontinued == false);
            }
        }

        static void UnitOfWork(MyDbContext db)
        {
            var category = db.Categories.Create();
            category.CategoryName = "New category";
            category.Description = "New category description";
            db.Categories.Add(category);

            var product1 = db.Products.Create();
            product1.ProductName = "New product1";
            product1.Category = category;

            var product2 = db.Products.Create();
            product2.ProductName = "New product2";
            product2.Category = category;
            
            db.Products.Add(product1);
            db.Products.Add(product2);
            db.SaveChanges();
        }
    }
}

