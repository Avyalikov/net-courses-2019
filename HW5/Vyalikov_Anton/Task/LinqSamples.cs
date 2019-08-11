// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Data.Linq.SqlClient;
using System.Linq;
using SampleSupport;
using Task.Data;
using Task;

// Version Mad01

namespace SampleQueries
{
	[Title("LINQ Module")]
	[Prefix("Linq")]
	public class LinqSamples : SampleHarness
	{

		private DataSource dataSource = new DataSource();

		[Category("Restriction Operators")]
		[Title("Where - Task 1")]
		[Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
		public void Linq1()
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
				from p in dataSource.Products
				where p.UnitsInStock > 0
				select p;

			foreach (var p in products)
			{
				ObjectDumper.Write(p);
			}
		}

        [Category("Homework")]
        [Title("Linq001")]
        [Description("This querie return all cutomers with revenue more than some x_sum.")]

        public void Linq001()
        {
            int[] x_sum = { 5000, 10000, 20000 };
            foreach (int x in x_sum)
            {
                var customers = dataSource.Customers
                    .Where(a => a.Orders.Sum(b => b.Total) > x)
                    .Select(a => new {a.CustomerID, a.CompanyName, Revenue = a.Orders.Sum(b => b.Total)});
                ObjectDumper.Write($"Customers for required sum {x}: ");

                foreach (var c in customers)
                {
                    ObjectDumper.Write(c);
                }
            }
        }

        [Category("Homework")]
        [Title("Linq002")]
        [Description("This querie return all providers from the same city or country for each customer (without group).")]

        public void Linq002()
        {
            var suppliers =
                from c in dataSource.Customers
                from s in dataSource.Suppliers
                where c.Country == s.Country && c.City == s.City
                select new { s.Country, s.City, s.SupplierName, c.CompanyName };

            foreach (var s in suppliers)
            {
                ObjectDumper.Write(s);
            }
        }

        [Category("Homework")]
        [Title("Linq022")]
        [Description("This querie return all providers from the same city or country for each customer (with group by cities).")]

        public void Linq022()
        {
            var suppliers = from s in dataSource.Suppliers
                            from c in dataSource.Customers
                            where c.Country == s.Country && c.City == s.City
                            select new { s.Country, s.City, s.SupplierName, c.CompanyName };

            var groups = from s in suppliers
                         group s by s.City into cityGroups
                         select new
                         {
                             cityGroups.Key,
                             Names = from city in cityGroups
                                     select new { city.CompanyName, city.SupplierName }
                         };

            foreach (var g in groups)
            {
                ObjectDumper.Write(g, 2);
            }
        }

        [Category("Homework")]
        [Title("Linq003")]
        [Description("This querie return all customers with orders more expensive than x.")]

        public void Linq003()
        {
            int x = 5000;
            var customers = dataSource.Customers
                .Where(a => a.Orders.Any(b => b.Total > x))
                .Select(a => new { a.CompanyName, MaxValue = a.Orders.Max(b => b.Total) });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("Linq004")]
        [Description("This querie return all customers with dates of their first orders.")]

        public void Linq004()
        {
            var customers = dataSource.Customers
                .Where(a => a.Orders.Count() > 0)
                .Select(a => new { a.CompanyName, ClientSince = a.Orders.Min(b => b.OrderDate) });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("Linq005")]
        [Description("This querie return all customers with dates of their first orders sorted by date, revenue and customer name.")]

        public void Linq005()
        {
            var customers = dataSource.Customers
                .Where(a => a.Orders.Count() > 0)
                .Select(a => new { a.CompanyName, ClientSince = a.Orders.Min(b => b.OrderDate), Revenue = a.Orders.Sum(b => b.Total) })
                .OrderBy(a => a.CompanyName)
                .OrderByDescending(a => a.Revenue)
                .OrderBy(a => a.ClientSince.Month)
                .OrderBy(a => a.ClientSince.Year);

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("Linq006")]
        [Description("This querie return all customers with non-num postcode or without region or without operator code.")]

        public void Linq006()
        {
            var customers = dataSource.Customers
                .Where(a => a.PostalCode == null ? false : a.PostalCode.All(char.IsDigit) || a.Region is null || !a.Phone.StartsWith("("))
                .Select(a => new { a.CustomerID, a.CompanyName, a.PostalCode, a.Region, a.Phone});

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("Linq007")]
        [Description("This querie return all products grouped by categories, stock and price.")]

        public void Linq007()
        {
            var products = dataSource.Products
                .GroupBy(a => a.Category)
                .Select(gr => new {gr.Key, Subgroups = gr.GroupBy(a => a.UnitsInStock > 0)
                .Select(a => new { a.Key, Units = a.OrderBy(b => b.UnitPrice) })
                });

            foreach (var p in products)
            {

                ObjectDumper.Write(p.Key, 0);
                foreach (var gr in p.Subgroups)
                {
                    ObjectDumper.Write(gr.Key, 1);
                    foreach (var u in gr.Units)
                    {
                        ObjectDumper.Write(u, 2);
                    }
                }
            }
        }

        [Category("Homework")]
        [Title("Linq008")]
        [Description("This querie return all products grouped by price.")]

        private string chooseKey(string key)
        {
            switch (key)
            {
                case "0": return "Expensive";
                case "50": return "Cheap";
                default: return "Average";
            }
        }

        public void Linq008()
        {
            int[] pricesRange = { 0, 50, 100 };
            var products = dataSource.Products.GroupBy(a => pricesRange.FirstOrDefault(b => b > a.UnitPrice));

            foreach (var p in products)
            {
                ObjectDumper.Write(chooseKey(p.Key.ToString()), 0);

                foreach (var a in p)
                {
                    ObjectDumper.Write(a, 1);
                }
            }
        }

        [Category("Homework")]
        [Title("Linq009")]
        [Description("This querie return mid revenue and mid intensity of all cities.")]

        public void Linq009()
        {
            var customers = dataSource.Customers.GroupBy(x => x.City);
            var statistic = customers.Select(x => new {x.Key,
                Income = x.Average(y => y.Orders.Sum(z => z.Total)),
                Frequency = x.Average(y => y.Orders.Count())
            });

            foreach (var s in statistic)
            {

                ObjectDumper.Write(s);
            }
        }

        [Category("Homework")]
        [Title("Linq010")]
        [Description("This querie return statistics of customers activity.")]

        public void Linq010()
        {
            var ordersDate = dataSource.Customers.SelectMany(a => a.Orders.Select(b => b.OrderDate));
            var monthStat = ordersDate.GroupBy(a => a.Month).Select(a => new { a.Key, MonthsOrders = a.Count() }).OrderBy(a => a.Key);
            var monthMid = monthStat.Average(a => a.MonthsOrders);

            ObjectDumper.Write(monthMid, 0);
            foreach (var m in monthStat)
            {
                ObjectDumper.Write(m, 1);
            }

            var yearStat = ordersDate.GroupBy(a => a.Year).Select(a => new { a.Key, YearsOrders = a.Count() }).OrderBy(a => a.Key);

            ObjectDumper.Write("Year Statistics", 0);
            foreach (var y in yearStat)
            {
                ObjectDumper.Write(y, 1);
            }

            var fullStat = ordersDate.GroupBy(a => new { a.Year, a.Month }).Select(a => new { Key = a.Key.ToString(), YearsMonthOrders = a.Count() });

            ObjectDumper.Write("Full Statistics", 0);
            foreach (var fs in fullStat)
            {
                ObjectDumper.Write(fs, 1);
            }
        }
    }

    [Title("LINQ to SQL Module")]
    [Prefix("Linq")]
    public class LinqSqlSamples : SampleHarness
    {
        private DataClasses1DataContext dataSource = new DataClasses1DataContext();

        [Category("Homework")]
        [Title("Linq001")]
        [Description("This querie return all cutomers with revenue more than some x_sum.")]
        public void Linq001()
        {
            int[] x_sum = { 5000, 10000, 20000 };
            foreach (int x in x_sum)
            {
                var customers = dataSource.Customers
                    .Where(a => a.Orders.Sum(b => b.Order_Details.Sum(c => (c.UnitPrice * c.Quantity * (decimal)(1 - c.Discount)))) > x)
                    .Select(a => new {a.CustomerID, a.CompanyName,
                        Revenue = a.Orders.Sum(b => b.Order_Details.Sum(c => (c.UnitPrice * c.Quantity * (decimal)(1 - c.Discount))) )});
                ObjectDumper.Write($"Customers for required sum {x}:");

                foreach (var c in customers)
                {
                    ObjectDumper.Write(c);
                }
            }
        }

        [Category("Homework")]
        [Title("Linq003")]
        [Description("This querie return all customers with orders more expensive than x.")]

        public void Linq003()
        {
            int x = 5000;
            var customers = dataSource.Customers
                .Where(a => a.Orders.Any(b => b.Order_Details.Sum(c => (c.UnitPrice * c.Quantity * (decimal)(1 - c.Discount))) > x))
                .Select(a => new {a.CompanyName, MaxValue = 
                a.Orders.Max(b => b.Order_Details.Sum(c => (c.UnitPrice * c.Quantity * (decimal)(1 - c.Discount))) )});

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("Linq006")]
        [Description("This querie return all customers with non-num postcode or without region or without operator code.")]

        public void Linq006()
        {
            var customers = dataSource.Customers 
                .Where(a => SqlMethods.Like(a.PostalCode, "%[^0-9]%") || a.Region == null || !a.Phone.StartsWith("("))
                .Select(a => new { a.CustomerID, a.ContactName, a.Region, a.Phone, a.PostalCode });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
    }
}
