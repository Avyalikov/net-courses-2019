using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSql
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new SampleDataContext();

            var result = db.Products;

            var expensiveInStockProducts = 
                from prod in db.Products
                where prod.UnitsInStock > 0 && prod.UnitPrice > 30
                select prod;

            var orders = from cust in db.Customers
                         where cust.Region == "WA"
                         from order in cust.Orders
                            where order.OrderDate >= new DateTime(1990, 1, 1)
                         select new { cust.CustomerID, order.OrderID };

            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var firstNumbersLessThan6 = numbers.TakeWhile(n => n < 6);


            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9, 8 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var uniqueNumbers = numbersA.Union(numbersB);


            Product product12 = 
                (from prod in db.Products
                 where prod.ProductID == 12
                 select prod).First();

            Console.WriteLine(product12.ProductName);

            var scoreRecords = new[] { new { Name = "Alice", Score = 50 }, new { Name = "Bob", Score = 40 }, new { Name = "Cathy", Score = 45 } };
            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name, sr => sr.Score);


            foreach (var item in uniqueNumbers)
            {
                Console.WriteLine(item);
                //Console.WriteLine($"{item.CustomerID}, {item.OrderID}");
            }

            Console.ReadKey();
        }
    }
}
