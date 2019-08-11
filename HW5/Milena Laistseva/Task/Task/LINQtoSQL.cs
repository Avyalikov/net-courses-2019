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

        public void LinqA01()
        {
            var customers = dataSource.Customers.
                Select(x => new { x.CustomerID, x.CompanyName, Total = x.Orders.Sum(y => y.Order_Details.Sum(z => z.UnitPrice * (decimal)(1 - z.Discount))) });
                

            void CustomerTotalMoreThanX(decimal X)
            {
                foreach (var customer in customers)
                {
                    if (customer.Total > X)
                    {
                        ObjectDumper.Write(customer);
                    }
                }
            }

            CustomerTotalMoreThanX(10000);
            ObjectDumper.Write("____________________________");
            CustomerTotalMoreThanX(50000);

        }

        [Category("Homework")]
        [Title("HW - Task 3")]
        [Description("Shows orders more than X")]

        public void Linq003()
        {
            int X = 7000;

            var customers = dataSource.Customers.Where(c => c.Orders.
            Any(o => o.Order_Details.Sum(z => z.UnitPrice * (decimal)(1 - z.Discount)) > X)).
                Select(c => new
                {
                    c.CompanyName,
                    MaxOrder = c.Orders.
                Max(o => o.Order_Details.Sum(z => z.UnitPrice * (decimal)(1 - z.Discount)) > X)
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
            !SqlMethods.Like(c.PostalCode, "[^0-9}%") || (c.Phone.First() != '(')).
            Select(c => new { c.CompanyName, c.Region, c.PostalCode, c.Phone });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

    }
}