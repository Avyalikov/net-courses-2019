using SampleSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task;

namespace SampleQueries
{
    [Title("Linq to Sql Module")]
    [Prefix("Linq")]
    class SQLLinqQueries : SampleHarness
    {
        private NorthwindDBDataClassesDataContext dataSource = new NorthwindDBDataClassesDataContext();

        [Category("HW5 Linq queries")]
        [Title("B1")]
        [Description("Returns names list of customers which orders cost over 25000.")]
        public void Linq1()
        {
            /* A1
             * Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)
             */
            int x = 25000;

            var customers =
                from c in dataSource.Customers
                where (from c2 in dataSource.Customers
                       from o in dataSource.Orders
                       from od in dataSource.Order_Details
                       where c2.CustomerID == o.CustomerID 
                       && o.OrderID == od.OrderID
                       select od).Sum(n => n.UnitPrice * n.Quantity * (1 - (decimal)n.Discount)) > x                
                select c.CompanyName;            

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW5 Linq queries")]
        [Title("B3")]
        [Description("Returns names of clients with any order total over X")]
        public void Linq2()
        {
            /* A3
             * Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X
             */
            int x = 5000;

            var customers =
                from c in dataSource.Customers
                where (from c2 in dataSource.Customers
                       from o in dataSource.Orders
                       from od in dataSource.Order_Details
                       where c2.CustomerID == o.CustomerID
                       && o.OrderID == od.OrderID
                       select od).Any(n => n.UnitPrice * n.Quantity * (1 - (decimal)n.Discount) > x) // Does any order total over X?
                select c.CompanyName;


            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW5 Linq queries")]
        [Title("B6")]
        [Description("Return clients list with incorrect data")]
        public void Linq3()
        {
            /* A6
             * Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
             */            
            var clients =
                (from c in dataSource.Customers
                // let ParsedPostalCode = 0 // this variable create only for int.TryParse method correctly work
                where c.PostalCode == null
                || System.Data.Linq.SqlClient.SqlMethods.Like(c.PostalCode.Trim(), "%[^0-9]%")
                || c.Region == null                
                || !System.Data.Linq.SqlClient.SqlMethods.Like(c.Phone.Trim(), "(%") // check that 1st symbol in Phone is '('
                 select c);

            // ObjectDumper.Write(clients.Count);
            foreach (var client in clients)
            {
                ObjectDumper.Write($"Name: {client.CompanyName}, PostalCode: {client.PostalCode}, Region: {client.Region}, Phone: {client.Phone}");
            }
        }
    }
}
