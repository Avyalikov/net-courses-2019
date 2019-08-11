// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

// Version Mad01

namespace SampleQueries
{
    using System;
    using System.Linq;
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
                from p in this.dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 1")]
        [Description("This sample return customers name with total turnover more then X")]
        public void Linq3()
        {
            int[] X = { 10000, 25000, 50000 };

            foreach (var x in X)
            {
                var query = this.dataSource.Customers
                    .Where(c => c.Orders.Sum(o => o.Total) > x)
                    .Select(c => c.CompanyName);

                ObjectDumper.Write($"Customers with {x} or more total turnover");
                foreach (var q in query)
                {
                    ObjectDumper.Write(q);
                }

                ObjectDumper.Write(string.Empty);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 2")]
        [Description("This sample return customers and suppliers located in the same city")]
        public void Linq4()
        {
            var сustomers = this.dataSource.Customers;
            var suppliers = this.dataSource.Suppliers;

            var query1 = сustomers
                .Join(
                    suppliers,
                    c => c.City, 
                    s => s.City,
                    (c, s) => new
                    {
                        Company = c.CompanyName,
                        Supplier = s.SupplierName,
                        City = c.City,
                    });

            var query2 = query1
                .GroupBy(q => q.City);

            foreach (var q in query1)
            {
                ObjectDumper.Write(q);
            }

            ObjectDumper.Write(string.Empty);
            ObjectDumper.Write("Grouping by city");
            ObjectDumper.Write(string.Empty);
            foreach (var city in query2)
            {
                ObjectDumper.Write(city.Key);
                foreach (var q in city)
                {
                    ObjectDumper.Write($"Company = {q.Company}\tSupplier = {q.Supplier}");
                }

                ObjectDumper.Write(string.Empty);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 3")]
        [Description("This sample return customer's name which have order priсe more then X")]
        public void Linq5()
        {
            int X = 10000;

            var query = this.dataSource.Customers
                .Where(c =>
                {
                    foreach (var order in c.Orders)
                    {
                        if (order.Total > X)
                        {
                            return true;
                        }
                    }

                    return false;
                })
                .Select(c => c.CompanyName);

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 4")]
        [Description("This sample return customer's name with first order's date")]
        public void Linq6()
        {
            var query = this.dataSource.Customers
                .Where(c => c.Orders.Length > 0)
                .Select(c => new
                {
                    CompanyName = c.CompanyName,
                    Year = c.Orders.Min(o => o.OrderDate).Year,
                    Month = c.Orders.Min(o => o.OrderDate).Month,
                });

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 5")]
        [Description("This sample return ordered customer's name with first order's date")]
        public void Linq7()
        {
            var query = this.dataSource.Customers
                .Where(c => c.Orders.Length > 0)
                .Select(c => new
                {
                    CompanyName = c.CompanyName,
                    Total = c.Orders.Sum(o => o.Total),
                    Year = c.Orders.Min(o => o.OrderDate).Year,
                    Month = c.Orders.Min(o => o.OrderDate).Month,
                })
                .OrderBy(c => c.Year)
                .ThenBy(c => c.Month)
                .ThenByDescending(c => c.Total)
                .ThenBy(c => c.CompanyName);

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 6")]
        [Description("This sample return customer's name with incorrect data")]
        public void Linq8()
        {
            var query = this.dataSource.Customers
                .Where(c => c.PostalCode == null || c.PostalCode.Any(char.IsLetter) || c.Region == null || !c.Phone.StartsWith("("))
                .Select(c => new
                {
                    c.CompanyName,
                    c.PostalCode,
                    c.Region,
                    c.Phone
                });

            int i = 1;
            foreach (var q in query)
            {
                ObjectDumper.Write(i.ToString() + q);
                i++;
            }
        }

        [Category("LINQ samples")]
        [Title("Task 7")]
        [Description("This sample return group of product's category by existing in stock")]
        public void Linq9()
        {
            var query = this.dataSource.Products
                .OrderBy(product => product.UnitPrice)
                .GroupBy(product => product.Category)
                .SelectMany(
                    category => category.GroupBy(product => product.UnitsInStock > 0),
                    (category, inStock) => new
                    {
                        Category = category,
                        InStock = inStock
                    })
                .GroupBy(i => i.Category.Key, i => i.InStock)
                .OrderBy(category => category.Key);

            foreach (var category in query)
            {
                ObjectDumper.Write($"Category = {category.Key}");

                foreach (var inStock in category)
                {
                    ObjectDumper.Write($"\tIn stock: {inStock.Key}");

                    foreach (var product in inStock)
                    {
                        ObjectDumper.Write(product);
                    }

                    ObjectDumper.Write(string.Empty);
                }

                ObjectDumper.Write(string.Empty);
            }
        }
        
        [Category("LINQ samples")]
        [Title("Task 8")]
        [Description("This sample return three group of products")]
        public void Linq10()
        {
            var query = this.dataSource.Products
                .Select(p => new
                {
                    p.Category,
                    p.ProductName,
                    Price =
                        p.UnitPrice < 10.0000M ?
                            "Low price" :
                            (p.UnitPrice > 25.0000M ?
                                "Height price" :
                                "Medium price")
                })
                .GroupBy(p => p.Price);

            foreach (var group in query)
            {
                ObjectDumper.Write(group.Key);
                ObjectDumper.Write(string.Empty);

                foreach (var p in group)
                {
                    ObjectDumper.Write(p);
                }

                ObjectDumper.Write(string.Empty);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 9")]
        [Description("This sample return city's average profit and average intensity")]
        public void Linq11()
        {
            var query = this.dataSource.Customers
                .Select(c => new
                    {
                        c.City,
                        AP = c.Orders.Length == 0 ? 0 : c.Orders.Sum(o => o.Total),
                        AI = c.Orders.Length == 0 ? 0 : c.Orders.Count()
                    })
                .GroupBy(
                    c => c.City,
                    (key, item) => new
                    {
                        City = key,
                        AverageProfit = item.Sum(i => i.AP) / item.Sum(i => i.AI),
                        AverageIntensity = item.Average(i => i.AI)
                    })
                .OrderBy(c => c.City);

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("LINQ samples")]
        [Title("Task 10")]
        [Description("This sample return three group of products")]
        public void Linq12()
        {
            var orderDate = this.dataSource.Customers.SelectMany(c => c.Orders.Select(o => o.OrderDate));

            var query1 = orderDate
                .GroupBy(
                    od => od.Month, 
                    (key, item) => new
                    {
                        Month = key,
                        Orders = item.Count() / item.Select(i => i.Year).Distinct().Count()
                    })
                .OrderBy(m => m.Month);

            var query2 = orderDate
                .GroupBy(
                    od => od.Year, 
                    (key, item) => new
                    {
                        Year = key,
                        Orders = item.Count()
                    })
                .OrderBy(m => m.Year);

            var query3 = orderDate
                .OrderBy(od => od.Year)
                .GroupBy(od => od.Year)
                .SelectMany(
                    dt => dt.GroupBy(
                        d => d.Month,
                        (key, item) => new
                        {
                            Month = key,
                            Orders = item.Count()
                        })
                        .OrderBy(m => m.Month),
                    (year, month) => new
                    {
                        Year = year,
                        Month = month,
                    })
                .GroupBy(i => i.Year.Key, i => i.Month);

            ObjectDumper.Write("Customers activity by months");
            foreach (var q in query1)
            {
                ObjectDumper.Write(q);
            }

            ObjectDumper.Write(string.Empty);

            ObjectDumper.Write("Customers activity by years");
            foreach (var q in query2)
            {
                ObjectDumper.Write(q);
            }

            ObjectDumper.Write(string.Empty);

            ObjectDumper.Write("Customers activity by months in each year");
            foreach (var year in query3)
            {
                ObjectDumper.Write($"Year = {year.Key}");

                foreach (var mounth in year)
                {
                    ObjectDumper.Write(mounth);
                }

                ObjectDumper.Write(string.Empty);
            }
        }
    }
}