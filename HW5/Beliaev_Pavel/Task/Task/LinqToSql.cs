using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SampleSupport;
using Task.Data;

namespace Task
{
    [Title("LINQ to SQL Module")]
    [Prefix("Linq")]
    class LinqToSql : SampleHarness
    {
        private NorthwindDbDataContext dataSource = new NorthwindDbDataContext();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample returns customers with a turnover more than given value")]
        public void Linq1()
        {
            decimal x = 10000;
            var cust = dataSource.Customers.
                Where(c => c.Orders.Sum(t => t.Order_Details.Sum(s=>s.Quantity*s.UnitPrice*(1-(decimal)s.Discount))) > x).
                Select(c => c);            

            foreach (var c in cust)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("This sample returns customers with orders totals more than given value")]
        public void Linq3()
        {
            decimal x = 10000;
            var cust = dataSource.Customers.
                Where(c => c.Orders.Any(t => t.Order_Details.Sum(a=>a.Quantity * a.UnitPrice * (1- (decimal)a.Discount)) > x)).
                Select(c => c);

            foreach (var c in cust)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return return customers validated on postalcode, region, phone")]
        public void Linq6()
        {
            var cust = dataSource.Customers.
                Where(w => w.PostalCode == null ? false : 
                w.PostalCode.Contains("A") || w.PostalCode.Contains("B") || w.PostalCode.Contains("C") 
                || w.PostalCode.Contains("D") || w.PostalCode.Contains("E") || w.PostalCode.Contains("F") 
                || w.PostalCode.Contains("G") || w.PostalCode.Contains("H") || w.PostalCode.Contains("I") 
                || w.PostalCode.Contains("J") || w.PostalCode.Contains("K") || w.PostalCode.Contains("L") 
                || w.PostalCode.Contains("M") || w.PostalCode.Contains("N") || w.PostalCode.Contains("O") 
                || w.PostalCode.Contains("P") || w.PostalCode.Contains("Q") || w.PostalCode.Contains("R") 
                || w.PostalCode.Contains("S") || w.PostalCode.Contains("T") || w.PostalCode.Contains("U") 
                || w.PostalCode.Contains("V") || w.PostalCode.Contains("W") || w.PostalCode.Contains("X") 
                || w.PostalCode.Contains("Y") || w.PostalCode.Contains("Z")
                || w.Region == null 
                || !w.Phone.Contains("(")).
                Select(c => new { c.CompanyName, c.PostalCode, c.Region, c.Phone });


            foreach (var c in cust)
            {
                ObjectDumper.Write(c, 2);
            }
        }
    }
}
