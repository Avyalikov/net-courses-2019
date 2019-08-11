using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using SampleSupport;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Data.Linq.Provider;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq.SqlClient;
using System.Xml.Linq;
using System.Text.RegularExpressions;
//using System.Data.Linq.SqlClient.SqlMethods;

namespace SampleQueries
{
    [Title("Linq to Sql Module")]
    [Prefix("LinqToSql")]
    public class LinqToSqlSamples : SampleHarness
    {
        private Task.DataClasses1DataContext dataSource = new Task.DataClasses1DataContext();

        //Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X.
        //Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)

        [Category("Restriction Operators")]
        [Title("HW - Task 1")]
        [Description("This sample return a list of all clients who has orders total sum more than X")]

        public void LinqToSql001()
        {
            Random r = new Random();

            for (int i = 0; i < 5; i++)
            {
                decimal x = r.Next(10000);
                var clients =
                   from p in dataSource.Customers
                   where p.Orders.Sum(o => o.Order_Details.Sum(c => (c.Quantity * c.UnitPrice - (1 - (decimal)c.Discount)))) > x
                   select p;
                ObjectDumper.Write($"Customers, who has orders total sum more than {x}");
                ObjectDumper.Write(" ");
                foreach (var p in clients)
                {
                    ObjectDumper.Write(p.CompanyName);
                }
                ObjectDumper.Write(" ");
            }
            
        }

        //Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X

        [Category("Restriction Operators")]
        [Title("HW - Task 3")]
        [Description("This sample return a list of all clients, who has orders costs more than X")]

        public void LinqToSql003()
        {
            var r = new Random();
            decimal x = r.Next(10000);

            var clients =
                from cust in dataSource.Customers
                from ordr in cust.Orders
                let Total = ordr.Order_Details.Sum(c => (c.Quantity * c.UnitPrice - (1 - (decimal)c.Discount)))
                where Total > x
                select new { cust.CompanyName, ordr.OrderID, Total };


            ObjectDumper.Write($"Customers, who has orders costs more than {x}:");
            ObjectDumper.Write(clients);
        }

        //  Укажите всех клиентов, у которых указан нецифровой почтовый код или 
        //не заполнен регион или в телефоне не указан код оператора(для простоты считаем, 
        //что это равнозначно «нет круглых скобочек в начале

        [Category("Restriction Operators")]
        [Title("HW - Task 6")]
        [Description("This sample return a list of clients who hasn't digital postal code OR hasn't region OR hasn't phone code operator")]

        public void LinqToSql006()
        {
            var clients =
                  from cust in dataSource.Customers
                  where cust.PostalCode != null && cust.Phone != null && (SqlMethods.Like(cust.PostalCode.Trim(), ("%[^0-9] %")) || !cust.Phone.Contains("(") || cust.Region == null)
                  select cust;
            foreach (var cust in clients)
            {
                ObjectDumper.Write(cust.CompanyName);
                if (cust.PostalCode.Any(char.IsLetter))
                {
                    ObjectDumper.Write($"{null,10} -- PostalCode {cust.PostalCode}");
                }
                if (!cust.Phone.Contains("("))
                {
                    ObjectDumper.Write($"{null,10} -- Phone {cust.Phone}");
                }
                if (cust.Region == null)
                {
                    ObjectDumper.Write($"{null,10} -- Region is NULL");
                }
                ObjectDumper.Write(" ");
            }


        }
    }
}