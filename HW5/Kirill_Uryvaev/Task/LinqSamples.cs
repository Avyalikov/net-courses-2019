// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task;
using Task.Data;

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

        [Category("Home Task")]
        [Title("Linq001")]
        [Description("This sample return all customers that have order ")]

        public void Linq001()
        {
            int[] requeredSum = { 10000, 5000, 15000 };
            foreach (int r in requeredSum)
            {
                var clients = dataSource.Customers.Where(x => x.Orders.Sum(y => y.Total) > r).
                    Select(x=>new { x.CustomerID, x.CompanyName, OrdersSum = x.Orders.Sum(y => y.Total) });
                ObjectDumper.Write($"Requested sum {r}:");
                foreach (var c in clients)
                {
                    ObjectDumper.Write(c);
                }
            }
        }

        [Category("Home Task")]
        [Title("Linq002")]
        [Description("This sample return all customers and suppliers in same city and country ")]

        public void Linq002()
        {
            var clients =
                from c in dataSource.Customers
                from s in dataSource.Suppliers
                where c.Country == s.Country && c.City==s.City
                select new { c.City, c.Country, c.CompanyName, s.SupplierName};

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq002x")]
        [Description("This sample return all customers and suppliers in same city and country with group by")]

        public void Linq002x()
        {
            var suppliers = dataSource.Suppliers.GroupBy(x => new { x.City, x.Country });
            var clients = dataSource.Customers.GroupBy(x => new { x.City, x.Country }).Select(g => new
            {
                g.Key,
                Subgroups = g.Select(x => new { x, Suppliers = suppliers.Where(y => y.Key.Equals(g.Key)) })
            }).Where(x=>x.Subgroups.Sum(y=>y.Suppliers.Count())!=0);

            foreach (var c in clients)
            {
               ObjectDumper.Write(c.Key,0);
                foreach (var cs in c.Subgroups)
                {
                    ObjectDumper.Write(cs.x,1);
                    foreach (var s in cs.Suppliers)
                    {
                        ObjectDumper.Write(s,2);
                    }
                }
            }
        }

        [Category("Home Task")]
        [Title("Linq003")]
        [Description("This sample return all customers that have order bigger than r ")]

        public void Linq003()
        {
            int r = 1000;
            var clients = dataSource.Customers.Where(x => x.Orders.Any(y => y.Total>r)).
                Select(x=>new { x.CompanyName, MaxValue = x.Orders.Max(y=>y.Total)});

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq004")]
        [Description("This sample return all customers with orders and date of their first order ")]

        public void Linq004()
        {
            var clients = dataSource.Customers.Where(x=>x.Orders.Count()>0).
                Select(x => new { x.CompanyName, ClientFrom = x.Orders.Min(y => y.OrderDate) });

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq005")]
        [Description("This sample return all customers with orders and date of their first order and ordered by")]

        public void Linq005()
        {
            var clients = dataSource.Customers.Where(x => x.Orders.Count() > 0).
                Select(x => new { x.CompanyName, ClientFrom = x.Orders.Min(y => y.OrderDate), OrdersSum = x.Orders.Sum(y=>y.Total) }).
                OrderBy(x => x.CompanyName).OrderByDescending(x => x.OrdersSum).OrderBy(x => x.ClientFrom.Month).OrderBy(x=>x.ClientFrom.Year);

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq006")]
        [Description("This sample return all customers with specific properties")]

        public void Linq006()
        {
            var clients = dataSource.Customers.Where(x => x.Region == null || !x.Phone.StartsWith("(") || !x.PostalCode.All(char.IsDigit)).
                Select(x => new { x.CustomerID, x.CompanyName, x.Region, x.Phone, x.PostalCode }); ;

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq007")]
        [Description("This sample return categorized products")]

        public void Linq007()
        {
            var products = dataSource.Products.GroupBy(x=>x.Category).
                Select(g=> new {
                    g.Key,
                    Subgroups = g.GroupBy(x=>x.UnitsInStock>0).Select(x=>new { x.Key, Units = x.OrderBy(y => y.UnitPrice) })
            });

                foreach (var p in products)
            {

                ObjectDumper.Write(p.Key,0);
                foreach (var s in p.Subgroups)
                {
                    ObjectDumper.Write(s.Key, 1);
                    foreach (var u in s.Units)
                    {
                        ObjectDumper.Write(u, 2);
                    }
                }
            }
        }

        [Category("Home Task")]
        [Title("Linq008")]
        [Description("This sample return products grouped by price")]

        public void Linq008()
        {
            int[] priceRange = { 0, 10, 100 };
            var products = dataSource.Products.GroupBy(x=>priceRange.FirstOrDefault(r=>r>x.UnitPrice));

            foreach (var p in products)
            {

                ObjectDumper.Write(changeKeyLinq008(p.Key.ToString()),0);
                foreach (var u in p)
                {
                    ObjectDumper.Write(u,1);
                }
            }
        }

        private string changeKeyLinq008(string key)
        {
            switch(key)
            {
                case "0": return "Expensive";
                case "10": return "Cheap";
                default: return "Average";
            }
        }

        [Category("Home Task")]
        [Title("Linq009")]
        [Description("This sample return avarage city values")]

        public void Linq009()
        {
            var clients = dataSource.Customers.GroupBy(x=>x.City);
            var statistic = clients.Select(x => new { x.Key,
                Income = x.Average(y => y.Orders.Sum(z => z.Total)),
                Frequency = x.Average(y => y.Orders.Count()) });
            foreach (var s in statistic)
            {

                ObjectDumper.Write(s);
            }
        }

        [Category("Home Task")]
        [Title("Linq010")]
        [Description("This sample return customers activity")]

        public void Linq010()
        {
            var ordersDate = dataSource.Customers.SelectMany(x=>x.Orders.Select(y=>y.OrderDate));
            var statisticMonth = ordersDate.GroupBy(x => x.Month).Select(x=>new {x.Key, MonthsOrders = x.Count() }).OrderBy(x=>x.Key);
            var avarageMonth = statisticMonth.Average(x => x.MonthsOrders);

            ObjectDumper.Write(avarageMonth,0);
            foreach (var m in statisticMonth)
            {
                ObjectDumper.Write(m,1);
            }

            var statisticYear = ordersDate.GroupBy(x => x.Year).Select(x => new { x.Key, YearsOrders = x.Count() }).OrderBy(x => x.Key);

            ObjectDumper.Write("Year Statistic", 0);
            foreach (var y in statisticYear)
            {
                ObjectDumper.Write(y,1);
            }

            var statisticYearMonth = ordersDate.GroupBy(x => new { x.Year, x.Month }).Select(x => new { Key = x.Key.ToString(), YearsMonthOrders = x.Count() });

            ObjectDumper.Write("Year Month Statistic", 0);
            foreach (var ym in statisticYearMonth)
            {
                ObjectDumper.Write(ym,1);
            }
        }
    }

    [Title("LINQ to SQL Module")]
    [Prefix("Linq")]
    public class LinqSqlSamples : SampleHarness
    {
        private NorthwindDataClassesDataContext dataSource = new NorthwindDataClassesDataContext();

        [Category("Home Task")]
        [Title("Linq001")]
        [Description("This sample return all customers that have order ")]
        public void Linq001()
        {
            int[] requeredSum = { 10000, 5000, 15000 };
            foreach (int r in requeredSum)
            {
                var clients = dataSource.Customers.Where(x => x.Orders.
                Sum(y => y.Order_Details.Sum(z=>(z.UnitPrice*z.Quantity* (decimal)(1-z.Discount)))) > r).
                    Select(x => new { x.CustomerID, x.CompanyName, OrdersSum = x.Orders.
                    Sum(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount))))
                    });
                ObjectDumper.Write($"Requested sum {r}:");
                foreach (var c in clients)
                {
                    ObjectDumper.Write(c);
                }
            }
        }

        [Category("Home Task")]
        [Title("Linq003")]
        [Description("This sample return all customers that have order bigger than r ")]

        public void Linq003()
        {
            int r = 1000;
            var clients = dataSource.Customers.Where(x => x.Orders.
            Any(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount))) > r)).
                Select(x => new { x.CompanyName, MaxValue = x.Orders.
                Max(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount)))) });

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Home Task")]
        [Title("Linq006")]
        [Description("This sample return all customers with specific properties")]

        public void Linq006()
        {
            var clients = dataSource.Customers.Where(x => x.Region == null || !x.Phone.StartsWith("(") || !SqlMethods.Like(x.PostalCode, "%[^0-9]%")).
                Select(x=>new { x.CustomerID, x.ContactName, x.Region, x.Phone, x.PostalCode });

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }
    }
}
