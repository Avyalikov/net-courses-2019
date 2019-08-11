// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using SampleSupport;
using Task.Northwind;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private NorthwindDBDataContext db = new NorthwindDBDataContext();
        private DataSource dataSource = new DataSource();
        private List<string> months = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        [Category("SQL to XML")]
        [Title("Where - Task Example 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void LinqExample1()
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

        [Category("SQL to XML")]
        [Title("Where - Task Example 2")]
        [Description("This sample return return all presented in market products")]

        public void LinqExample2()
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

        [Category("SQL to XML")]
        [Title("Where - Task 1")]
        [Description("This sample return list of all customers whose total of all orders exceeds a certain value X")]

        public void Linq1()
        {
            decimal someValueX = 34563.0M;
            var customers =
                dataSource.Customers
                    .Where(o => o.Orders.Sum(t => t.Total) > someValueX)
                    .Select(c => new
                    {
                        customers = c.CustomerID,
                        total = c.Orders.Sum(t => t.Total)
                    }
                    );

            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Total orders = " + customer.total);
            }

            someValueX = 55563.0M;
            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Total orders = " + customer.total);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 2_1")]
        [Description("Ths sample return a list of suppliers located in the same country and the same city")]

        public void Linq2_1()
        {
            var customers =
               from c in dataSource.Customers
               from s in dataSource.Suppliers
               where c.Country == s.Country && c.City == s.City
               select new
               {
                   city = c.City,
                   country = c.Country,
                   name = c.CustomerID,
                   supplier = s.SupplierName
               };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c.name + "\tsupplier = " + c.supplier + "\tCity = " + c.city + "\tCountry = " + c.country);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 2_2")]
        [Description("Ths sample return a list of suppliers located in the same country and the same city (using group)")]

        public void Linq2_2()
        {
            var customers = dataSource.Customers
                .Join(dataSource.Suppliers,
                        c => new { c.City, c.Country },
                        s => new { s.City, s.Country },
                        (c, s) => new
                        {
                            name = c.CustomerID,
                            supplier = s.SupplierName,
                            city = c.City,
                            country = c.Country
                        }).GroupBy(c => c.name);


            foreach (var p in customers)
            {
                ObjectDumper.Write(p.Key);
                ObjectDumper.Write(p);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 3")]
        [Description("This sample return list of all customers whose total price of all orders exceeds a certain value X")]

        public void Linq3()
        {
            decimal someValueX = 3456.0M;

            var customers = dataSource.Customers
                .Where(s => s.Orders
                .Any(o => o.Total > someValueX))
                //.Where(o => o.Orders.Any())
                //.Where(s => s.Orders.Sum(t => t.Total) > someValueX)
                .Select(c => new
                {
                    customers = c.CustomerID,
                    total = c.Orders.Max(t => t.Total)
                }
                );

            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Total = " + customer.total);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 4")]
        [Description("This sample returns a list of all customers with a first order date")]

        public void Linq4()
        {
            var customers = dataSource.Customers
                .Where(o => o.Orders.Any())
                .Select(c => new
                {
                    customers = c.CustomerID,
                    firstOrder = c.Orders.Min(m => m.OrderDate),
                }
                );

            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Month = " + months[customer.firstOrder.Month - 1] +
                    " Year = " + customer.firstOrder.Year);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 5")]
        [Description("This sample returns a list of all customers with sort by year, month, total price of orders (desc), name")]

        public void Linq5()
        {
            var customers = dataSource.Customers
                .Where(o => o.Orders.Any())
                .Select(c => new
                {
                    firstOrderYear = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate.Year).First(),
                    firstOrderMonth = c.Orders.OrderBy(o => o.OrderDate).Select(o => o.OrderDate.Month).First(),
                    total = c.Orders.Sum(o => o.Total),
                    name = c.CustomerID,
                })
                .OrderBy(c => c.firstOrderYear)
                .ThenBy(c => c.firstOrderMonth)
                .ThenByDescending(c => c.total)
                .ThenBy(c => c.name);

            foreach (var customer in customers)
            {
                ObjectDumper.Write("Year = " + customer.firstOrderYear + " Month = " + customer.firstOrderMonth
                        + " Total = " + customer.total + " Name = " + customer.name);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 6")]
        [Description("This sample returns a list of all clients who have a non-digital postal code or a region is not filled out or an operator code is not indicated on the phone")]

        public void Linq6()
        {
            var customers = dataSource.Customers
                .Where(c => String.IsNullOrEmpty(c.PostalCode) || !(c.PostalCode.All(char.IsDigit)) ||
                            String.IsNullOrEmpty(c.Region) || c.Phone.First() != '(')
                .Select(c => new
                {
                    customers = c.CustomerID,
                    postalCode = c.PostalCode,
                    region = c.Region,
                    phone = c.Phone
                }
                );
            foreach (var customer in customers)
            {
                string region = customer.region;
                if (String.IsNullOrEmpty(customer.region))
                    region = "null";
                ObjectDumper.Write($"Name: {customer.customers}");
                ObjectDumper.Write("PostalCode = " + customer.postalCode + " Region \"" + region + "\" Phone = " + customer.phone);
            }
        }


        [Category("SQL to XML")]
        [Title("Where - Task 7")]
        [Description("This sample return list of group of all products by categories, inside - by stock status, inside the last group sort by price")]

        public void Linq7()
        {
            var products =
                         dataSource.Products
                         .GroupBy(p => p.Category)
                         .Select(s => new
                         {
                             category = s.Key,
                             stockStatus = s.GroupBy(p => p.UnitsInStock > 0)
                                 .Select(c => new
                                 {
                                     availableInStock = c.Key,
                                     productPrice = c.OrderBy(p => p.UnitPrice)
                                 })
                         });

            foreach (var p in products)
            {
                ObjectDumper.Write(p.category);
                foreach (var c in p.stockStatus)
                {
                    ObjectDumper.Write(c.availableInStock ? "Available In Stock" : "Not Available In Stock");
                    foreach (var a in c.productPrice)
                        ObjectDumper.Write("price =  " + a.UnitPrice + " product: " + a.ProductName);
                }
                ObjectDumper.Write(String.Empty);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 8")]
        [Description("This sample return list of group of the products like \"cheap\", \"average\", \"expensive\" price")]

        public void Linq8()
        {
            decimal low = 50;
            decimal high = 100;

            var sortProducts =
                dataSource.Products
                    .GroupBy(p => p.UnitPrice < low ? "Cheap" : p.UnitPrice < high ? "Average" : "Expensive");

            foreach (var sort in sortProducts)
            {
                ObjectDumper.Write("*************");
                ObjectDumper.Write(sort.Key);
                ObjectDumper.Write("*************");
                foreach (var c in sort)
                    ObjectDumper.Write($"Product: {c.ProductName} Price= {c.UnitPrice}");
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 9")]
        [Description("This sample return the average profitability of each city and average intensity of each city")]

        public void Linq9()
        {
            var avg = dataSource.Customers
                .GroupBy(c => c.City)
                .Select(d => new
                {
                    city = d.Key,
                    averageOrdersSum = d.Average(c => (c.Orders.Sum(t => t.Total))),
                    averagePerOrder = d.Average(c => c.Orders.Count())
                });

            foreach (var p in avg)
            {
                ObjectDumper.Write("City: " + p.city);
                ObjectDumper.Write("Average profitability = " + p.averageOrdersSum);
                ObjectDumper.Write("Average intensity = " + p.averagePerOrder);
                ObjectDumper.Write("*************");
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 10_1")]
        [Description("This sample return count of orders for each month")]

        public void Linq10_1()
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

            var avgMonth = perMonth.Average(x => x.ordersCount);

            ObjectDumper.Write("Average orders per months = " + avgMonth);
            foreach (var avg in perMonth)
            {
                ObjectDumper.Write(months[avg.Key - 1] + "\tCount of orders = " + avg.ordersCount);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 10_2")]
        [Description("This sample return count of orders for each year")]

        public void Linq10_2()
        {
            var perYear = dataSource.Customers
                .SelectMany(o => o.Orders.Select(y => y.OrderDate))
                .GroupBy(m => m.Year)
                .Select(s => new
                {
                    s.Key,
                    ordersCount = s.Count()
                })
                .OrderBy(m => m.Key);

            var avgYear = perYear.Average(x => x.ordersCount);

            ObjectDumper.Write("Average orders per year = " + avgYear);
            foreach (var avg in perYear)
            {
                ObjectDumper.Write("Year = " + avg.Key + "\tCount of orders = " + avg.ordersCount);
            }
        }

        [Category("SQL to XML")]
        [Title("Where - Task 10_3")]
        [Description("This sample return count of orders for each years,months")]

        public void Linq10_3()
        {
            var perYearMonth = dataSource.Customers
                .SelectMany(o => o.Orders.Select(y => y.OrderDate))
                .GroupBy(m => new { m.Year, m.Month })
                .Select(s => new
                {
                    month = s.Key.Month,
                    year = s.Key.Year,
                    ordersCount = s.Count()
                }).OrderBy(y => y.year);

            foreach (var avg in perYearMonth)
            {
                ObjectDumper.Write(avg.year + ", " + months[avg.month - 1] + "\tCount of orders = " + avg.ordersCount);
            }
        }

        [Category("SQL to DB")]
        [Title("Where - Task 1")]
        [Description("This sample return list of all customers whose total of all orders exceeds a certain value X")]
        public void Linq11()
        {
            int someValueX = 10;

            var customers =
                db.Customers
                    .Where(o => o.Orders.Count() > someValueX)
                    .Select(c => new
                    {
                        customers = c.CustomerID,
                        total = c.Orders.Count()
                    }
                    );

            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Total orders = " + customer.total);
            }

            someValueX = 25;
            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.customers + " Total orders = " + customer.total);
            }
        }


        [Category("SQL to DB")]
        [Title("Where - Task 3")]
        [Description("This sample return list of all customers whose total price of all orders exceeds a certain value X")]

        public void Linq13()
        {
            decimal someValueX = 3456.0M;

            var customers =
             db.Customers
                 .Where(x => x.Orders.Any(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount))) > someValueX))
                 .Select(x => new
                 {
                     name = x.CustomerID,
                     order = x.Orders.Max(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount))))
                 });

            ObjectDumper.Write("Value to check: " + someValueX);
            foreach (var customer in customers)
            {
                ObjectDumper.Write("Name: " + customer.name + " Total = " + customer.order);
            }
        }

        [Category("SQL to DB")]
        [Title("Where - Task 6")]
        [Description("This sample returns a list of all clients who have a non-digital postal code or a region is not filled out or an operator code is not indicated on the phone")]

        public void Linq16()
        {
            var customers = db.Customers
                .Where(c => c.PostalCode == null || (c.PostalCode.Contains("%[0-9]%")) ||
                            c.Region == null || !c.Phone.StartsWith("("))
                .Select(c => new
                {
                    customers = c.CustomerID,
                    postalCode = c.PostalCode,
                    region = c.Region,
                    phone = c.Phone
                }
                );

            //ObjectDumper.Write($"Count: {customers.Count()}");
            foreach (var customer in customers)
            {
                string region = customer.region;
                if (String.IsNullOrEmpty(customer.region))
                    region = "null";
                ObjectDumper.Write($"Name: {customer.customers}");
                ObjectDumper.Write("PostalCode = " + customer.postalCode + " Region \"" + region + "\" Phone = " + customer.phone);
            }
        }
    }

}