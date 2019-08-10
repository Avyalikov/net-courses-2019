// Copyright © Microsoft Corporation.  All Rights Reserved. 
// This code released under the terms of the  
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.) 
// 
//Copyright (C) Microsoft Corporation.  All rights reserved. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
//using nwind;
using SampleSupport;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
//using System.Transactions;
using System.Linq.Expressions;
using System.Data.Linq.Provider;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq.SqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace SampleQueries
{
    [Title("LINQ To SQL module")]
    [Prefix("LinqToSql")]
    public class LinqToSqlSamples : SampleHarness
    {

        private readonly static string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\NORTHWND.MDF"));
        private readonly static string sqlServerInstance = @".\SQLEXPRESS";
        private readonly static string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI;Connection Timeout=60";

        //private Northwind db;
        private Task.Northwind_DataContext db = new Task.Northwind_DataContext();

        [Category("HW")]
        [Title("HW - Task 1")]
        [Description("All customers whose orders total is greater than X")]
        public void LinqToSQL1()
        {
            decimal X = 10000;

            //select c.CompanyName, sum(od.Quantity * (od.UnitPrice - od.UnitPrice * od.Discount)) as "Total"
            //from Orders o
            //join [Order Details] od on o.OrderID = od.OrderID
            //join Customers c on o.CustomerID = c.CustomerID
            //group by c.CompanyName
            //having sum(od.Quantity*(od.UnitPrice-od.UnitPrice * od.Discount)) > 10000
            //order by c.CompanyName

            //var result =
            //    from c in
            //        (from o in db.Orders
            //         join od in db.Order_Details on o.OrderID equals od.OrderID
            //         join c in db.Customers on o.CustomerID equals c.CustomerID
            //         select new { c.CompanyName, od.OrderID, total = od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) })
            //    group c by c.CompanyName into g
            //    orderby g.Key
            //    where g.ToList().Select(x => x.total).Sum() > X
            //    select new { g.Key, Total = g.ToList().Select(x => x.total).Sum() };

            //var result =
            //    from c in
            //        (from o in db.Orders
            //         join od in db.Order_Details on o.OrderID equals od.OrderID
            //         join c in db.Customers on o.CustomerID equals c.CustomerID
            //         select new { c.CompanyName, od.OrderID, total = od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) })
            //    group c by c.CompanyName into g
            //    orderby g.Key
            //    where (from x in g.ToList() select x.total).Sum() > X
            //    select new { g.Key, Total = (from x in g.ToList() select x.total).Sum() };

            var result = db.Orders.Join(db.Order_Details, o => new { o.OrderID }, od => new { od.OrderID },
                (o, od) => new { o.CustomerID, od.OrderID, total = od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) }).
                Join(db.Customers, x => x.CustomerID, c => c.CustomerID, (x, c) => new { c.CompanyName, x.total }).GroupBy(gr => gr.CompanyName).
                OrderBy(x=>x.Key).Where(y => y.ToList().Select(x => x.total).Sum() > X).Select(z => new { z.Key, Total = z.ToList().
                Select(x => x.total).Sum() });


            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 3")]
        [Description("All customers who had orders greater than X")]
        public void LinqToSQL3()
        {
            decimal X = 10000;

            //var result =
            //    (from o in db.Orders
            //    join od in db.Order_Details on o.OrderID equals od.OrderID
            //    join c in db.Customers on o.CustomerID equals c.CustomerID
            //    where od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) > X
            //    select new { c.CompanyName, od.OrderID, total = od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) }).Distinct();

            var result = db.Orders.Join(db.Order_Details, o => new { o.OrderID }, od => new { od.OrderID },
                (o, od) => new { o.CustomerID, od.OrderID, total = od.Quantity * (od.UnitPrice - od.UnitPrice * (decimal)od.Discount) }).
                Join(db.Customers, x => x.CustomerID, c => c.CustomerID, (x, c) => new { c.CompanyName, x.total }).
                Where(od=>od.total>X).OrderBy(y=>y.CompanyName).Select(x=> new { x.CompanyName, x.total }).Distinct();

            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW")]
        [Title("HW - Task 6")]
        [Description(".")]
        public void LinqToSQL6()
        {
            var result =
                from c in db.Customers
                where c.PostalCode != null && c.Phone != null
                where c.PostalCode.Contains(@"[\D]") || !c.Phone.Contains(@"^[(]") || c.Region == null
                select new { c.CompanyName, c.PostalCode, c.Phone, c.Region };

            //var result = dataSource.Customers.Select(c => new { c.CompanyName, c.PostalCode, c.Phone, c.Region }).
            //    Where(c => c.PostalCode != null && c.Phone != null &&
            //    (c.PostalCode.Contains(@"[\D]") || !c.Phone.Contains(@"^[(]") || c.Region == null));

            foreach (var c in result)
            {
                ObjectDumper.Write(c);
            }
        }
    }
}