//-----------------------------------------------------------------------
// <copyright file="LinqSamples.cs" company="AVLozhechkin">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------
namespace SampleQueries
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using SampleSupport;
    using Task.Data;

    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq11()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq2()
        {
            var products =
                from p in this.dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("Linq queries")]
        [Title("Task 1")]
        [Description(@"Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) 
превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X 
(подумайте, можно ли обойтись без копирования запроса несколько раз)")]

        public void LinqTask1()
        {
            int x = 20000;

            // int x = 15000;
            // int x = 10000;
            var customers =
                from c in this.dataSource.Customers
                where c.Orders.Select(price => price.Total).Sum() > x
                select new { c.CompanyName, price = c.Orders.Select(price => price.Total).Sum() };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Linq queries")]
        [Title("Task 2 - Without Grouping")]
        [Description(@"Для каждого клиента составьте список поставщиков, 
находящихся в той же стране и том же городе. 
Сделайте задания с использованием группировки и без.")]

        public void LinqTask2WithoutGrouping()
        {
            var query = from c in this.dataSource.Customers
                        join s in this.dataSource.Suppliers on new { c.Country, c.City } equals new { s.Country, s.City }
                        select new { c.CompanyName, c.Country, c.City, s.SupplierName };

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("Linq queries")]
        [Title("Task 2 - With Grouping")]
        [Description(@"Для каждого клиента составьте список поставщиков, 
находящихся в той же стране и том же городе. 
Сделайте задания с использованием группировки и без.")]

        public void LinqTask2WithGrouping()
        {
            var query = from c in this.dataSource.Customers
                        join s in this.dataSource.Suppliers on new { c.Country, c.City } equals new { s.Country, s.City }
                        group c by new { c.Country, c.City, c.CompanyName, s.SupplierName } into g
                        select g.Key;

            foreach (var group in query)
            {
                ObjectDumper.Write(group);
            }
        }

        [Category("Linq queries")]
        [Title("Task 3")]
        [Description(@"Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]

        public void LinqTask3()
        {
            int x = 1000;
            var customers = this.dataSource.Customers.Where(c => c.Orders.Any(t => t.Total > x)).Select(c => c.CompanyName).Distinct();

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Linq queries")]
        [Title("Task 4")]
        [Description(@"Выдайте список клиентов с указанием, начиная с какого месяца какого 
года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]

        public void LinqTask4()
        {
            var customers = this.dataSource.Customers
                .Select(c => new
                {
                    c.CompanyName,
                    FirstOrder = c.Orders
                        .Select(o => o.OrderDate)
                        .FirstOrDefault()
                        .ToString("MM, yyyy")
                })
                .Where(y => y.FirstOrder != new DateTime(day: 01, month: 01, year: 01).ToString("MM, yyyy"));

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Linq queries")]
        [Title("Task 5")]
        [Description(@"Сделайте предыдущее задание, но выдайте список отсортированным по году, 
месяцу, оборотам клиента (от максимального к минимальному) и имени клиента")]

        public void LinqTask5()
        {
            var customers = this.dataSource.Customers
                .Select(c => new
                {
                    c.CompanyName,
                    FirstOrder = c.Orders
                        .Select(o => o.OrderDate)
                        .FirstOrDefault()
                        .ToString("MM, yyyy"),
                    Amount = c.Orders.Sum(t => t.Total)
                })
                .Where(y => y.FirstOrder != new DateTime(day: 01, month: 01, year: 01).ToString("MM, yyyy"))
                .OrderBy(c => c.FirstOrder.Substring(2)).ThenBy(c => c.FirstOrder).ThenByDescending(c => c.Amount).ThenBy(c => c.CompanyName);

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Linq queries")]
        [Title("Task 6")]
        [Description(@"Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен 
регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).")]

        public void LinqTask6()
        {
            Regex regex = new Regex(@"^[\d -]+$");
            var customers = this.dataSource.Customers
                .Where(c => c.PostalCode != null && !regex.IsMatch(c.PostalCode) | (c.Region == null) | (!c.Phone.StartsWith("(")))
                .Select(c => new { c.CompanyName, c.PostalCode, c.Region, c.Phone });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Linq queries")]
        [Title("Task 7")]
        [Description(@"Сгруппируйте все продукты по категориям, внутри – по наличию на складе, 
внутри последней группы отсортируйте по стоимости")]
        public void LinqTask7()
        {
            var products = this.dataSource.Products
                .GroupBy(p => p.Category)
                .Select(c => new
                {
                    Category = c.Key,
                    Stocks = c.GroupBy(s => s.UnitsInStock == 0 ? "Sold" : "In stock")
                .Select(e => new
                {
                    Left = e.Key,
                    Prices = e
                .Select(pr => new { Name = pr.ProductName, Price = pr.UnitPrice }).OrderBy(pr => pr.Price)
                })
                });

            ObjectDumper.Write(products, 2);
        }

        [Category("Linq queries")]
        [Title("Task 8")]
        [Description(@"Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами")]
        public void LinqTask8()
        {
            decimal[] ranges = { 0M, 10M, 20M };
            var products = this.dataSource.Products
                .GroupBy(p => new { Price = (p.UnitPrice <= ranges[1]) ? "Cheap" : (p.UnitPrice < ranges[2]) ? "Medium price" : "Expensive" })
                .Select(p => new { PriceGroup = p.Key.Price, Product = p.Select(g => new { Name = g.ProductName, Price = g.UnitPrice }) });

            ObjectDumper.Write(products, 1);
        }

        [Category("Linq queries")]
        [Title("Task 9")]
        [Description(@"Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) 
и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)")]
        public void LinqTask9()
        {
            var cities = this.dataSource.Customers.GroupBy(c => c.City)
                .Select(c => new
                {
                    City = c.Key,
                    OrdersSum = c
                .Select(o => o.Orders.Sum(t => t.Total)),
                    OrderNumber = c
                .Select(o => o.Orders.Count())
                })
                .Select(c => new { c.City, Sum = c.OrdersSum.Average(), AverageNumber = c.OrderNumber.Average() });

            ObjectDumper.Write(cities, 1);
        }

        [Category("Linq queries")]
        [Title("Task 10")]
        [Description(@"Сделайте среднегодовую статистику активности клиентов по месяцам 
(без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void LinqTask10()
        {
            var staticticByMonths = this.dataSource.Customers
                .SelectMany(o => o.Orders.GroupBy(g => g.OrderDate)
                .Select(s => s.Key)).GroupBy(g => g.Month)
                .Select(s => new { s.Key, Count = s.Count() }).OrderBy(o => o.Key);

            var statisticByYears = this.dataSource.Customers
                .SelectMany(o => o.Orders.GroupBy(g => g.OrderDate)
                .Select(s => s.Key)).GroupBy(g => g.Year)
                .Select(s => new { s.Key, Count = s.Count() }).OrderBy(o => o.Key);

            var statisticByMonthsAndYears = this.dataSource.Customers
                .SelectMany(o => o.Orders.GroupBy(g => g.OrderDate)
                .Select(s => s.Key)).GroupBy(g => new { g.Month, g.Year })
                .Select(s => new { s.Key.Month, s.Key.Year, Count = s.Count() }).OrderBy(o => o.Year).ThenBy(o => o.Month);

            ObjectDumper.Write("By months:");
            ObjectDumper.Write(staticticByMonths, 1);

            ObjectDumper.Write("By years:");
            ObjectDumper.Write(statisticByYears, 1);

            ObjectDumper.Write("By months and years:");
            ObjectDumper.Write(statisticByMonthsAndYears, 1);
        }
    }
}
