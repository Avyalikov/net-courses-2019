// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
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
        private northwindDataContext db = new northwindDataContext();

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
		[Description("This sample returns return all presented in market products")]

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

        [Category("LinqToObject")]
        [Title("Task А.1.1")]
        [Description("This query returns all clients, who have total turnover over 90000")]

        public void LinqA1()
        {
            int threshold = 90000;
            var clients = dataSource.Customers.Where(c => (c.Orders.Sum(o => o.Total) > threshold));

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID}: {c.Orders.Sum(o => o.Total)}");
            }
        }


        [Category("LinqToObject")]
        [Title("Task А.1.2")]
        [Description("This query returns all clients, who have total turnover over 50000")]

        public void LinqA11()
        {
            int threshold = 50000;
            var clients = dataSource.Customers.Where(c => (c.Orders.Sum(o => o.Total) > threshold));

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID}: {c.Orders.Sum(o => o.Total)}");
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.2.1")]
        [Description("This query returns all clients and suppliers, who have one city. Without grouping")]

        public void LinqA2()
        {
            var clients = dataSource.Customers.Join(
                dataSource.Suppliers,
                c => c.City,
                s => s.City,
                (c, s) => 
                    new { c.CustomerID, s.SupplierName, CustomerCity = c.City, SupplierCity = s.City });

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID} : '{c.SupplierName}' in {c.CustomerCity} : {c.SupplierCity}");
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.2.2")]
        [Description("This query returns all clients and suppliers, who have one city. With grouping")]

        public void LinqA21()
        {
            var clients = dataSource.Customers.Join(
                dataSource.Suppliers,
                c => c.City,
                s => s.City,
                (c, s) =>
                    new { c.CustomerID, s.SupplierName, CustomerCity = c.City, SupplierCity = s.City })
                .GroupBy(c => c.CustomerCity);

            foreach (var c in clients)
            {
                ObjectDumper.Write(c.Key);
                ObjectDumper.Write(c);

            }
        }

        [Category("LinqToObject")]
        [Title("Task А.3")]
        [Description("This query returns all clients, who have orders worth > 5000")]

        public void LinqA3()
        {
            int threshold = 5000;

            var clients = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > threshold))
                .Select(c => new { c.CustomerID, OrderPrice = c.Orders.Max(t => t.Total) });

            foreach (var cl in clients)
            {
                ObjectDumper.Write($"{cl.CustomerID}, {cl.OrderPrice}");
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.4")]
        [Description("This query returns all clients and date of their first order.")]

        public void LinqA4()
        {
            var clients = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new {
                    c.CustomerID,
                    FirstOrder = c.Orders.Min(o => o.OrderDate)
                });

            foreach (var cl in clients)
            {
                ObjectDumper.Write($"{cl.CustomerID}, {cl.FirstOrder.ToShortDateString()}");
            }
        }


        [Category("LinqToObject")]
        [Title("Task А.5")]
        [Description("This query returns all clients and date of their first order, " +
                    "ordered by year, month, turnover, name")]

        public void LinqA5()
        {
            var clients = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new {
                    c.CustomerID,
                    FirstOrder = c.Orders.Min(o => o.OrderDate),
                    Turnover = c.Orders.Sum(o => o.Total)
                })
                .OrderBy(c => c.FirstOrder.Year)
                .ThenBy(c => c.FirstOrder.Month)
                .ThenByDescending(c => c.Turnover)
                .ThenBy(c => c.CustomerID);

            foreach (var cl in clients)
            {
                ObjectDumper.Write($"{cl.CustomerID}, {cl.FirstOrder.ToShortDateString()}, {cl.Turnover}");
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.6")]
        [Description("This query returns all clients, who have no digital code" +
                    " or empty region, or no operator code.")]

        public void LinqA6()
        {
            var clients = dataSource.Customers
                .Where(c => object.ReferenceEquals(null, c.PostalCode) ||
                            c.PostalCode.Any(ch => Char.IsLetter(ch)) ||
                            object.ReferenceEquals(null, c.Region) ||
                            !c.Phone.StartsWith("(") )
                .Select(c => new
                {
                    c.CustomerID,
                    PostalCode = object.ReferenceEquals(null, c.PostalCode) ? "null" : c.PostalCode,
                    Region = object.ReferenceEquals(null, c.Region) ? "null" : c.Region,
                    c.Phone
                });

            foreach (var cl in clients)
            {
                ObjectDumper.Write($"{cl.CustomerID}, {cl.PostalCode}, {cl.Region}, {cl.Phone}");
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.7")]
        [Description("This query returns all products, grouped by categories, " +
                    "inside by avalability in stock, inside by price.")]

        public void LinqA7()
        {
            var products = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(pr => new
                {
                   Category = pr.Key,
                   inStockGroup = pr.GroupBy(pro => pro.UnitsInStock > 0)
                   .Select(prod => new
                   {
                       inStock = prod.Key,
                       Price = prod.OrderBy(produc => produc.UnitPrice)
                   })
                });

            foreach (var p in products)
            {
                ObjectDumper.Write(p.Category);
                foreach (var s in p.inStockGroup)
                {
                    ObjectDumper.Write(s.inStock ? "In Stock" : "Out of Stock");
                    foreach (var r in s.Price)
                        ObjectDumper.Write("Price =  " + r.UnitPrice + " Product: " + r.ProductName);
                }
                ObjectDumper.Write(String.Empty);
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.8")]
        [Description("This query returns all products, grouped by categories, " +
                    "inside by avalability in stock, inside by price.")]

        public void LinqA8()
        {
            int Cheap = 20;
            int Expensive = 40;

            var products = dataSource.Products
                .OrderBy(p => p.UnitPrice)
                .GroupBy(p => p.UnitPrice < Cheap ? "Cheap" : p.UnitPrice < Expensive ? "Average" : "Expensive")
                .Select(x => new {
                    Price_Category = x.Key,
                    Product = x.Select( y => new { y.ProductName, y.UnitPrice })
                });

            foreach (var category in products)
            {
                ObjectDumper.Write(category, 2);
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.9")]
        [Description("This query returns average profitability and average intensity of each city.")]

        public void LinqA9()
        {
            var average = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(city => new {
                    City = city.Key,
                    averageProfit = city.Average(a => (a.Orders.Sum(o => o.Total))),
                    averageIntensity = city.Average(a => a.Orders.Count())
                });

            foreach (var avg in average)
            {
                ObjectDumper.Write($"In {avg.City} is {avg.averageProfit:F2}$ " +
                    $"profit and {avg.averageIntensity:F1} clients.");
                ObjectDumper.Write(String.Empty);
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.10.1")]
        [Description("This query returns amount of orders in each mounth.")]

        public void LinqA101()
        {
            var perMonth = dataSource.Customers
                         .SelectMany(o => o.Orders.Select(y => y.OrderDate))
                         .GroupBy(m => m.Month)
                         .Select(s => new
                         {
                             s.Key,
                             ordersCount = s.Count()
                         })
                         .OrderBy(m => m.Key);

            ObjectDumper.Write("Month : Count of orders");
            foreach (var avg in perMonth)
            {
                ObjectDumper.Write(avg.Key + " : " + avg.ordersCount);
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.10.2")]
        [Description("This query returns amount of orders in each year.")]

        public void LinqA102()
        {
            var perMonth = dataSource.Customers
                         .SelectMany(o => o.Orders.Select(y => y.OrderDate))
                         .GroupBy(m => m.Year)
                         .Select(s => new
                         {
                             s.Key,
                             ordersCount = s.Count()
                         })
                         .OrderBy(m => m.Key);

            ObjectDumper.Write("Year : Count of orders");
            foreach (var avg in perMonth)
            {
                ObjectDumper.Write(avg.Key + " : " + avg.ordersCount);
            }
        }

        [Category("LinqToObject")]
        [Title("Task А.10.3")]
        [Description("This query returns amount of orders in each year and month.")]

        public void LinqA103()
        {
            var perMonth = dataSource.Customers
                         .SelectMany(o => o.Orders.Select(y => y.OrderDate))
                         .GroupBy(m => new {m.Year, m.Month})
                         .Select(s => new
                         {
                             month = s.Key.Month,
                             year = s.Key.Year,
                             ordersCount = s.Count()
                         })
                         .OrderBy(y => y.year).ThenBy(m => m.month);

            ObjectDumper.Write("Year : Month : average number of Orders");
            foreach (var avg in perMonth)
            {
                ObjectDumper.Write(avg.year + " : " + (avg.month) + " : " + avg.ordersCount);
            }
        }

        [Category("LinqToSQL")]
        [Title("Task B.1.1")]
        [Description("This query returns all clients, who have total turnover over 9")]
        public void LinqB11()
        {
            int threshold = 9;

            var clients =
                db.Customers
                   .Where(c => c.Orders.Sum(o => o.Order_Details.Sum(od =>
                        (od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)))) > threshold)
                   .Select(c => new
                   {
                       c.CustomerID,
                       Total = c.Orders.Sum(o => o.Order_Details.Sum(od =>
                        (od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))))
                   }
                    );

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID}: {c.Total}");
            }
        }

        [Category("LinqToSQL")]
        [Title("Task B.1.2")]
        [Description("This query returns all clients, who have total turnover over 18")]
        public void LinqB12()
        {
            int threshold = 18;

            var clients =
               db.Customers
                   .Where(c => c.Orders.Sum(o => o.Order_Details.Sum(od =>
                        (od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)))) > threshold)
                   .Select(c => new
                   {
                       c.CustomerID,
                       Total = c.Orders.Sum(o => o.Order_Details.Sum(od =>
                        (od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))))
                   }
                   );

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID}: {c.Total}");
            }
        }

        [Category("LinqToSQL")]
        [Title("Task B.3")]
        [Description("This query returns all clients, who have orders worth > 5000")]

        public void LinqB3()
        {
            decimal threshold = 5000;

            var clients =
             db.Customers
                 .Where(c => c.Orders.Any(o => o.Order_Details.Sum(od => 
                        (od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) > threshold))
                 .Select(cl => new
                 {
                     cl.CustomerID,
                     OrderPrice = cl.Orders.Max(or => or.Order_Details.Sum(ord => 
                                                (ord.UnitPrice * ord.Quantity * (decimal)(1 - ord.Discount))))
                 });

            foreach (var c in clients)
            {
                ObjectDumper.Write($"{c.CustomerID}, {c.OrderPrice:F0}");
            }
        }

        [Category("LinqToSQL")]
        [Title("Task B.6")]
        [Description("This query returns all clients, who have no digital code" +
                    " or empty region, or no operator code.")]

        public void LinqB6()
        {
            var clients = db.Customers
                .Where(c => c.PostalCode == null || (c.PostalCode.Contains("%[0-9]%")) ||
                            c.Region == null || !c.Phone.StartsWith("("))
                .Select(c => new
                {
                    c.CustomerID,
                    c.PostalCode,
                    c.Region,
                    c.Phone
                }
                );

           
            foreach (var c in clients)
            {
                string region = c.Region;
                if (String.IsNullOrEmpty(c.Region))
                    region = "null";
                ObjectDumper.Write($"{c.CustomerID}, {c.PostalCode}, {region}, {c.Phone}");
            }
        }

    }
}
