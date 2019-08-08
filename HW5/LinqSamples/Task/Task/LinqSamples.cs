// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using SampleSupport;
using System.Collections.Generic;
using System.Linq;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {
        private DataSource dataSource = new DataSource();

        public class Customers
        {
            public string CompanyName;
            public decimal Total;
        }
        [Category("Basic LINQ Operations")]
        [Title("TaskA01-Customers w/total")]
        [Description("Get all customers that spend more than X on products")]
        public void LinqA01()
        {
            IEnumerable<Customers> customers =
               from cust in dataSource.Customers
               orderby cust.Orders.Sum(o => o.Total) descending
               select new Customers { CompanyName = cust.CompanyName, Total = cust.Orders.Sum(o => o.Total) };
            
            void ShowCustomerIfTotal(decimal total)
            {
                foreach (var customer in customers)
                {
                    if(customer.Total>total)
                        ObjectDumper.Write(customer);
                }
            }

            ObjectDumper.Write("__________________");
            ShowCustomerIfTotal(100000);
            ObjectDumper.Write("__________________");
            ShowCustomerIfTotal(50000);
            ObjectDumper.Write("__________________");
            ShowCustomerIfTotal(10000);
        }

        [Category("Basic LINQ Operations")]
        [Title("TaskA02-CustomersAndSuppliers")]
        [Description("All clients, that live with supplier in one city")]
        public void LinqA02()
        {
            var customers =
                from cust in dataSource.Customers
                from supp in dataSource.Suppliers
                where cust.City == supp.City
                select new { cust.CompanyName, supp.SupplierName };
            
            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }

            //second variant with join
            ObjectDumper.Write("________________");
            var customers2 = 
               from cust in dataSource.Customers
               join supp in dataSource.Suppliers on cust.City equals supp.City into custSupGroup
               select new
               {
                   City = cust.City,
                   Supplier = from supGroup in custSupGroup
                              select new { SupplierName = supGroup.SupplierName },
                   Customer = cust.CompanyName
               };

            foreach (var customer in customers2)
            {
                ObjectDumper.Write(customer, 2);
            }
        }

        [Category("Basic LINQ Operations")]
        [Title("TaskA03-CustomersWithOrderMoreThan")]
        [Description("Select all customers that have order more than")]
        public void LinqA03()
        {
            decimal minOrderValue = 8000;
            var customers =
                from cust in dataSource.Customers
                let maxOrder = cust.Orders.Any() ? cust.Orders.Max(o => o.Total) : 0
                select new { cust.CompanyName, maxOrder };

            ObjectDumper.Write($"Customers with order more than {minOrderValue}");
            foreach (var customer in customers)
            {
                if(customer.maxOrder > minOrderValue)
                ObjectDumper.Write(customer);
            }
        }

        [Category("Basic LINQ Operations")]
        [Title("TaskA04-CustomersFirstOrder")]
        [Description("Select first order of customer")]
        public void LinqA04()
        {
            var customers =
                from cust in dataSource.Customers
                where cust.Orders.Any()
                select new {
                    cust.CompanyName,
                    Year = cust.Orders.ElementAt(0).OrderDate.Year,
                    Month = cust.Orders.ElementAt(0).OrderDate.Month
                };

            ObjectDumper.Write($"Select first order of customer");
            foreach (var customer in customers)
            {
                    ObjectDumper.Write(customer);
            }
        }


        [Category("Basic LINQ Operations")]
        [Title("TaskA05-CustomersFirstOrderAndTotal")]
        [Description("Customer first order and total sum of orders")]
        public void LinqA05()
        {
            var customers =
                   from cust in dataSource.Customers
                   where cust.Orders.Any()
                   orderby
                        cust.Orders.ElementAt(0).OrderDate.Year,
                        cust.Orders.ElementAt(0).OrderDate.Month,
                        cust.Orders.Sum(o => o.Total) descending,
                        cust.CompanyName
                   select new
                   {
                       Year = cust.Orders.ElementAt(0).OrderDate.Year,
                       Month = cust.Orders.ElementAt(0).OrderDate.Month,
                       OrdersTotal = cust.Orders.Sum(o => o.Total),
                       CompanyName = cust.CompanyName,
                   };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }



        [Category("Basic LINQ Operations")]
        [Title("TaskA06-CustomersAddressIssues")]
        [Description("Select customers that have non numerical index, or region is null, or phone entered withou operators code")]
        public void LinqA06()
        {
            int tryInteger;
            var customers =
                from cust in dataSource.Customers
                where ((cust.Region == null) || (!int.TryParse(cust.PostalCode, out tryInteger)) || (cust.Phone[0] != '('))
                select new { cust.CompanyName, cust.Region, cust.PostalCode, cust.Phone };
            
            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }


        [Category("Basic LINQ Operations")]
        [Title("TaskA07-GroupProductByCategory")]
        [Description("Group all orders by category, then by stock availability, then by price")]
        public void LinqA07()
        {
            var products =
                from prod in dataSource.Products
                group prod by prod.Category into categoryGroup
                select new
                {
                    Category = categoryGroup.Key,
                    Stock =
                        from catgrp in categoryGroup
                        group catgrp by (catgrp.UnitsInStock != 0) into stockCategGroup
                        select new
                        {
                            Available = stockCategGroup.Key,
                            Products =
                                from stockCatgrp in stockCategGroup
                                orderby stockCatgrp.UnitPrice descending
                                select new { stockCatgrp.UnitPrice, stockCatgrp.ProductName }
                        }
                };

            foreach (var prod in products)
            {
                ObjectDumper.Write(prod, 3);
            }
        }

        [Category("Basic LINQ Operations")]
        [Title("TaskA08-GroupProductByPrice")]
        [Description("Group orders in cheap and expensive categorys")]
        public void LinqA08()
        {
            decimal cheapThreshold = 20;
            decimal expensiveThreshold = 40;

            var products =
                from prod in dataSource.Products
                group prod by (prod.UnitPrice < cheapThreshold ? "Cheap" :
                                prod.UnitPrice > expensiveThreshold ? "Expensive" : "Middle price") into priceGroup
                select new { priceGroup.Key, priceGroup };

            foreach (var prod in products)
            {
                ObjectDumper.Write(prod, 2);
            }
        }


        [Category("Basic LINQ Operations")]
        [Title("TaskA09-Average city profit")]
        [Description("Average profit for every city and average intensity")]
        public void LinqA09()
        {
            var averageProfit =
                from cust in dataSource.Customers
                group cust by cust.City into cityGroup
                select new
                {
                    City = cityGroup.Key,
                    AverageTotal = cityGroup.Average(o => o.Orders.Sum(t => t.Total)),
                    AverageOrders = cityGroup.Average(o => o.Orders.Length),
                };

            foreach (var city in averageProfit)
            {
                ObjectDumper.Write(city);
            }
        }


        [Category("Basic LINQ Operations")]
        [Title("TaskA10-Average year profit")]
        [Description("Average profit for every year")]
        public void LinqA10()
        {
            var customersOrdersByMonth =
                from cust in dataSource.Customers
                    from ordrs in cust.Orders
                    group ordrs by ordrs.OrderDate.Month into monthGroup
                    orderby monthGroup.Key
                    select new
                    {
                        Month = monthGroup.Key,
                        TotalOrders = monthGroup.Count() 
                    };

            foreach (var customer in customersOrdersByMonth)
            {
                ObjectDumper.Write(customer, 2);
            }

            ObjectDumper.Write("_____________________________");
            var customersOrdersByYear =
                from cust in dataSource.Customers
                    from ordrs in cust.Orders
                    group ordrs by ordrs.OrderDate.Year into yearGroup
                    orderby yearGroup.Key
                    select new
                    {
                        Year = yearGroup.Key,
                        TotalOrders = yearGroup.Count()
                    };

            foreach (var customer in customersOrdersByYear)
            {
                ObjectDumper.Write(customer, 2);
            }

            ObjectDumper.Write("_____________________________");
            var customersOrdersByYearAndMonth =
                from cust in dataSource.Customers
                    from ordrs in cust.Orders
                    group ordrs by new {Year = ordrs.OrderDate.Year, Month = ordrs.OrderDate.Month } into dateGroup
                    orderby dateGroup.Key.Year, dateGroup.Key.Month
                    select new
                    {
                        Year = dateGroup.Key.Year,
                        Month = dateGroup.Key.Month,
                        TotalOrders = dateGroup.Count()
                    };


            foreach (var customer in customersOrdersByYearAndMonth)
            {
                ObjectDumper.Write(customer, 2);
            }
        }
    }
}

