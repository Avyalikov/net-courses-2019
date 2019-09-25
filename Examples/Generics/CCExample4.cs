using System;
using System.Collections.Generic;
using System.Text;

namespace Generics
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

    interface IBank<out T> where T : Account
    {
        T DoOperation();
    }

    class Bank : IBank<DepositAccount>
    {
        public DepositAccount DoOperation()
        {
            DepositAccount acc = new DepositAccount();
            acc.DoTransfer();
            return acc;
        }
    }

    public static class Run
    {
        public static void Start()
        {
            IBank<DepositAccount> depositBank = new Bank();
            depositBank.DoOperation();

            IBank<Account> ordinaryBank = depositBank;
            ordinaryBank.DoOperation();

            Console.ReadLine();
        }
    }
}
