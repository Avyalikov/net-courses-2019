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
using System.Linq.Expressions;
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
        [Description("This sample returns customers with a turnover more than given value")]

        public void Linq001()
        {
            decimal v = 10000;
            var customers =
                 from c in dataSource.Customers
                 where c.Orders.Sum(o => o.Total) > v
                 select c;

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }

        }


        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample returns suppliers foreach customer located in the same country and city")]

        public void Linq002()
        {
            var customersAndSuppliers =
                 from cust in dataSource.Customers
                 join sup in dataSource.Suppliers
                 on cust.Country equals sup.Country
                 where cust.City == sup.City
                 select new { cust.CompanyName, sup.SupplierName, cust.Country, cust.City };

            foreach (var c in customersAndSuppliers)
            {
                ObjectDumper.Write(c, 2);
            }

        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2a")]
        [Description("This sample returns suppliers foreach customer located in the same country and city with grouping")]

        public void Linq002a()
        {
            var customersAndSuppliers =
                 from cust in dataSource.Customers
                 join sup in dataSource.Suppliers
                 on cust.Country equals sup.Country
                 where cust.City == sup.City
                 select new { cust.CompanyName, sup.SupplierName, cust.Country, cust.City };

            var grouped = from c in customersAndSuppliers
                          group c by c.City into cities
                          select new
                          {
                              cities.Key,
                              CompanySuppliers = from g in cities
                                                 select new
                                                 {
                                                     g.CompanyName,
                                                     g.SupplierName
                                                 }
                          };



            foreach (var c in grouped)
            {
                ObjectDumper.Write(c, 2);
            }

        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("This sample returns customers with orders totals more than given value")]

        public void Linq003()
        {
            decimal v = 10000;
            var customers =
                 from c in dataSource.Customers
                 where c.Orders.Any(o => o.Total > v)
                 select c;

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }


        }

        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("This sample returns customers with the date of first order")]

        public void Linq004()
        {

            var customers =
            from cust in dataSource.Customers
            .Where(o => o.Orders.Any())
            select new
            {
                cust.CompanyName,
                Year = (cust.Orders.First().OrderDate.Year),
                Month = (cust.Orders.First().OrderDate.Month)
            };
            foreach (var c in customers)
            {
                if (c != null)
                    ObjectDumper.Write(c);
            }
        }



        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return return customers validated on postalcode, region, phone")]

        public void Linq006()
        {
            Regex notnumeric = new Regex(@"(\D)");
            var customers =
                 from c in dataSource.Customers
                 where
                 c.PostalCode == null ? false : notnumeric.IsMatch(c.PostalCode, 0)
                 || c.Region is null
                 || !c.Phone.StartsWith("(")
                 select new { c.CompanyName, c.PostalCode, c.Region, c.Phone };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Ordering Operators")]
        [Title("Where - Task 5")]
        [Description("This sample returns sorted customers list with the date of first order ")]

        public void Linq005()
        {

            var customers =
         from cust in dataSource.Customers.Where(o => o.Orders.Any())
         select new
         {
             cust.CompanyName,
             Year = (cust.Orders.First().OrderDate.Year),
             Month = (cust.Orders.First().OrderDate.Month),
             Total = cust.Orders.Sum(or => or.Total)
         };
            var sorted = customers.OrderBy(o => o.Year).ThenBy(o => o.Month).ThenByDescending(o => o.Total).ThenBy(c => c.CompanyName);


            foreach (var c in sorted)
            {
                if (c != null)
                    ObjectDumper.Write(c.CompanyName + "\t" + c.Year + "\t" + c.Month + "\t" + c.Total);
            }
        }


        [Category("Grouping Operators")]
        [Title("Where - Task 7")]
        [Description("This sample returns productes grouped by categories, availability in stock")]
        public void Linq007()
        {
            var queryGroup =
           from product in dataSource.Products
           group product by product.Category into productCategories
           select new
           {
               Category = productCategories.Key,
               StockGroups =
                   (from prod in productCategories
                    group prod by prod.UnitsInStock > 0 ? "available" : "not available" into stockGroups
                    select new
                    {
                        StockGroup = stockGroups.Key,
                        Product =
                         (from prod in stockGroups
                          orderby prod.UnitPrice
                          select new { prod.ProductName, prod.UnitPrice }
                          )
                    })
           };
            ObjectDumper.Write(queryGroup, 2);
        }




        [Category("Grouping Operators")]
        [Title("Where - Task 8")]
        [Description("This sample returns products grouped by ranges")]

        public void Linq008()
        {
            var avgPrice = dataSource.Products.Average(p => p.UnitPrice);
            Dictionary<string, decimal> ranges = new Dictionary<string, decimal>();
            ranges.Add("cheap", Math.Round(avgPrice * (decimal)0.67));
            ranges.Add("average", Math.Round(avgPrice * (decimal)1.33));
            ranges.Add("expensive", Math.Round(dataSource.Products.Max(p => p.UnitPrice)));

            var grouped = dataSource.Products.Select(p => new { p.ProductName, p.UnitPrice }).OrderBy(p => p.UnitPrice).GroupBy(x => ranges.FirstOrDefault(r => r.Value > x.UnitPrice)).Select(gProduct => new { gProduct.Key, gProduct });
            ObjectDumper.Write(grouped, 2);
        }



        [Category("Grouping Operators")]
        [Title("Where - Task 9")]
        [Description("This sample return average income statistics ")]

        public void Linq009()
        {
            var sumbycity = from ord in dataSource.Customers.Where(o => o.Orders.Any())
                            select new
                            {
                                ord.City,
                                Totals = ord.Orders.Sum(o => o.Total),
                                Amount = ord.Orders.Count()
                            };

            var grouped = from q in sumbycity
                          group q by q.City into cities
                          select new
                          {
                              City = cities.Key,
                              Totals = Math.Round(cities.Average(c => c.Totals),2),
                              Amount = Math.Round(cities.Average(c => c.Amount),2),
                          };


            foreach (var c in grouped)
            {
                if (c != null)
                    ObjectDumper.Write(c);
            }

        }

        [Category("Grouping Operators")]
        [Title("Where - Task 10")]
        [Description("This sample return activity statistics")]

        public void Linq010()
        {

            var ordersstatisticsm = from c in dataSource.Customers.SelectMany(o => o.Orders.Select(od => od.OrderDate)).OrderBy(m => m.Month).GroupBy(m => m.Month)
                                    select new { c.Key, AverageOrdersPerMonth = (c.Count() / c.Select(oy => oy.Year).Distinct().Count()) };

            foreach (var m in ordersstatisticsm)
            {
                ObjectDumper.Write(m);
            }

            var ordersstatisticsy = from c in dataSource.Customers.SelectMany(o => o.Orders.Select(od => od.OrderDate)).OrderBy(y => y.Year).GroupBy(y => y.Year)
                                    select new { c.Key, OrdersPerYear = c.Count() };

            foreach (var y in ordersstatisticsy)
            {
                ObjectDumper.Write(y);
            }


            var ordersstatisticsym = from c in dataSource.Customers.SelectMany(o => o.Orders.Select(od => od.OrderDate)).OrderBy(y => y.Year).ThenBy(m => m.Month)//GroupBy(ym => new { ym.Year, ym.Month })
                                     group c by c.Year into years
                                     select new
                                     {
                                         Year = years.Key,
                                         Month = from m in years
                                                 group m by m.Month into months
                                                 select new { Month = months.Key, OrdersPerYear_Month = months.Count() }
                                     };

            foreach (var o in ordersstatisticsym)
            {
                ObjectDumper.Write(o, 2);
            }
        }

    }
}

