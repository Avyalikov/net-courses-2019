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

        [Category("LINQ Queries")]
        [Title("Task 1")]
        [Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов)" +
            " превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X " +
            "(подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void Linq001()
        {
            decimal x = 30000;

            var customers = dataSource.Customers.Where(c => c.Orders.Sum(o => o.Total) > x)
                .Select(c => c.CompanyName);

            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in customers)
            {
                Console.WriteLine(i);
            }

            x = 100;
            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in customers)
            {
                Console.WriteLine(i);
            }

            x = 150000;
            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in customers)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 2")]
        [Description("Для каждого клиента составьте список поставщиков, находящихся в той же стране" +
            " и том же городе. Сделайте задания с использованием группировки и без.")]
        public void Linq002()
        {
            var clientsWithGroup =
                from customer in dataSource.Customers
                join suppliers in dataSource.Suppliers on customer.City equals suppliers.City
                group new { customer.CompanyName, suppliers.SupplierName } by suppliers.City into Groupped
                select Groupped;

            Console.WriteLine($"{Environment.NewLine}Customers and Suppliers in the same city: ");
            foreach (var i in clientsWithGroup)
            {
                Console.WriteLine($"City: {i.Key}");
                foreach (var j in i)
                {
                    Console.WriteLine($"Customer: {j.CompanyName}, Suppliers: {j.SupplierName}");
                }
            }

            var clients =
            from customer in dataSource.Customers
            join sup in dataSource.Suppliers on customer.City equals sup.City into supGroup
            from sup2 in supGroup
            select new { Customer = customer.CompanyName, customer.City, customer.Country, Suppliers = supGroup };

            Console.WriteLine($"{Environment.NewLine}Customers and Suppliers in the same city: ");
            foreach (var i in clients)
            {
                Console.WriteLine($"{Environment.NewLine}{i.Customer}'s Suppliers in {i.City}, {i.Country}: ");
                foreach (var j in i.Suppliers)
                {
                    Console.WriteLine($" {j.SupplierName}");
                }
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 3")]
        [Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]
        public void Linq003()
        {
            decimal x = 1000;

            var clients =
            from customer in dataSource.Customers
            where customer.Orders.Any(o => o.Total > x)
            select customer.CompanyName;

            Console.WriteLine($"{Environment.NewLine}Customers that have orders with price more than {x}: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }

            x = 10000;
            Console.WriteLine($"{Environment.NewLine}Customers that have orders with price more than {x}: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 4")]
        [Description("Выдайте список клиентов с указанием, начиная с какого месяца " +
            "какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)")]
        public void Linq004()
        {
            var clients =
                from customer in dataSource.Customers
                where customer.Orders.Length >= 1
                select new { customer.CompanyName, FirstOrder = customer.Orders.Min(o => o.OrderDate).ToShortDateString() };

            Console.WriteLine($"{Environment.NewLine}Customers and theirs first orders: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 5")]
        [Description("Сделайте предыдущее задание, но выдайте список отсортированным" +
            " по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента)")]
        public void Linq005()
        {
            var clients =
                from customer in dataSource.Customers
                let orders = customer.Orders
                where orders.Length >= 1
                orderby orders.Min(o => o.OrderDate).Year, orders.Min(o => o.OrderDate).Month,
                orders.Sum(s => s.Total) descending, customer.CompanyName
                select new { customer.CompanyName, FirstOrder = customer.Orders.Min(o => o.OrderDate).ToShortDateString(), Total = orders.Sum(s => s.Total) };

            Console.WriteLine($"{Environment.NewLine}Customers and theirs first orders sorted: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код" +
            " или не заполнен регион или в телефоне не указан код оператора " +
            "(для простоты считаем, что это равнозначно «нет круглых скобочек в начале»))")]
        public void Linq006()
        {
            bool IsDigitsOnly(string str)
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }

                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                        return false;
                }

                return true;
            }

            var clients =
                from customer in dataSource.Customers
                where !IsDigitsOnly(customer.PostalCode)
                || String.IsNullOrEmpty(customer.Region)
                || customer.Phone[0] != '('
                select customer.CompanyName;

            Console.WriteLine($"{Environment.NewLine}Customers with wrong filled fields: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 7")]
        [Description("Сгруппируйте все продукты по категориям, внутри – по наличию на складе," +
            " внутри последней группы отсортируйте по стоимости»))")]
        public void Linq007()
        {
            var groupedproducts =
                from products in dataSource.Products
                group products by products.Category into productsCategoried
                select new
                {
                    Category = productsCategoried.Key,
                    Products = from p in productsCategoried
                               group p by p.UnitsInStock > 0 into innerGroup
                               select new
                               {
                                   AreInstock = innerGroup.Key,
                                   products = from o in innerGroup
                                              orderby o.UnitPrice
                                              select o.ProductName
                               }

                };

            Console.WriteLine($"{Environment.NewLine}Products: ");
            foreach (var i in groupedproducts)
            {
                Console.WriteLine($"{Environment.NewLine}Products category: {i.Category}");
                foreach (var j in i.Products)
                {
                    Console.WriteLine(j.AreInstock ? $"{Environment.NewLine}Products in stock:" :
                        $"{Environment.NewLine}Products out of stock:");
                    foreach (var k in j.products)
                    {
                        Console.WriteLine(k);
                    }
                }
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 8")]
        [Description("Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». " +
            "Границы каждой группы задайте сами)")]
        public void Linq008()
        {
            decimal x = 10M, y = 20M;
            var products = dataSource.Products
                .GroupBy(p => p.UnitPrice <= x ? "Cheap" : p.UnitPrice <= y ? "Average" : "Expensive")
                .Select(g => new { g.Key, g });

            Console.WriteLine($"{Environment.NewLine}Products' groups: ");
            foreach (var i in products)
            {
                Console.WriteLine($"{Environment.NewLine}{i.Key} :");
                foreach (var j in i.g)
                {
                    Console.WriteLine(j.ProductName);
                }
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 9")]
        [Description("Рассчитайте среднюю прибыльность каждого города " +
            "(среднюю сумму заказа по всем клиентам из данного города) " +
            "и среднюю интенсивность (среднее количество заказов, " +
            "приходящееся на клиента из каждого города)")]
        public void Linq009()
        {
            var averages =
                from customers in dataSource.Customers
                where customers.Orders.Length > 0
                orderby customers.City
                group
                     from o in customers.Orders
                     select o
                by customers.City into ordersGroup
                select new
                {
                    City = ordersGroup.Key,
                    AverageIncome = from i in ordersGroup
                                    select i.Average(j => j.Total),
                    AverageIntencity = from k in ordersGroup
                                       select k.Count()
                };

            Console.WriteLine($"{Environment.NewLine}Cities' average indicators: ");
            foreach (var i in averages)
            {
                Console.WriteLine($"{Environment.NewLine}{i.City} :");
                Console.WriteLine($"{Environment.NewLine}Average income: {i.AverageIncome.First()}");
                Console.WriteLine($"Average intencity: {i.AverageIntencity.First()}");
            }
        }

        [Category("LINQ Queries")]
        [Title("Task 10")]
        [Description("Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года)," +
            " статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).")]
        public void Linq010()
        {
            var monthActivity =
                from customers in dataSource.Customers
                where customers.Orders.Length > 0
                from orders in customers.Orders
                orderby orders.OrderDate.Month
                group orders.OrderID by orders.OrderDate.Month into MonthActivity
                select new
                {
                    Month = MonthActivity.Key,
                    Activity = MonthActivity.Count()
                };
            var yearActivity =
                from customers in dataSource.Customers
                where customers.Orders.Length > 0
                from orders in customers.Orders
                orderby orders.OrderDate.Year
                group orders.OrderID by orders.OrderDate.Year into YearActivity
                select new
                {
                    Year = YearActivity.Key,
                    Activity = YearActivity.Count()
                };
            var yearMonthActivity =
                from customers in dataSource.Customers
                where customers.Orders.Length > 0
                from orders in customers.Orders
                orderby orders.OrderDate.Date
                group orders.OrderID by new { year = orders.OrderDate.Year, month = orders.OrderDate.Month }
                into YearMonthActivity
                select new
                {
                    YearMonth = YearMonthActivity.Key,
                    Activity = YearMonthActivity.Count()
                };
            System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();

            Console.WriteLine($"{Environment.NewLine}Month activity: ");
            foreach (var i in monthActivity)
            {
                Console.WriteLine($"{Environment.NewLine}{mfi.GetMonthName(i.Month)}");
                Console.WriteLine(i.Activity);
            }

            Console.WriteLine($"{Environment.NewLine}Year activity: ");
            foreach (var i in yearActivity)
            {
                Console.WriteLine($"{Environment.NewLine}{i.Year}");
                Console.WriteLine(i.Activity);
            }

            Console.WriteLine($"{Environment.NewLine}Year and month activity: ");
            foreach (var i in yearMonthActivity)
            {
                Console.WriteLine($"{Environment.NewLine}{i.YearMonth.year}, {mfi.GetMonthName(i.YearMonth.month)}");
                Console.WriteLine(i.Activity);
            }
        }
    }
}
