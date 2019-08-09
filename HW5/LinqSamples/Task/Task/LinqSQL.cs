// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Data.Linq.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ To SQL")]
    [Prefix("Linq")]
    public class LinqSQL : SampleHarness
    {
        private NorthwindDataContext dataBase = new NorthwindDataContext();

        public class Customers
        {
            public string ContactName;
            public decimal Total;
        }

        [Category("Work With SQL")]
        [Title("TaskB01-Customers w/total")]
        [Description("Get all customers that spend more than X on our products")]
        public void LinqB01()
        {
            decimal minTotalSum = 7900;
            var customers =
                from o in dataBase.Orders
                join c in dataBase.Customers on o.CustomerID equals c.CustomerID into ords
                join od in dataBase.Order_Details on o.OrderID equals od.OrderID into custOdet
                let orderSumTotal = o.Order_Details.Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))
                where orderSumTotal > minTotalSum
                orderby orderSumTotal descending
                select new { orderSumTotal, o.Customer.CompanyName };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }


        [Category("Work With SQL")]
        [Title("TaskB03-Customers w/products")]
        [Description("Get all customers that made order with price more than X")]
        public void LinqB03()
        {
            decimal orderMinValue = 7900;
            var customers =
                from o in dataBase.Orders
                join c in dataBase.Customers on o.CustomerID equals c.CustomerID into ords
                join od in dataBase.Order_Details on o.OrderID equals od.OrderID into custOdet
                let orderMaxValue = o.Order_Details.Max(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))
                where orderMaxValue > orderMinValue
                orderby orderMaxValue descending
                select new { orderMaxValue, o.Customer.CompanyName };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }

        }

        [Category("Work With SQL")]
        [Title("TaskB06-Customers w/address issues")]
        [Description("Get all customers that have address issues")]
        public void LinqB06()
        {
            var customers =
                from cust in dataBase.Customers
                where (cust.Region == null) || (cust.Phone[0] != '(') || !SqlMethods.Like(cust.PostalCode, "%[^0-9]%")
                select new { cust.CompanyName, cust.Region, cust.PostalCode, cust.Phone };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }
    }
}

