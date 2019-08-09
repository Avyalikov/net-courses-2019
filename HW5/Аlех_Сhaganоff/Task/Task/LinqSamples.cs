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
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
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

        [Category("HW")]
        [Title("HW - Task 1")]
        [Description("All customers whose orders total is greater than X")]

        public void LinqHW1()
        {
            decimal X = 10000;

            //var customers =
            //    from c in dataSource.Customers
            //    where c.Orders.Select(o => o.Total).Sum() > X
            //    select new { c.CustomerID, OrderSum = c.Orders.Select(o => o.Total).Sum()};

            //var customers =
            //    from c in dataSource.Customers
            //    where (from o in c.Orders select o.Total).Sum() > X
            //    select new { c.CustomerID, OrderSum = (from o in c.Orders select o.Total).Sum()};

            var customers = dataSource.Customers.Where(c => c.Orders.Select(o => o.Total).Sum() > X).
                Select(x=>new { x.CustomerID, OrderSum = x.Orders.Select(o => o.Total).Sum() });    

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 2 no grouping")]
        [Description("Suppliers situated in the same country and city as the cleints")]

        public void LinqHW2A()
        {
            //var result =
            //    from c in dataSource.Customers
            //    join s in dataSource.Suppliers on new { c.Country, c.City } equals new { s.Country, s.City }
            //    select new { c.CustomerID, c.Country, c.City, s.SupplierName };

            var result = dataSource.Customers.Join(dataSource.Suppliers, c => new { c.Country, c.City, }, s => new { s.Country, s.City },
                (c, s) => new { c.CustomerID, c.Country, c.City, s.SupplierName }); 

            foreach (var c in result)
            {
                ObjectDumper.Write(c,1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 2 with grouping")]
        [Description("Suppliers situated in the same country and city as the cleints")]

        public void LinqHW2B()
        {
            //var result =
            //    from c in dataSource.Customers
            //    join s in dataSource.Suppliers on new { c.Country, c.City } equals new { s.Country, s.City } into gr
            //    select new { c.CustomerID, c.Country, c.City, Suppliers = from g in gr select new { g.SupplierName } } into r
            //    where r.Suppliers.Count() != 0
            //    select r;

            //var result =
            //    from c in dataSource.Customers
            //    group c by c.CustomerID into gr
            //    join s in dataSource.Suppliers on new { gr.FirstOrDefault().Country, gr.FirstOrDefault().City }
            //    equals new { s.Country, s.City }
            //    select new { gr.Key, s.Country, s.City, s.SupplierName };

            var result = dataSource.Customers.GroupBy(c => c.CustomerID).Join(dataSource.Suppliers, gr => new { gr.FirstOrDefault().Country, gr.FirstOrDefault().City, },
                s => new { s.Country, s.City },
                (gr, s) => new { gr.Key, s.Country, s.City, s.SupplierName });

            foreach (var c in result)
            {
                ObjectDumper.Write(c,1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 3")]
        [Description("All customers who had orders greater than X")]

        public void LinqHW3()
        {
            decimal X = 10000;

            //var customers =
            //    (from c in dataSource.Customers
            //    where c.Orders.Any(o => o.Total > X)
            //    select c).Distinct();

            //var customers =
            //    (from c in dataSource.Customers
            //    where (from o in c.Orders select o.Total).Any(t => t > X)
            //    select c).Distinct();

            var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > X)).Distinct();

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 4")]
        [Description("Clients and first order dates")]

        public void LinqHW4()
        {
            //var result =
            //    from c in dataSource.Customers
            //    select new { c.CustomerID, Date = c.Orders.Select(x => x.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM") }
            //    into clients
            //    where clients.Date != new DateTime().ToString("yyyy, MM")
            //    select new { clients.CustomerID, clients.Date };

            //var result =
            //    from c in dataSource.Customers
            //    select new { c.CustomerID, Date = (from o in c.Orders select o.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM") }
            //    into clients
            //    where clients.Date != new DateTime().ToString("yyyy, MM")
            //    select new { clients.CustomerID, clients.Date };
                       
            var result =
                dataSource.Customers.Select
                (c => new
                { c.CustomerID, Date = c.Orders.Select(x => x.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM") })
                .Where(y => y.Date != new DateTime().ToString("yyyy, MM"));

            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 5")]
        [Description("Clients and first order dates sorted")]

        public void LinqHW5()
        {
            //var result =
            //    from c in dataSource.Customers
            //    select new
            //    {  c.CustomerID,
            //        Date = c.Orders.Select(x => x.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM"),
            //        Total = (from o in c.Orders select o.Total).Sum()
            //    }
            //    into clients
            //    where clients.Date != new DateTime().ToString("yyyy, MM")
            //    orderby clients.Date, clients.Total descending, clients.CustomerID
            //    select new { clients.CustomerID, clients.Date, clients.Total };

            //var result =
            //    from c in dataSource.Customers
            //    select new
            //    {
            //        c.CustomerID,
            //        Date = (from o in c.Orders select o.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM"),
            //        Total = c.Orders.Select(o => o.Total).Sum()
            //    }
            //    into clients
            //    where clients.Date != new DateTime().ToString("yyyy, MM")
            //    orderby clients.Date, clients.Total descending, clients.CustomerID
            //    select new { clients.CustomerID, clients.Date, clients.Total };

            var result =
                dataSource.Customers.Select
                (c => new
                {
                    c.CustomerID,
                    Date = c.Orders.Select(x => x.OrderDate).DefaultIfEmpty(new DateTime()).Min().ToString("yyyy, MM"),
                    Total = c.Orders.Select(o => o.Total).Sum()
                })
                .Where(y => y.Date != new DateTime().ToString("yyyy, MM")).
                OrderBy(t => t.Date).ThenByDescending(t => t.Total).ThenBy(t => t.CustomerID);

            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 6")]
        [Description("Clients with a non-digit postal code or with no region info or with no phone operator code")]

        public void LinqHW6()
        {
            //var result =
            //    from c in dataSource.Customers
            //    where c.PostalCode != null && c.Phone != null
            //    where Regex.IsMatch(c.PostalCode, @"[\D]") || !Regex.IsMatch(c.Phone, @"^[(]") || c.Region == null
            //    select new { c.CustomerID, c.PostalCode, c.Phone, c.Region };

            var result = dataSource.Customers.Select(c => new { c.CustomerID, c.PostalCode, c.Phone, c.Region }).
                Where(c => c.PostalCode != null && c.Phone != null &&
                (Regex.IsMatch(c.PostalCode, @"[\D]") || !Regex.IsMatch(c.Phone, @"^[(]") || c.Region == null));
              
            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 7")]
        [Description("Group products by categories, number in stock, sort by price")]

        public void LinqHW7()
        {
            //var result = from p in dataSource.Products
            //             group p by p.Category into x
            //             select new { Category = x.Key, Stock = from y in x.ToList() group y by y.UnitsInStock > 0 ? 1 : 0 into z
            //             select new { Stock = z.Key == 0 ? "Sold out" : "In stock", Products = from n in z.ToList() orderby n.UnitPrice
            //             select new { n.ProductName, n.UnitPrice } } };

            var result = dataSource.Products.GroupBy(p => p.Category).
               Select(x => new {Category = x.Key, Stock = x.ToList().GroupBy(y => y.UnitsInStock>0 ? 1 : 0).
               Select(y => new {Stock = y.Key==0? "Sold out" : "In stock", Products = y.ToList().
               Select(z => new {z.ProductName, z.UnitPrice }).OrderBy(z => z.UnitPrice) }) });

            foreach (var c in result)
            {
                ObjectDumper.Write(c,2);
            }
        }

        [Category("HW")]
        [Title("HW - Task 8")]
        [Description("Group products by price")]

        public void LinqHW8()
        {
            //var result = from p in dataSource.Products
            //             group p by p.UnitPrice < 30M ? "Low" : p.UnitPrice > 90M ? "High" : "Medium" into x
            //             select new { Price_Category = x.Key, Product = from y in x.ToList() orderby y.UnitPrice
            //             select new { y.ProductName, y.UnitPrice }};

            var result = dataSource.Products.GroupBy(p => p.UnitPrice < 30M ? "Low" : p.UnitPrice > 90M ? "High" : "Medium").
                Select(x => new { Price_Category = x.Key, Product = x.ToList().
                Select(y => new { y.ProductName, y.UnitPrice }).OrderBy(y => y.UnitPrice)});



            foreach (var c in result)
            {
                ObjectDumper.Write(c, 1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 9")]
        [Description("Average order pre client per city and average number of orders per client per city")]

        public void LinqHW9()
        {
            //var result = from c in dataSource.Customers
            //             group c by c.City into x
            //             orderby x.Key
            //             select new {City = x.Key, AverageOrder = (from y in x.ToList()
            //             select y.Orders.Count() == 0 ? 0 : (from z in y.Orders
            //             select z.Total).Average()).Average(), AverageNumberOfOrders = (from c in x.ToList()
            //             select c.Orders.Count()).Average()
            //             };
            
            //var avgOrderByCustomer = dataSource.Customers.Select(c => new { c.CustomerID, orders = c.Orders.Count()==0 ? 0 : c.Orders.Select(o => o.Total).Average() });
            //var avgNumberOfOrdersByCustomer = dataSource.Customers.Select(c => new { c.CustomerID, orders = c.Orders.Count() });
            
            var result = dataSource.Customers.GroupBy(c => c.City).OrderBy(c=>c.Key).
                Select(x => new { City = x.Key, AverageOrder = x.ToList().
                Select(c=> c.Orders.Count() == 0 ? 0 : c.Orders.
                Select(o => o.Total).Average()).Average(), AverageNumberOfOrders = x.ToList().
                Select(c => c.Orders.Count()).Average()
                });

            foreach (var c in result)
            {
                ObjectDumper.Write(c, 1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 10A")]
        [Description("Client statistics by year")]

        public void LinqHW10A()
        {
            //var result =
            //     from c in dataSource.Customers
            //     from o in c.Orders
            //     orderby o.OrderDate.Year
            //     group o by o.OrderDate.Year into gr
            //     select new { gr.Key, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() };

            //var result =
            //    from c in dataSource.Customers
            //    from o in c.Orders
            //    orderby o.OrderDate.Year
            //    group o by o.OrderDate.Year into gr
            //    select new { gr.Key, total = (from x in gr.ToList() select x.Total).Sum(), count = (from x in gr.ToList() select x.Total).Count() };

            var result = dataSource.Customers.SelectMany(c => c.Orders).OrderBy(o => o.OrderDate.Year).GroupBy(o => o.OrderDate.Year).
                Select(gr => new { gr.Key, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() });

            foreach (var c in result)
            {
                ObjectDumper.Write(c, 1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 10B")]
        [Description("Client statistics by month")]

        public void LinqHW10B()
        {
            //var result =
            //     from c in dataSource.Customers
            //     from o in c.Orders
            //     orderby o.OrderDate.Month
            //     group o by o.OrderDate.ToString("MMM") into gr
            //     select new { gr.Key, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() };

            //var result =
            //    from c in dataSource.Customers
            //    from o in c.Orders
            //    orderby o.OrderDate.Month
            //    group o by o.OrderDate.ToString("MMM") into gr
            //    select new { gr.Key, total = (from x in gr.ToList() select x.Total).Sum(), count = (from x in gr.ToList() select x.Total).Count() };

            var result = dataSource.Customers.SelectMany(c => c.Orders).OrderBy(o => o.OrderDate.Month).GroupBy(o => o.OrderDate.ToString("MMM")).
                Select(gr => new { gr.Key, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() });

            foreach (var c in result)
            {
                ObjectDumper.Write(c, 1);
            }
        }

        [Category("HW")]
        [Title("HW - Task 10C")]
        [Description("Client statistics by month of the year")]

        public void LinqHW10C()
        {
            var result =
                 from c in dataSource.Customers
                 from o in c.Orders
                 orderby o.OrderDate.Year, o.OrderDate.Month
                 group o by new { year = o.OrderDate.Year, month = o.OrderDate.ToString("MMM") } into gr
                 select new { gr.Key.year, gr.Key.month, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() };

            //var result =
            //    from c in dataSource.Customers
            //    from o in c.Orders
            //    orderby o.OrderDate.Year, o.OrderDate.Month
            //    group o by new { year = o.OrderDate.Year, month = o.OrderDate.ToString("MMM") } into gr
            //    select new { gr.Key.year, gr.Key.month, total = (from x in gr.ToList() select x.Total).Sum(), count = (from x in gr.ToList() select x.Total).Count() };

            //var result = dataSource.Customers.SelectMany(c => c.Orders).OrderBy(o => o.OrderDate.Year).ThenBy(o => o.OrderDate.Month).
            //    GroupBy(o => new { year = o.OrderDate.Year, month = o.OrderDate.ToString("MMM") }).
            //    Select(gr => new { gr.Key.year, gr.Key.month, total = gr.ToList().Select(x => x.Total).Sum(), count = gr.ToList().Select(x => x.Total).Count() });

            foreach (var c in result)
            {
                ObjectDumper.Write(c, 1);
            }
        }
    }
}
