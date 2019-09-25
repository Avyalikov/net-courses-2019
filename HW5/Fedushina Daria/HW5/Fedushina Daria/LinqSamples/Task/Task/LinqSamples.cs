// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01////

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq2()
        {
            var products =
                from p in dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        //Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X.
        //Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)

        [Category("Restriction Operators")]
        [Title("HW - Task 1")]
        [Description("This sample return a list of all clients who has orders total sum more than X")]

        public void Linq001()
        {
            Random r = new Random();

            for (int i = 0; i < 5; i++)
            {
                decimal x = r.Next(10000);
                var clients =
                   from p in dataSource.Customers
                   where p.Orders.Sum(o => o.Total) > x
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

        //Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе.
        //Сделайте задания с использованием группировки и без.

        [Category("Restriction Operators")]
        [Title("HW - Task 2")]
        [Description("This sample return a list of all suppliers, living in a same city as client, for each client")]

        public void Linq002()
        {
            foreach (var cust in dataSource.Customers)
            {
                var suppliers =
               from p in dataSource.Suppliers
               where p.Country == cust.Country && p.City == cust.City
               select p;

                ObjectDumper.Write($"{cust.CompanyName}  city:{cust.City}");
                if (suppliers.Count() != 0)
                {
                    ObjectDumper.Write("Suppliers: ");
                    foreach (var p in suppliers)
                    {
                        ObjectDumper.Write(p.SupplierName);
                    }
                }
                else
                {
                    ObjectDumper.Write($"There is no suppliers in {cust.City}");
                }

                ObjectDumper.Write("-------------------------------------------------------------------");

            }
        }

        [Category("Restriction Operators")]
        [Title("HW - Task 2.1")]
        [Description("This sample return a list of cities, where suppliers living in a same city as client, for each client")]

        public void Linq002A()
        {
            var clients =
                 from cust in dataSource.Customers
                 join sup in dataSource.Suppliers on cust.Country equals sup.Country
                 where cust.City == sup.City
                 select new { cust.CompanyName, sup.SupplierName, cust.Country, cust.City };

            var groupingByCities = from cust in clients
                                   group cust by cust.City into customerCity
                                   select new
                                   {
                                       customerCity.Key,
                                       custSuppliers = from cust in customerCity
                                                       select new
                                                       {
                                                           cust.CompanyName,
                                                           cust.SupplierName
                                                       }
                                   };


            foreach (var cust in groupingByCities)
            {
                ObjectDumper.Write($"{cust.Key}");
                foreach (var c in cust.custSuppliers)
                {
                    ObjectDumper.Write($"Client: {c.CompanyName}    Supplier: {c.SupplierName}");
                }
            }
        }

        //Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X

        [Category("Restriction Operators")]
        [Title("HW - Task 3")]
        [Description("This sample return a list of all clients, who has orders costs more than X")]

        public void Linq003()
        {
            var r = new Random();
            decimal x = r.Next(10000);

            var clients =
                from cust in dataSource.Customers
                from ordr in cust.Orders
                where ordr.Total > x
                select new { cust.CompanyName, ordr.OrderID, ordr.Total };


            ObjectDumper.Write($"Customers, who has orders costs more than {x}:");
            ObjectDumper.Write(clients);
        }

        //Выдайте список клиентов с указанием, 
        //начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)

        [Category("Restriction Operators")]
        [Title("HW - Task 4")]
        [Description("This sample return a list of clients with date of first order")]

        public void Linq004()
        {
            var clients =
                from cust in dataSource.Customers
                where cust.Orders.Any()
                select new
                {
                    Name = cust.CompanyName,
                    FirstOrderDate = cust.Orders.OrderBy(o => o.OrderDate).Select(ordr => ordr.OrderDate.ToString("MMMM, yyyy")).First()
                };

            foreach (var cust in clients)
            {
                ObjectDumper.Write($"{cust.Name} {cust.FirstOrderDate}");
            }
            ObjectDumper.Write(" ");
        }

        //Сделайте предыдущее задание, но выдайте список отсортированным по году, 
        //месяцу, оборотам клиента (от максимального к минимальному) и имени клиента

        [Category("Restriction Operators")]
        [Title("HW - Task 5")]
        [Description("This sample return a list of clients with date of first order, ordered by Month, year, Total Orders Sum, Name")]

        public void Linq005()
        {
            var clients =
                  from cust in dataSource.Customers
                  where cust.Orders.Any()
                  select new
                  {
                      Name = cust.CompanyName,
                      FirstOrderDate = cust.Orders.OrderBy(o => o.OrderDate).Select(ordr => ordr.OrderDate).First(),
                      OrdersTotalSum = cust.Orders.Sum(o => o.Total)
                  };
            var orderedClients =
                from client in clients
                orderby client.FirstOrderDate.Year, client.FirstOrderDate.Month, client.OrdersTotalSum descending, client.Name
                select client;
            foreach (var cust in orderedClients)
            {
                ObjectDumper.Write($"{cust.Name}");
                ObjectDumper.Write($"{null,15}First order in: {cust.FirstOrderDate.ToString("MMMM, yyyy")}  Total Sum of orders: {cust.OrdersTotalSum}");
                ObjectDumper.Write(" ");
            }
            ObjectDumper.Write(" ");

        }

        //  Укажите всех клиентов, у которых указан нецифровой почтовый код или 
        //не заполнен регион или в телефоне не указан код оператора(для простоты считаем, 
        //что это равнозначно «нет круглых скобочек в начале

        [Category("Restriction Operators")]
        [Title("HW - Task 6")]
        [Description("This sample return a list of clients who hasn't digital postal code OR hasn't region OR hasn't phone code operator")]

        public void Linq006()
        {
            var clients =
                  from cust in dataSource.Customers
                  where cust.PostalCode != null && cust.Phone != null && (cust.PostalCode.Any(char.IsLetter) || !cust.Phone.Contains("(") || cust.Region == null)
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

        //Сгруппируйте все продукты по категориям, внутри – по наличию на складе,
        //внутри последней группы отсортируйте по стоимости

        [Category("Restriction Operators")]
        [Title("HW - Task 7")]
        [Description("This sample return a list of products grouped by Category and UnitsInStock, and ordered by price")]

        public void Linq007()
        {
            var products =
                  from prod in dataSource.Products
                  group prod by prod.Category into CategoryGroup
                  select new
                  {
                      CategoryGroup.Key,
                      UnitsInStockGroup =
                            from prod in CategoryGroup
                            group prod by prod.UnitsInStock!=0 into UnitCatGroup
                            select new
                            {
                                UnitCatGroup.Key,
                                UnitCatGroupOrdered =
                                        from prod in UnitCatGroup
                                        orderby prod.UnitPrice
                                        select prod
                            }
                  };

            foreach (var prod in products)
            {
                ObjectDumper.Write($"CATEGORY: {prod.Key,0}");
                foreach (var prodCat in prod.UnitsInStockGroup)
                {
                    ObjectDumper.Write($"{null,15}UNITS IN STOCK:  {prodCat.Key,0}");
                    foreach (Product prodCatUnit in prodCat.UnitCatGroupOrdered)
                    {
                        ObjectDumper.Write($"{null,40}PRODUCT: {prodCatUnit.ProductName}   {prodCatUnit.UnitPrice}");
                    }
                }

            }
        }

        //Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами

        [Category("Restriction Operators")]
        [Title("HW - Task 8")]
        [Description("This sample return a list of products grouped by price")]

        public void Linq008()
        {

            var products =
                   from prod in dataSource.Products
                   group prod by prod.UnitPrice.CompareTo(50) into LowPice
                   select new
                   {
                       LowPice.Key,
                       MediumPrice =
                             from prod in LowPice
                             group prod by prod.UnitPrice.CompareTo(100) into MediumPrice
                             select new
                             {
                                 MediumPrice.Key,
                                 BigPrice =
                                         from prod in MediumPrice
                                         select prod
                             }
                   };

            foreach (var prod in products)
            {
                if (prod.Key < 0)
                {
                    ObjectDumper.Write($"Cheap:");
                }
                foreach (var prodCat in prod.MediumPrice)
                {
                    if (prod.Key == 1)
                    {
                        if (prodCat.Key == -1)
                        {
                            ObjectDumper.Write($"Medium price:");
                        }
                        else
                        {
                            ObjectDumper.Write($"Expensive:");
                        }
                    }
                    foreach (Product prodCatUnit in prodCat.BigPrice)
                    {

                        ObjectDumper.Write($"{null,10}PRODUCT: {prodCatUnit.ProductName}   {prodCatUnit.UnitPrice}");
                    }
                }

            }
        }


        //Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) 
        //и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)

        [Category("Restriction Operators")]
        [Title("HW - Task 9")]
        [Description("This sample return a list of cities with average order amount for all customers for each city and average intensity ")]

        public void Linq009()
        {

            var products =
                   from cust in dataSource.Customers
                   group cust by cust.City into City
                   select new
                   {
                       City.Key,
                       averageAmount = City.Average(c => c.Orders.Sum(s => s.Total)),
                       averageIntensity = City.Average(c => c.Orders.Count())
                   };

            foreach (var prod in products)
            {
                ObjectDumper.Write(prod.Key);
                ObjectDumper.Write($"Average order amount: {Math.Round(prod.averageAmount, 2)}");
                ObjectDumper.Write($"Average intensity: {Math.Round(prod.averageIntensity,2)}");
                ObjectDumper.Write(" ");
            }

        }

        //Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года),
        //статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение).

        [Category("Restriction Operators")]
        [Title("HW - Task 10")]
        [Description("This sample return a list  with average clients activity by month, by year , by monthsper year")]

        public void Linq010()
        {
            Dictionary<int, string> Calendar = new Dictionary<int, string>
            {
                {1,"January"},
                {2,"Feburary"},
                {3,"March"},
                {4,"April"},
                {5,"May"},
                {6,"June"},
                {7,"July"},
                {8,"August"},
                {9,"September"},
                {10,"October"},
                {11,"November"},
                {12,"December"}
            };

           
            var monthActivity =
            from cust in dataSource.Customers
            from order in cust.Orders
            orderby order.OrderDate.Month
            group order by order.OrderDate.Month into monthAverage
            select new
            {
                monthAverage.Key,
                averageActivity = monthAverage.Count()/ monthAverage.Select(item=>item.OrderDate.Year).Distinct().Count()
            };
            var yearActivity =
                   from cust in dataSource.Customers
                   from order in cust.Orders
                   orderby order.OrderDate.Year
                   group order by order.OrderDate.Year into YearAverage
                   select new
                   {
                       YearAverage.Key,
                       averageActivity = YearAverage.Count()
                   };
            var yearAndMonthActivity =
                from cust in dataSource.Customers
                from orders in cust.Orders
                group orders by new { Year = orders.OrderDate.Year, Month = orders.OrderDate.Month } into yearAndMonthGroupped
                orderby yearAndMonthGroupped.Key.Year, yearAndMonthGroupped.Key.Month
                select new
                {
                    Year = yearAndMonthGroupped.Key.Year,
                    Month = yearAndMonthGroupped.Key.Month,
                    Total = yearAndMonthGroupped.Count()
                };
           

            ///* Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года)
            ObjectDumper.Write("Average annual statistical customer activity by months (excluding year)");
            ObjectDumper.Write(" ");
            foreach (var prod in monthActivity)
            {
                ObjectDumper.Write($"{Calendar[prod.Key]}  {prod.averageActivity}");
                ObjectDumper.Write(" ");
            }

            ObjectDumper.Write("---------------------------------------------------------------------------------------- ");
            //... статистику по годам,

            ObjectDumper.Write("Average annual statistical customer activity by years");
            ObjectDumper.Write(" ");
            foreach (var prod in yearActivity)
            {
                ObjectDumper.Write($"Year {prod.Key}  {prod.averageActivity}");
                ObjectDumper.Write(" ");

   
            }
            ObjectDumper.Write("---------------------------------------------------------------------------------------- ");
            //...по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение). 
            ObjectDumper.Write("Average annual statistical customer activity by months and years");
            foreach (var month in yearAndMonthActivity)
            {
                ObjectDumper.Write($"{ month.Year}  {null,15}{Calendar[month.Month]}  { month.Total}");
                ObjectDumper.Write(" ");
            }

        }
    }
}