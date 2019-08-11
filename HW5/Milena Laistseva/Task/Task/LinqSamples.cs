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
        
        [Category("Homework")]
        [Title("HW - Task 1")]
        [Description("Shows customers whose orders total is more than X")]

        public void LinqA01()
        {
            var customers = dataSource.Customers.
                Select(c => new { c.CustomerID, c.CompanyName, Total = c.Orders.Sum(o => o.Total) });

            void CustomerTotalMoreThanX (decimal X)
            {
                foreach (var customer in customers)
                {
                    if (customer.Total > X)
                    {
                        ObjectDumper.Write(customer);
                    }
                }
            }

            CustomerTotalMoreThanX(10000);
            ObjectDumper.Write("____________________________");
            CustomerTotalMoreThanX(50000);
        }

        [Category("Homework")]
        [Title("HW - Task 2.1 wihtout group by")]
        [Description("Shows customers and suppliers from one city")]

        public void LinqA02_1()
        {
            var customers = dataSource.Customers.
                Select(c => new
                {
                    Customers = c.CompanyName,
                    Suppliers = dataSource.Suppliers.Where(s => s.City == c.City && s.Country == c.Country).
                    Select (s => new
                    {
                        Customer = c.CompanyName,
                        Supplier = s.SupplierName,
                        Country = s.Country,
                        City = s.City
                    })
                });

            foreach (var c in customers)
            {
                //ObjectDumper.Write(c.Customer);
                foreach (var s in c.Suppliers)
                {
                    ObjectDumper.Write(s);
                }
            }
        }

        [Category("Homework")]
        [Title("HW - Task 2.2 with group by")]
        [Description("Shows customers and suppliers from one city")]

        public void LinqA02_2()
        {
            var customers = dataSource.Customers.GroupBy(x => new { x.Country, x.City}).
                Select(c => new
                {
                    Countries = c.Key.Country,
                    Cities = c.Key.City,
                    Suppliers = dataSource.Suppliers.
                    Where(s => s.Country == c.Key.Country && s.City == c.Key.City).
                    Select(y => new
                    {
                        Supplier = y.SupplierName,
                        Country = y.Country,
                        City = y.City,                       
                    })

                });

            foreach (var c in customers)
            {
                //ObjectDumper.Write(c.Customer);
                foreach (var s in c.Suppliers)
                {
                    ObjectDumper.Write(s);
                }
            }
        }

        [Category("Homework")]
        [Title("HW - Task 3")]
        [Description("Shows orders more than X")]

        public void LinqA03()
        {
            int X = 7000;

            var customers = dataSource.Customers.Where(c => c.Orders.Any(o => o.Total > X)).
                Select(c => new { c.CompanyName, MaxOrder = c.Orders.Max(o => o.Total) });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("HW - Task 4")]
        [Description("Shows customers first order date")]

        public void LinqA04()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Any()).
                Select(c => new { c.CompanyName, FirstOrderDate = c.Orders.Min(o => o.OrderDate) });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("HW - Task 5")]
        [Description("Shows customers first order date after sorting")]

        public void LinqA05()
        {
            var customers = dataSource.Customers.Where(c => c.Orders.Any()).
                Select(c => new { c.CompanyName, FirstOrderDate = c.Orders.Min(o => o.OrderDate), Turnover = c.Orders.Sum(o => o.Total) }).
                OrderBy(c => c.CompanyName).
                ThenBy(c => c.FirstOrderDate.Year).
                ThenBy(c => c.FirstOrderDate.Month).
                ThenByDescending(c => c.Turnover);

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("HW - Task 6")]
        [Description("Shows customers without region, or with non numerical index, or wrong phone number")]
        public void LinqA06()
        {
            int NumberCode;
            var customers = dataSource.Customers.Where(c => c.Region == null || 
            (!int.TryParse(c.PostalCode, out NumberCode)) || (c.Phone.First() != '(')).
            Select (c => new { c.CompanyName, c.Region, c.PostalCode, c.Phone});

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Homework")]
        [Title("HW - Task 7")]
        [Description("Groups products by categories, by availability in stocks and by price")]
        public void LinqA07()
        {
            var products = dataSource.Products.GroupBy(p => p.Category).
                Select(s => new
                {
                    s.Key,
                    InStock = s.GroupBy(p => p.UnitsInStock > 0).
                    Select(p => new {p.Key, Products = p.OrderBy(u => u.UnitPrice) })
                });

            foreach (var p in products)
            {
                ObjectDumper.Write(p, 3);
            }

        }

        [Category("Homework")]
        [Title("HW - Task 8")]
        [Description("Groups products by price")]
        public void LinqA08()
        {
            decimal minPrice = 10;
            decimal maxPrice = 80;

            var products = dataSource.Products.OrderBy(o => o.UnitPrice).
                GroupBy(p => p.UnitPrice < minPrice ? "Cheap" : p.UnitPrice > maxPrice ? "Expensive"
                : "Average").Select(g => new { ProductPrice = g.Key, Count = g.Count()});

            foreach(var p in products)
            {
                ObjectDumper.Write(p);
            }

        }

        [Category("Homework")]
        [Title("HW - Task 9")]
        [Description("Returns average city statistics")]
        public void LinqA09()
        {
            var cityStatistics = dataSource.Customers.GroupBy(c => c.City).
                Select(g => new
                {
                    City = g.Key,
                    averageProfit = g.Average(o => o.Orders.Sum(t => t.Total)),
                    averageIntencity = g.Average(o => o.Orders.Length)
                });

            foreach (var city in cityStatistics)
            {
                ObjectDumper.Write(city);
            }
        }

        [Category("Homework")]
        [Title("HW - Task 10")]
        [Description("Returns orders statistics by months and years")]
        public void LinqA10()
        {
            var statisticByMonths = dataSource.Customers.
                Select(c => new
                {
                    Customer = c.CompanyName,
                    OrdersByMonth = c.Orders.
                    GroupBy(o => o.OrderDate.Month).
                    Select(g => new
                    {
                        Month = g.Key,
                        Total = g.Count()
                    })
                });

            ObjectDumper.Write("Statistics by months");
            foreach (var i in statisticByMonths)
            {
                ObjectDumper.Write(i.Customer);
                foreach(var month in i.OrdersByMonth)
                {
                    ObjectDumper.Write(month);
                }
            }

            var statisticByYears = dataSource.Customers.
                Select(c => new
                {
                    Customer = c.CompanyName,
                    OrdersByYear = c.Orders.
                    GroupBy(o => o.OrderDate.Year).
                    Select(g => new
                    {
                        Year = g.Key,
                        Total = g.Count()
                    })
                });

            ObjectDumper.Write("____________________________");
            ObjectDumper.Write("Statistics by years");
            foreach (var i in statisticByYears)
            {
                ObjectDumper.Write(i.Customer);
                foreach (var year in i.OrdersByYear)
                {
                    ObjectDumper.Write(year);
                }
            }

            var statisticByYearsAndMonths = dataSource.Customers.
                Select(c => new
                {
                    Customer = c.CompanyName,
                    OrdersByYearAndMonths = c.Orders.
                    GroupBy(o =>new { o.OrderDate.Year, o.OrderDate.Month }).
                    Select(g => new
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        Total = g.Count()
                    })
                });

            ObjectDumper.Write("____________________________");
            ObjectDumper.Write("Statistics by years and months");
            foreach (var i in statisticByYearsAndMonths)
            {
                ObjectDumper.Write(i.Customer);
                foreach (var date in i.OrdersByYearAndMonths)
                {
                    ObjectDumper.Write(date);
                }
            }
        }
    }
}
