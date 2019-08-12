namespace SampleQueries
{
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using SampleSupport;
    using Task;

    [Title("LINQ To SQL")]
    [Prefix("Linq")]
    public class LinqSQL : SampleHarness
    {
        private NorthwindDataContext dataBase = new NorthwindDataContext();
        
        [Category("Work With SQL")]
        [Title("TaskB01-Customers w/total")]
        [Description("Get all customers that spend more than X on our products")]
        public void LinqB01()
        {
            decimal minTotalSum = 7900;
            var customers =
                from o in this.dataBase.Orders
                join c in this.dataBase.Customers on o.CustomerID equals c.CustomerID into ords
                join od in this.dataBase.Order_Details on o.OrderID equals od.OrderID into custOdet
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
                from o in this.dataBase.Orders
                join c in this.dataBase.Customers on o.CustomerID equals c.CustomerID into ords
                join od in this.dataBase.Order_Details on o.OrderID equals od.OrderID into custOdet
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
                from cust in this.dataBase.Customers
                where (cust.Region == null) || (cust.Phone[0] != '(') || !SqlMethods.Like(cust.PostalCode, "%[^0-9]%")
                select new { cust.CompanyName, cust.Region, cust.PostalCode, cust.Phone };

            foreach (var customer in customers)
            {
                ObjectDumper.Write(customer);
            }
        }
    }
}