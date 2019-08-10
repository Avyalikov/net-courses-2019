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
    [Title("Linq to Sql Module")]
	[Prefix("LinqToSql")]
	public class LinqToSqlSamples : SampleHarness
	{
        private DataClasses2DataContext db = new DataClasses2DataContext();

        [Category("LINQtoSql")]
        [Title("Task 1")]
        [Description("Выдайте список всех клиентов, чей суммарный оборот " +
            "(сумма всех заказов) превосходит некоторую величину X. " +
            "Продемонстрируйте выполнение запроса с различными X " +
            "(подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void Linq001()
        {
            decimal x = 30000;

            var customers =
                from c in db.Customers
                from o in db.Orders
                from od in db.Order_Details
                where od.UnitPrice * (decimal)(1 - od.Discount) * (decimal)od.Quantity > x
                select c.CompanyName;


            
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

        //[Category("LINQ Queries")]
        //[Title("Task 3")]
        //[Description("Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]
        //public void Linq003()
        //{
        //    decimal x = 1000;

        //    var clients =
        //    from customer in dataSource.Customers
        //    where customer.Orders.Any(o => o.Total > x)
        //    select customer.CompanyName;

        //    Console.WriteLine($"{Environment.NewLine}Customers that have orders with price more than {x}: ");
        //    foreach (var i in clients)
        //    {
        //        Console.WriteLine(i);
        //    }

        //    x = 10000;
        //    Console.WriteLine($"{Environment.NewLine}Customers that have orders with price more than {x}: ");
        //    foreach (var i in clients)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}

        //[Category("LINQ Queries")]
        //[Title("Task 6")]
        //[Description("Укажите всех клиентов, у которых указан нецифровой почтовый код" +
        //    " или не заполнен регион или в телефоне не указан код оператора " +
        //    "(для простоты считаем, что это равнозначно «нет круглых скобочек в начале»))")]
        //public void Linq006()
        //{
        //    bool IsDigitsOnly(string str)
        //    {
        //        if (String.IsNullOrEmpty(str))
        //        {
        //            return false;
        //        }

        //        foreach (char c in str)
        //        {
        //            if (c < '0' || c > '9')
        //                return false;
        //        }

        //        return true;
        //    }

        //    var clients =
        //        from customer in dataSource.Customers
        //        where !IsDigitsOnly(customer.PostalCode)
        //        || String.IsNullOrEmpty(customer.Region)
        //        || customer.Phone[0] != '('
        //        select customer.CompanyName;

        //    Console.WriteLine($"{Environment.NewLine}Customers with wrong filled fields: ");
        //    foreach (var i in clients)
        //    {
        //        Console.WriteLine(i);
        //    }
        //}
    }
}
