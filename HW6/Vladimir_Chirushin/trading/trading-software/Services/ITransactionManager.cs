using System;
using System.Timers;

namespace trading_software
{
    public interface ITransactionManager
    {
        void AddNewTransaction();
        void ReadAllTransactions();
        void MakeRandomTransaction(Object source, ElapsedEventArgs e);

    }
}