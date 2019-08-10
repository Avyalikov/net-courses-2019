namespace SampleQueries
{
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using SampleSupport;
    using Task;

    [Title("Linq to Sql Module")]
    [Prefix("Linq")]
    public class LinqToSqlSamples : SampleHarness
    {
        private NorthwindDataContext dbSource = new NorthwindDataContext();

        [Category("LINK to SQL samples")]
        [Title("Task 1")]
        [Description("This sample return customers name with total turnover more then X")]
        public void Linq13()
        {
            int[] X = { 10000, 25000, 50000 };

            foreach (var x in X)
            {
                var query = this.dbSource.Customers
                    .Where(c => c.Orders
                        .Sum(o => o.Order_Details
                            .Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount))) > x)
                    .Select(c => c.CompanyName);
                
                ObjectDumper.Write($"Customers with {x} or more total turnover");
                foreach (var q in query)
                {
                    ObjectDumper.Write(q);
                }

                ObjectDumper.Write(string.Empty);
            }
        }

        [Category("LINK to SQL samples")]
        [Title("Task 3")]
        [Description("This sample return customer's name which have order priсe more then X")]
        public void Linq14()
        {
            int X = 10000;

            var query = this.dbSource.Customers
                .Where(c => c.Orders
                    .Any(o => o.Order_Details
                        .Sum(od => od.UnitPrice * od.Quantity * (decimal)(1 - od.Discount)) > X))
                .Select(c => c.CompanyName);

            foreach (var q in query)
            {
                ObjectDumper.Write(q);
            }
        }

        [Category("LINK to SQL samples")]
        [Title("Task 6")]
        [Description("This sample return customer's name with incorrect data")]
        public void Linq15()
        {
            var query = this.dbSource.Customers
                .Where(c => c.PostalCode == null
                    || SqlMethods.Like(c.PostalCode.Trim(), "%[^0-9]%")
                    || c.Region == null
                    || !c.Phone.StartsWith("("))
                .Select(c => new
                {
                    c.CompanyName,
                    c.PostalCode,
                    c.Region,
                    c.Phone
                });

            int i = 1;
            foreach (var q in query)
            {
                ObjectDumper.Write(i.ToString() + q);
                i++;
            }
        }
    }
}