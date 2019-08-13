using System;
using System.Timers;

namespace trading_software
{
    public interface ITransactionManager
    {
        void AddNewTransaction();
        void AddTransaction(Client sellerClient, Client buyerClient, Stock stock, int stockAmount);
        void ReadAllTransactions();
        bool MakeRandomTransaction();

    }
}