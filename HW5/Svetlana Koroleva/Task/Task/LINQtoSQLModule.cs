
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Configuration;
using System.Data.Common;
using Task;
using System.Text.RegularExpressions;

namespace SampleSupport
{

    [Title("LINQ to SQL Module")]
    [Prefix("Linq")]
    public class LINQtoSQLModule : SampleHarness
    {
        LINQDBDataContext db = new LINQDBDataContext();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample returns customers with a turnover more than given value")]

        public void Linq001()
        {
            decimal value = 10000;

            var customers =
                 from c in db.Customers
                 where (c.Orders.Sum(o => o.Order_Details.Sum(t => (t.Quantity * t.UnitPrice * (decimal)(1 - t.Discount)))) > value)
                 select new { c.CompanyName, Turnover = c.Orders.Sum(o => o.Order_Details.Sum(od => (od.Quantity * od.UnitPrice * (decimal)(1 - od.Discount)))) };



            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }

        }

        [Category("Restriction Operators")]
        [Title("Where - Task 3")]
        [Description("This sample returns customers with orders totals more than given value")]

        public void Linq003()
        {
            decimal value = 10000;

            var customers =
                 from c in db.Customers
                 where (c.Orders.Any(o => o.Order_Details.Sum(t => (t.Quantity * t.UnitPrice * (decimal)(1 - t.Discount))) > value))
                 select new { c.CompanyName };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }



        }


        [Category("Restriction Operators")]
        [Title("Where - Task 6")]
        [Description("This sample return return customers validated on postalcode, region, phone")]

        public void Linq006()
        {
           
            var customers =
                 from c in db.Customers
                 where 
                 
                 System.Data.Linq.SqlClient.SqlMethods.Like(c.PostalCode.Trim(), "%[^0-9]")
                 || c.Region==null
                 || !c.Phone.StartsWith("(")


                 select new { c.CompanyName, c.PostalCode, c.Region, c.Phone };

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

     
    }
}
