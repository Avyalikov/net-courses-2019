// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

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

        [Category("HW5 Linq queries")]
        [Title("A1")]
        [Description("Returns names list of customers which orders cost over 25000.")]
        public void Linq3()
        {
            /* A1
             * Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) превосходит некоторую величину X. Продемонстрируйте выполнение запроса с различными X (подумайте, можно ли обойтись без копирования запроса несколько раз)
             */
            int x = 25000;

            var customers =
                from c in dataSource.Customers
                where c.Orders.Sum(n => n.Total) > x
                select c.CompanyName;

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A2")]
        [Description("Returns list with suppliers and customers in the same city (and country)")]
        public void Linq4()
        {
            /* A2
             * Для каждого клиента составьте список поставщиков, находящихся в той же стране и том же городе. Сделайте задания с использованием группировки и без.
             */
            var customers =
                from c in dataSource.Customers
                select new
                {
                    Name = c.CompanyName,
                    City = c.City,
                    Suppls = from s in dataSource.Suppliers where s.City == c.City && s.Country == c.Country select s.SupplierName
                };

            foreach (var item in customers)
            {
                ObjectDumper.Write($"City: {item.City}, Customer: {item.Name}");
                foreach (var supl in item.Suppls)
                {
                    ObjectDumper.Write($"Supplier: supl");
                }
                ObjectDumper.Write(String.Empty);
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A3")]
        [Description("Returns names of clients with any order total over X")]
        public void Linq5()
        {
            /* A3
             * Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X
             */
            int x = 5000;

            var customers =
                from c in dataSource.Customers
                where c.Orders.Any(n => n.Total > x) // Does any order total over X?
                select c.CompanyName;

            foreach (var c in customers)
            {
                ObjectDumper.Write(c);
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A4")]
        [Description("Returns clients list with year and month when them joined")]
        public void Linq6()
        {
            /* A4
             * Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами (принять за таковые месяц и год самого первого заказа)
             */            
            // 2nd ver.
            var clients =
                from c in dataSource.Customers
                where c.Orders.Length > 0
                let FirstOrder = c.Orders.Take(1)
                select new
                {
                    Name = c.CompanyName,
                    StartYear = c.Orders.Take(1).FirstOrDefault().OrderDate.Year,
                    StartMonth = c.Orders.Take(1).FirstOrDefault().OrderDate.Month
                };

            foreach (var client in clients)
            {
                ObjectDumper.Write($"Name: {client.Name}, Year: {client.StartYear}, Month: {client.StartMonth}");
            }

            //// 1st ver.
            //var clients =
            //    from c in dataSource.Customers
            //    where c.Orders.Length > 0
            //    select new
            //    {
            //        Name = c.CompanyName,
            //        FirstOrder = c.Orders.Take(1)
            //    };

            //foreach (var client in clients)
            //{
            //    ObjectDumper.Write($"Client: {client.Name}");
            //    foreach (var order in client.FirstOrder)
            //    {
            //        ObjectDumper.Write($"Year: {order.OrderDate.Year} Month: {order.OrderDate.Month}");
            //    }
            //    ObjectDumper.Write(String.Empty);
            //}
        }

        [Category("HW5 Linq queries")]
        [Title("A5")]
        [Description("Returns list sorted by year and month when them joined, Flow and Name")]
        public void Linq7()
        {
            /* A5
             * Сделайте предыдущее задание, но выдайте список отсортированным по году, месяцу, оборотам клиента (от максимального к минимальному) и имени клиента
             */
            var clients =
                from c in dataSource.Customers
                where c.Orders.Length > 0
                let StartYear = c.Orders.Take(1).FirstOrDefault().OrderDate.Year
                let StartMonth = c.Orders.Take(1).FirstOrDefault().OrderDate.Month
                let Flow = c.Orders.Sum(n => n.Total) // Здесь оборот клиента - сумма заказов
                let Name = c.CompanyName
                orderby StartYear, StartMonth, Flow descending, Name
                select new
                {
                    StartYear,
                    StartMonth,
                    Flow,
                    Name
                };
            
            foreach (var client in clients)
            {
                ObjectDumper.Write($"Year: {client.StartYear}, Month: {client.StartMonth}, Flow: {client.Flow}, Name: {client.Name}");
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A6")]
        [Description("Return clients list with incorrect data")]
        public void Linq8()
        {
            /* A6
             * Укажите всех клиентов, у которых указан нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
             */
            var clients =
                from c in dataSource.Customers
                let ParsedPostalCode = 0 // this variable create only for int.TryParse method correctly work
                where c.PostalCode == null 
                || int.TryParse(c.PostalCode.Trim(), out int ParsedPostalCode) == false 
                || c.Region == null
                || !c.Phone.Trim().Take<char>(1).Contains('(') // get 1st symbol from Phone and compare with '('
                select c;

            foreach (var client in clients)
            {
                ObjectDumper.Write($"Name: {client.CompanyName}, PostalCode: {client.PostalCode}, Region: {client.Region}, Phone: {client.Phone}");
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A7")]
        [Description("Returns a list of products grouped by 'Category' then 'UnitsInStock' then 'UnitPrice'")]
        public void Linq9()
        {
            /* A7
             * Сгруппируйте все продукты по категориям, внутри – по наличию на складе, внутри последней группы отсортируйте по стоимости
             */
            // 2nd ver.
            var products =
                from p in dataSource.Products
                group p by p.Category into c
                group c by c.First().UnitsInStock into s
                group s by s.First().First().UnitPrice
                ;

            foreach (var categoryGroup in products)
            {
                foreach (var unitsInStockGroup in categoryGroup)
                {
                    foreach (var unitPriceGroup in unitsInStockGroup)
                    {
                        foreach (var product in unitPriceGroup)
                        {
                            ObjectDumper.Write($"Category: {product.Category}, In stock: {product.UnitsInStock}, Price: {product.UnitPrice}");
                        }
                    }
                }
            }

            //// 1st ver.
            //var products =
            //    from p in dataSource.Products
            //    group p by new { p.Category, p.UnitsInStock, p.UnitPrice} into g
            //    orderby g.Key.Category, g.Key.UnitsInStock, g.Key.UnitPrice
            //    select new { PCategory = g.Key.Category, InStock = g.Key.UnitsInStock, Price = g.Key.UnitPrice}
            //    ;

            //foreach (var product in products)
            //{
            //    ObjectDumper.Write($"Category: {product.PCategory}, In stock: {product.InStock}, Price: {product.Price}");
            //}
        }

        [Category("HW5 Linq queries")]
        [Title("A8")]
        [Description("Returns a list of products grouped by price rank")]
        public void Linq10()
        {
            /* A8
             * Сгруппируйте товары по группам «дешевые», «средняя цена», «дорогие». Границы каждой группы задайте сами
             */
            var products =
                from p in dataSource.Products
                let rank = GetRank(p)
                group p by rank
                ;

            foreach (var group in products)
            {
                ObjectDumper.Write(group.Key);
                foreach (var product in group)
                {
                    ObjectDumper.Write($"   {product.ProductName} — {product.UnitPrice}");
                }
            }
        }

        protected virtual string GetRank(Product p)
        {
            decimal price = p.UnitPrice;

            if (price > 30M)
            {
                return "Expensive";
            }
            else if (price > 15M)
            {
                return "Middle";
            }
            else
            {
                return "Cheap";
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A9")]
        [Description("Returns Average city profitability and Average intensity for each city")]
        public void Linq11()
        {
            /* A9
             * Рассчитайте среднюю прибыльность каждого города (среднюю сумму заказа по всем клиентам из данного города) и среднюю интенсивность (среднее количество заказов, приходящееся на клиента из каждого города)
             */
            var cities =
                (from c in dataSource.Customers
                select new { c.City, c.Country }).Distinct();

            foreach (var city in cities)
            {
                // Number of all clients in this city
                var numberOfClients =
                    from c in dataSource.Customers
                    where c.City == city.City && c.Country == city.Country
                    select new { numberOfOrders = c.Orders.Length, sumOrderPrices = c.Orders.Select(p => p.Total).Sum() };

                // Number of all orders in this city
                var numberOfOrders = numberOfClients.Select(n => n.numberOfOrders).Sum();

                /* Средняя интенсивность (среднее количество заказов, приходящееся на клиента из этого города)
                 * Average intensity = Number of all orders in this city / Number of all clients in this city
                 */
                var averageIntensity = numberOfOrders / numberOfClients.Count();

                /* Средняя прибыльность города (средняя сумма заказа по всем клиентам из данного города)
                 * Average city profitability = Total value of all orders in this city / Number of all customers in this city
                 */
                var averageProfitabilit = numberOfClients.Select(n => n.sumOrderPrices).Sum() / numberOfClients.Count();

                ObjectDumper.Write($"City: {city.City}({city.Country}), Average city profitability: {Math.Round(averageProfitabilit, 2)}, Average intensity: {averageIntensity}");
            }
        }

        [Category("HW5 Linq queries")]
        [Title("A10")]
        [Description("")]
        public void Linq12()
        {
            /* A10
             * Сделайте среднегодовую статистику активности клиентов по месяцам (без учета года), статистику по годам, по годам и месяцам (т.е. когда один месяц в разные годы имеет своё значение)
             */
            Dictionary<int, int> monthsStatistic;
            Dictionary<int, int> yearsStatistic;
            Dictionary<string, int> yearsAndMonthsStatistic;

            var clients = dataSource.Customers.Select(c => 
            new {
                c.CompanyName,
                ordersDates = c.Orders.Select(o => o.OrderDate)
            });

            foreach (var client in clients)
            {
                monthsStatistic = new Dictionary<int, int>();
                yearsStatistic = new Dictionary<int, int>();
                yearsAndMonthsStatistic = new Dictionary<string, int>();

                foreach (var date in client.ordersDates)
                {
                    int month = date.Month;
                    int year = date.Year;

                    if (monthsStatistic.ContainsKey(month))
                    {
                        monthsStatistic[month]++;
                    }
                    else
                    {
                        monthsStatistic.Add(month, 1);
                    }

                    if (yearsStatistic.ContainsKey(year))
                    {
                        yearsStatistic[year]++;
                    }
                    else
                    {
                        yearsStatistic.Add(year, 1);
                    }

                    if (yearsAndMonthsStatistic.ContainsKey($"{year}_{month}"))
                    {
                        yearsAndMonthsStatistic[$"{year}_{month}"]++;
                    }
                    else
                    {
                        yearsAndMonthsStatistic.Add($"{year}_{month}", 1);
                    }
                }

                ObjectDumper.Write($"Client — {client.CompanyName}");
                StringBuilder sb = new StringBuilder("   Months: ");
                foreach (var month in monthsStatistic)
                {
                    sb.Append($"{month.Key} — {month.Value}, ");
                }
                ObjectDumper.Write(sb.ToString());

                sb = new StringBuilder("   Years: ");
                foreach (var year in yearsStatistic)
                {
                    sb.Append($"{year.Key} — {year.Value}, ");
                }
                ObjectDumper.Write(sb.ToString());

                sb = new StringBuilder("   Years & Months: ");
                foreach (var ym in yearsAndMonthsStatistic)
                {
                    sb.Append($"{ym.Key} — {ym.Value}, ");
                }
                ObjectDumper.Write(sb.ToString());
            }
        }
    }
}
