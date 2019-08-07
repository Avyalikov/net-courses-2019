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
using Task.Fluent;
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

        public void LinqA1()
        {
           var customers = dataSource.Customers
                .Select(c => new SelectCustomer // i don't want use anon type, i use fluent interface
                {
                    CustomerId = c.CustomerID,
                    CompName = c.CompanyName,
                    TotalSumOrders = c.Orders.Sum(o => o.Total)
                });

            decimal sum = 15000;
            foreach (SelectCustomer customer in customers)
            {
                if (customer.TotalSumOrders > sum)
                    Console.WriteLine(customer.ToString());
            }

            Console.WriteLine("*************************************");

            sum = 25000;
            foreach (SelectCustomer customer in customers)
            {
                if (customer.TotalSumOrders > sum)
                    Console.WriteLine(customer.ToString());
            }
        }

        public void LinqA2()
        {
            var suppplierInCityList = dataSource.Customers
                .Select(c => new SupplierList
                {
                    CustomerId = c.CustomerID,
                    Country = c.Country,
                    City = c.City,
                    SuppliersNameList = dataSource.Suppliers
                    .Where(s => s.City == c.City && s.Country == c.Country)
                    .Select(s => s.SupplierName).ToList()
                });

            foreach(SupplierList supplier in suppplierInCityList)
            {
                Console.WriteLine(supplier.ToString());
            }

            Console.WriteLine("*************************************");
            
            suppplierInCityList = dataSource.Customers
                .GroupJoin(dataSource.Suppliers,
                c => new { c.City, c.Country },
                s => new { s.City, s.Country },
                (c, s) => new SupplierList
                {
                    CustomerId = c.CustomerID,
                    Country = c.Country,
                    City = c.City,
                    SuppliersNameList = s.Select(supl => supl.SupplierName).ToList()
                });

            foreach (SupplierList supplier in suppplierInCityList)
            {
                Console.WriteLine(supplier.ToString());
            }
        }

        public void LinqA3()
        {
            decimal sum = 10000;
            var customers = dataSource.Customers
                .Where(c => c.Orders
                .Any(o => o.Total > sum))
                .Select(c => new SelectCustomer
                {
                    CustomerId = c.CustomerID,
                    CompName = c.CompanyName
                });

            foreach(SelectCustomer customer in customers)
            {
                Console.WriteLine(string.Format("Id {0}\tCompany Name {1}",
                    customer.CustomerId, customer.CompName));
            }
        }

        public void LinqA4()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new SelectCustomer
                {
                    CustomerId = c.CustomerID,
                    FirstOrderDate = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()
                });

            foreach(SelectCustomer customer in customers)
            {
                Console.WriteLine(string.Format("Id {0} first order {2} month {1} year", 
                    customer.CustomerId, customer.FirstOrderDate.Year, customer.FirstOrderDate.Month));
            }
        }

        public void LinqA5()
        {
            var customers = dataSource.Customers
                .Where(c => c.Orders.Any())
                .Select(c => new SelectCustomer
                {
                    CustomerId = c.CustomerID,
                    CompName = c.CompanyName,
                    TotalSumOrders = c.Orders.Sum(o => o.Total),
                    FirstOrderDate = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate).First()
                }).OrderByDescending(c => c.FirstOrderDate.Year)
                .ThenByDescending(c => c.FirstOrderDate.Month)
                .ThenByDescending(c => c.TotalSumOrders)
                .ThenByDescending(c => c.CustomerId);

            foreach(SelectCustomer customer in customers)
            {
                Console.WriteLine(string.Format(
                    "Customer {0} \tTotal sum {1}\tMonth {2} \tYear {3}",
                    customer.CustomerId, customer.TotalSumOrders,
                    customer.FirstOrderDate.Month, customer.FirstOrderDate.Year));
            }
        }

        public void LinqA6()
        {
            var customers = dataSource.Customers
                .Where(c => string.IsNullOrWhiteSpace(c.Region) ||
                c.Phone.First().ToString().ToCharArray()[0] != '(' ||
                c.PostalCode.Any(code => code < '0' || code > '9'))
                .Select(c => c);

            foreach(Customer customer in customers)
            {
                Console.WriteLine(string.Format(
                    "Id {0} \tPostal Code {1} \tPhone {2} \tRegion {3}",
                    customer.CustomerID, customer.PostalCode, customer.Phone, customer.Region));
            }
        }

        public void LinqA7()
        {
            var products = dataSource.Products
                .GroupBy(p => p.Category)
                .Select(s => new ProductsCategory
                {
                    Category = s.Key,
                    OnStock = s.GroupBy(p => p.UnitsInStock > 0)
                    .Select(c => new ProductsOnStock
                    {
                        HasInStock = c.Key,
                        Products = c.OrderBy(p => p.UnitPrice)
                    })
                });

            foreach(ProductsCategory product in products)
            {
                Console.WriteLine(product.ToString());
            }
        }

        public void LinqA8()
        {
            decimal minAverage = 20;
            decimal maxAverage = 100;

            var productsCategory = dataSource.Products
                .OrderBy(i => i.UnitPrice)
                .GroupBy(p => p.UnitPrice < minAverage ? "Cheap"
                : p.UnitPrice < maxAverage ? "Average" : "Expensive");

            foreach(var group in productsCategory)
            {
                Console.WriteLine(group.Key);
                foreach(var item in group)
                {
                    Console.WriteLine(string.Format(
                        "\tProduct {0} Price {1}",
                        item.ProductName, item.UnitPrice));
                }
            }
        }  

        public void LinqA9()
        {
            var statistics = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(s => new OrdersStatisticsByCity
                {
                    City = s.Key,
                    Intensity = s.Average(p => p.Orders.Length),
                    AverageIncome = s.Average
                    (p => p.Orders.Sum(o => o.Total))
                });

            foreach (OrdersStatisticsByCity item in statistics)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void LinqA10()
        {
            var customerStatistics = dataSource.Customers
                .Select(c => new
                {
                    customerId = c.CustomerID,

                    MonthsStat = c.Orders
                    .GroupBy(o => o.OrderDate.Month)
                    .Select(s => new
                    {
                        Month = s.Key,
                        OrderCount = s.Count()
                    }),

                    YearStat = c.Orders
                    .GroupBy(o => o.OrderDate.Year)
                    .Select(s => new
                    {
                        Year = s.Key,
                        OrderCount = s.Count()
                    }),

                    YearMonthStat = c.Orders
                    .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                    .Select(s => new
                    {
                        s.Key.Year,
                        s.Key.Month,
                        OrderCount = s.Count()
                    })
                });

            foreach(var item in customerStatistics)
            {
                Console.WriteLine("Customer {0}", item.customerId);

                Console.WriteLine("\tMonth statistics:");
                foreach (var monthStat in item.MonthsStat)
                {
                    Console.WriteLine("\t\tMonth: {0} Orders count {1}", monthStat.Month, monthStat.OrderCount);
                }

                Console.WriteLine("\tYear statistics:");
                foreach (var yearStat in item.YearStat)
                {
                    Console.WriteLine("\t\tYear: {0} Orders count {1}", yearStat.Year, yearStat.OrderCount);
                }

                Console.WriteLine("\tYear and months statistics:");
                foreach (var yearMonthStat in item.YearMonthStat)
                {
                    Console.WriteLine("\t\tYear {0} Month: {1} Orders count {2}",
                        yearMonthStat.Year, yearMonthStat.Month, yearMonthStat.OrderCount);
                }
            }
        }
    }
}
