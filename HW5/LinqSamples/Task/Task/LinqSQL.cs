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
            decimal threshold = 1500;
            var customers = dataBase.ExecuteQuery<Customers>(@"
                SELECT C.ContactName, CAST(O.Total AS DECIMAL(10,4)) AS Total 
                FROM dbo.Customers C 
	                INNER JOIN 
	                (
		                SELECT
			                O.CustomerID,
			                SUM(OD.UnitPrice * OD.quantity * (1 - OD.Discount)) Total
		                FROM
			                dbo.[Order Details] OD 
				                INNER JOIN dbo.Orders O ON OD.OrderID = O.OrderID
		                GROUP BY
			                O.CustomerID
	                ) O ON C.CustomerID = O.CustomerID");

            ObjectDumper.Write($"Customers with Total more than {threshold}");
            foreach (Customers customer in customers)
            {
                if (customer.Total > threshold)
                    ObjectDumper.Write(customer);
            }
        }


        [Category("Work With SQL")]
        [Title("TaskB03-Customers w/products")]
        [Description("Get all customers that bought produt with price more than X")]
        public void LinqB03()
        {
            
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

