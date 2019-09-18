// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        [Description("This sample returns customers with a turnover more than given value")]
        public void Linq1()
        {
            decimal x = 10000;
            var cust = dataSource.Customers.
                Where(c => c.Orders.Sum(t => t.Total) > x).
                Select(c =>c);

            foreach (var c in cust)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2a")]
        [Description("This sample returns suppliers foreach customer located in the same country and city")]
        public void Linq2a()
        {
            var custAndSupp = dataSource.Customers.Join(dataSource.Suppliers, cust=> cust.Country, supp=> supp.Country, (cast,supp) => new {cast, supp}).
                Where(w => w.cast.City == w.supp.City).
                Select(c => new { c.cast.CompanyName, Supplier = new { c.supp.SupplierName, c.supp.Country, c.supp.City, c.supp.Address}  });

            foreach (var c in custAndSupp)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2b")]
        [Description("This sample returns suppliers foreach customer located in the same country and city with grouping")]
        public void Linq2b()
        {
            var custAndSupp = dataSource.Customers.Join(dataSource.Suppliers, cust => cust.Country, supp => supp.Country, (cast, supp) => new { cast, supp }).
                Where(w => w.cast.City == w.supp.City).
                GroupBy(g => g.cast.CompanyName, castandsupp => new { castandsupp.supp.SupplierName, castandsupp.supp.Country, castandsupp.supp.City, castandsupp.supp.Address}).
                Select(s => new { CompanyName = s.Key, Supplier = s });

            foreach (var c in custAndSupp)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("This sample returns customers with orders totals more than given value")]
        public void Linq3()
        {
            decimal x = 10000;
            var cust = dataSource.Customers.
                Where(c => c.Orders.Any(t => t.Total > x)).
                Select(c => c);

            foreach (var c in cust)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample returns customers with the date of first order")]
        public void Linq4()
        {
            var cust = dataSource.Customers.
                Where(w => w.Orders.Any()).
                Select(c => new { c.CompanyName, c.Orders.OrderBy(o => o.OrderDate).First().OrderDate.Year, c.Orders.OrderBy(o => o.OrderDate).First().OrderDate.Month });

            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return return customers validated on postalcode, region, phone")]
        public void Linq6()
        {
            var cust = dataSource.Customers.
                Where(w => w.PostalCode == null ? false : Regex.IsMatch(w.PostalCode, "[A-Z]") || w.Region == null || !w.Phone.Contains("(")).
                Select(c => new {c.CompanyName, c.PostalCode, c.Region, c.Phone });
                

            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Ordering Operators")]
        [Title("Where - Task 5")]
        [Description("This sample returns sorted customers list with the date of first order ")]
        public void Linq5()
        {
            var cust = dataSource.Customers.
                Where(w => w.Orders.Any()).
                Select(c => new { c.CompanyName, c.Orders.OrderBy(o => o.OrderDate).First().OrderDate.Year, c.Orders.OrderBy(o => o.OrderDate).First().OrderDate.Month, c.Orders.OrderBy(o => o.OrderDate).First().Total }).
                OrderBy(o => o.Year).ThenBy(o => o.Month).ThenByDescending(o => o.Total).ThenBy(o => o.CompanyName);

            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }
        
        [Category("Grouping Operators")]
        [Title("Where - Task 7")]
        [Description("This sample returns productes grouped by categories, availability in stock")]
        public void Linq7()
        {
            var cust = dataSource.Products.GroupBy(g => g.Category).
                Select(s => new { Category = s.Key, Availability = s.GroupBy(g => g.UnitsInStock > 0 ? "Yes" : "No").
                                                                     Select(s1 => new { Available = s1.Key, Products = s1.OrderBy(o => o.UnitPrice) }) });


            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Grouping Operators")]
        [Title("Where - Task 8")]
        [Description("This sample returns products grouped by ranges")]
        public void Linq8()
        {
            var cust = dataSource.Products.GroupBy(g => g.UnitPrice <= 20m ? "cheap" : g.UnitPrice <= 40m ? "average" : "expensive", res => new { res.ProductName, res.UnitPrice }).
            Select(s => new { Value = s.Key, Product = s });


            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Grouping Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return average income statistics ")]
        public void Linq9()
        {
            var cities = dataSource.Customers.Where(w => w.Orders.Any()).Select(s=>new {s.City, AvgTotal = s.Orders.Average(a=>a.Total), Quantity = s.Orders.Count()});
            var cust = cities.GroupBy(g => g.City).Select(s=>new {Cit = s.Key, Average_Total = Math.Round(s.Average(a=>a.AvgTotal),2), ntensivity = Math.Round(s.Average(a=>a.Quantity),2)});
                
            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }

        [Category("Grouping Operators")]
        [Title("Where - Task 10")]
        [Description("This sample return activity statistics")]
        public void Linq10()
        {
            //statistics on quantity of orders 
            var AllOrders = dataSource.Customers.SelectMany(s => s.Orders).OrderBy(o=>o.OrderDate.Month);

            var MonthStat = AllOrders.GroupBy(g => g.OrderDate.Month).
                Select(s => new { Month=s.Key, ClientsActivity = s.Count() });

            foreach (var c in MonthStat)
            {
                ObjectDumper.Write(c, 2);
            }

            var YearStat = AllOrders.GroupBy(g => g.OrderDate.Year).
                Select(s => new { Year = s.Key, ClientsActivity = s.Count() });

            foreach (var c in YearStat)
            {
                ObjectDumper.Write(c, 2);
            }

            var YearMonthStat = AllOrders.GroupBy(g => g.OrderDate.Year).
                Select(s => new { Year = s.Key, Months = s.GroupBy(g=>g.OrderDate.Month).Select(s1=>new { Month=s1.Key, ClientsActivity = s1.Count() })});
            
            foreach (var c in YearMonthStat)
            {
                ObjectDumper.Write(c, 2);
            }
        }
    }
}
