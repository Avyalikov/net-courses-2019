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

        [Category("Home Task XML")]
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

        [Category("Home Task XML")]
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

        /*[Category("Home Task XML")]
        [Title("Linq002x")]
        [Description("This sample return all customers and suppliers in same city and country with group by")]

        public void Linq002x()
        {
            var clients =
                from c in dataSource.Customers
                from s in dataSource.Suppliers
                group new {c,s} by new { s.City, s.Country } into g
                select g;

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }*/

        [Category("Home Task XML")]
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

        [Category("Home Task XML")]
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

        [Category("Home Task XML")]
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

        [Category("Home Task XML")]
        [Title("Linq006")]
        [Description("This sample return all customers with specific properties")]

        public void Linq006()
        {
            var clients = dataSource.Customers.Where(x => x.Region == null || x.Phone[0]!='(' || !x.PostalCode.All(char.IsDigit));

            foreach (var c in clients)
            {
                ObjectDumper.Write(c);
            }
        }

        /*[Category("Home Task XML")]
        [Title("Linq007")]
        [Description("This sample return categorized products")]

        public void Linq007()
        {
            var products = dataSource.Products.GroupBy(x=>x.Category).GroupBy(x=>x);

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }*/
    }
}
