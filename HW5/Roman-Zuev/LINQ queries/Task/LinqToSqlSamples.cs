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
    [Title("LINQ to Sql Module")]
    [Prefix("Linq")]
    public class LinqToSqlSamples : SampleHarness
    {
        DataClasses2DataContext db = new DataClasses2DataContext();

        [Category("LINQ to Sql")]
        [Title("Task 1")]
        [Description("Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов)" +
            " превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X " +
            "(подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void Linq001()
        {
            decimal x = 30000M;
            var clients =
                from c in db.Customers
                where c.Orders.Sum(p => p.Order_Details.Sum(l =>
                l.UnitPrice * (decimal)(1 - l.Discount) * l.Quantity)) > x
                select c.CompanyName;

            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }

            x = 100;
            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }

            x = 150000;
            Console.WriteLine($"{Environment.NewLine}Customers with total orders > {x} :");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ to Sql")]
        [Title("Task 3")]
        [Description("Найдите всех клиентов, у которых были заказы, " +
            "превосходящие по сумме величину X")]
        public void Linq003()
        {
            decimal x = 3000M;
            var clients =
                from c in db.Customers
                where c.Orders.Any(p => p.Order_Details.Sum(l =>
                l.UnitPrice * (decimal)(1 - l.Discount) * l.Quantity) > x)
                select c.CompanyName;

            Console.WriteLine($"{Environment.NewLine}Customers with any order > {x} :");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }

        [Category("LINQ to Sql")]
        [Title("Task 6")]
        [Description("Укажите всех клиентов, у которых указан нецифровой почтовый код" +
            " или не заполнен регион или в телефоне не указан код оператора " +
            "(для простоты считаем, что это равнозначно «нет круглых скобочек в начале»))")]
        public void Linq006()
        {
            var clients =
                from customer in db.Customers
                where System.Data.Linq.SqlClient.SqlMethods.Like(customer.PostalCode.Trim(), "%[^0-9]%")
                || customer.Region == null
                || customer.Phone[0] != '('
                select customer.CompanyName;

            Console.WriteLine($"{Environment.NewLine}Customers with wrong filled fields: ");
            foreach (var i in clients)
            {
                Console.WriteLine(i);
            }
        }
    }
}
