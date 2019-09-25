using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Data.Linq.Mapping;
using System.Configuration;
using System.Data.Common;
using Task;
using System.Text.RegularExpressions;
using SampleSupport;

namespace SampleQueries
{

    [Title("LINQ to SQL Module")]
    [Prefix("Linq")]
    public class LINQtoSQL : SampleHarness
    {
        NorthwindDataClass dataSource = new NorthwindDataClass();

        [Category("Homework")]
        [Title("HW - Task 1")]
        [Description("Shows customers whose orders total is more than X")]

        public void Linq001()
        {
            int[] xValue = { 10000, 50000 };
            foreach (int x in xValue)
            {
                var customers = dataSource.Customers.
                Where(a => a.Orders.Sum(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount)))) > x).
                Select(c => new
                {
                    CustomerID = c.CustomerID,
                    Name = c.CompanyName,
                    Total = c.Orders.Sum(y => y.Order_Details.Sum(z => (z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount))))
                });

                ObjectDumper.Write($"Orders total more than: {x}");
                foreach (var c in customers)
                {
                    ObjectDumper.Write(c);
                }
            }
                
        }

        [Category("Homework")]
        [Title("HW - Task 3")]
        [Description("Shows orders more than X")]

        public void Linq003()
        {
            int X = 7000;

            var customers = dataSource.Customers.Where(c => c.Orders.
            Any(o => o.Order_Details.Sum(z => z.UnitPrice * z.Quantity* (decimal)(1 - z.Discount)) > X)).
                Select(c => new
                {
                    c.CompanyName,
                    MaxOrder = c.Orders.
                Max(o => o.Order_Details.Sum(z => z.UnitPrice * z.Quantity * (decimal)(1 - z.Discount)))
                });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }


        [Category("Homework")]
        [Title("HW - Task 6")]
        [Description("Shows customers without region, or with non numerical index, or wrong phone number")]

        public void Linq006()
        {

            var customers = dataSource.Customers.Where(c => c.Region == null ||
            (c.PostalCode.Contains("%[0-9]%")) || !c.Phone.StartsWith("(")).
            Select(c => new
            {
                customer = c.CompanyName,
                region = c.Region,
                postalCode = c.PostalCode,
                phone =c.Phone });

            foreach (var c in customers)
            {
                ObjectDumper.Write($"Name: {c.customer} | Region: {c.region} | PostalCode: {c.postalCode} | Phone: {c.phone}");
            }
        }
    }
}