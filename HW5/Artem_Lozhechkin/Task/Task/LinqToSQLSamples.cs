//-----------------------------------------------------------------------
// <copyright file="LinqToSQLSamples.cs" company="AVLozhechkin">
//     Copyright
// </copyright>
//-----------------------------------------------------------------------
namespace SampleQueries
{
    using System.Data.Linq.SqlClient;
    using System.Linq;
    using SampleSupport;
    using Task;
    [Title("LINQToSql Module")]
    [Prefix("Linq")]
    public class LinqToSQLSamples : SampleHarness
    {
        private NorthwindDBDataContext dataSource = new NorthwindDBDataContext();

        [Category("LinqToSQL queries")]
        [Title("Task 1")]
        [Description(@"Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) 
превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X 
(подумайте, можно ли обойтись без копирования запроса несколько раз)")]
        public void LinqToSQLTask1()
        {
            int x = 20000;

            // int x = 15000;
            // int x = 10000;
            var customers =
                from c in this.dataSource.Customers
                where c.Orders.Sum(price => price.Order_Details.Sum(ord => ord.UnitPrice * ord.Quantity * (decimal)(1 - ord.Discount))) > x
                select c.CompanyName;

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("LinqToSQL queries")]
        [Title("Task 3")]
        [Description(@"Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X")]

        public void LinqToSQLTask3()
        {
            int x = 1000;
            var customers = this.dataSource.Customers
                .Where(c => c.Orders.Any(t => t.Order_Details.Sum(ord => ord.UnitPrice * ord.Quantity * (decimal)(1 - ord.Discount)) > x))
                .Select(c => c.CompanyName).Distinct();

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("LinqToSQL queries")]
        [Title("Task 6")]
        [Description(@"Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен 
регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).")]

        public void LinqToSQLTask6()
        {
            string regex = @"^[\d -]+$";
            var customers = this.dataSource.Customers
                .Where(c => c.PostalCode != null && !SqlMethods.Like(c.PostalCode, regex) | (c.Region == null) | (!c.Phone.StartsWith("(")))
                .Select(c => new { c.CompanyName, c.PostalCode, c.Region, c.Phone });

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }
    }
}
