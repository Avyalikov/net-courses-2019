using StructureMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPatternExample
{
    class Program
    {

        public class Person
        {
            public string Name { get; set; }

            public DateTime Birthday { get; set; }

            public decimal Cash { get; set; }

            public override string ToString()
            {
                return $"{this.Name} has {this.Cash}";
            }
        }


        static IEnumerable<Person> persons = new List<Person>()
        {
             new Person()
        {
            Name = "John",
            Cash = 20
        },

        new Person()
        {
            Name = "Alex",
            Cash = 30
        },

       new Person()
        {
            Name = "Olga",
            Cash = 40
        } };


        public interface IMoneyIncreaseStrategy
        {
            bool CanExecute(Person person);

            void IncreaseMoney(Person person);
        }

        public class IncreaseMoneyForNamesStartsWithJ : IMoneyIncreaseStrategy
        {
            public bool CanExecute(Person person)
            {
                return person.Name.StartsWith("J");
            }

            public void IncreaseMoney(Person person)
            {
                person.Cash *= 1.5m;
            }
        }
         
        public class IncreaseMoneyForNamesContainsL : IMoneyIncreaseStrategy
        {
            public bool CanExecute(Person person)
            {
                return person.Name.ToLowerInvariant().Contains("l");
            }

            public void IncreaseMoney(Person person)
            {
                person.Cash *= 2m;
            }
        }

        public interface ICashManager
        {
            void IncreaseMoney(IEnumerable<Person> persons);
        }

        public class CashMangerLoggerProxy : ICashManager
        {
            private readonly CashManager cashManager;

            public CashMangerLoggerProxy(CashManager cashManager)
            {
                this.cashManager = cashManager;
            }

            public void IncreaseMoney(IEnumerable<Person> persons)
            {
                var sw = new Stopwatch();
                sw.Start();

                Console.WriteLine("Start increasing money");
                this.cashManager.IncreaseMoney(persons);

                sw.Stop();

                Console.WriteLine($"End increasing money. Elapsed time {sw.Elapsed.TotalMilliseconds}");

            }
        }

        public class CashManager : ICashManager
        {
            private readonly IEnumerable<IMoneyIncreaseStrategy> moneyIncreaseStrategies;

            public CashManager(IEnumerable<IMoneyIncreaseStrategy> moneyIncreaseStrategies)
            {
                this.moneyIncreaseStrategies = moneyIncreaseStrategies;
            }

            public void IncreaseMoney(IEnumerable<Person> persons)
            {
                foreach (var person in persons)
                {
                    var strategyToRun = this.moneyIncreaseStrategies.FirstOrDefault(f => f.CanExecute(person));

                    if (strategyToRun == null)
                    {
                        Console.WriteLine($"Can't find strategy for person { person.Name }");
                        continue;
                    }

                    strategyToRun.IncreaseMoney(person);
                }
            }
        }

        public class AppRegistry : Registry
        {
            public AppRegistry()
            {
                this.For<CashManager>().Use<CashManager>();
                this.For<IMoneyIncreaseStrategy>().Add<IncreaseMoneyForNamesStartsWithJ>();
                this.For<IMoneyIncreaseStrategy>().Add<IncreaseMoneyForNamesContainsL>();
            }
        }

        static void Main(string[] args)
        {

            var container = new Container(new AppRegistry());

#if DEBUG

            var cashManager = new CashMangerLoggerProxy(container.GetInstance<CashManager>());

#else
              var cashManager = container.GetInstance<CashManager>();
#endif

            cashManager.IncreaseMoney(persons);

            Array.ForEach(persons.ToArray(), (p) => Console.WriteLine(p));
        }
    }
}
