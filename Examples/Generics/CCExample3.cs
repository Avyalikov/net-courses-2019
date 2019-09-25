using System;
using System.Collections.Generic;
using System.Text;

namespace CCExample3
{
    class Account
    {
        static Random rnd = new Random();

        public void DoTransfer()
        {
            int sum = rnd.Next(10, 120);
            Console.WriteLine($"Do transfer {sum}");
        }

        public string Name { get; set; }
    }

    class DepositAccount : Account
    {

    }

    interface IBank<in T> where T : Account
    {
        void GetStatistic(T account);
    }

    class Bank<T> : IBank<T> where T : Account
    {
        public void GetStatistic(T account)
        {
            Console.WriteLine(account.Name);
        }
    }

    public static class Application
    {
        public static void Start()
        {
            Account account = new Account() { Name = "simple account" };
            IBank<Account> ordinaryBank = new Bank<Account>();
            ordinaryBank.GetStatistic(account);

            DepositAccount depositAcc = new DepositAccount() { Name = "deposit account" };
            IBank<DepositAccount> depositBank = ordinaryBank;
            depositBank.GetStatistic(depositAcc);

            Console.ReadLine();
        }
    }
}
