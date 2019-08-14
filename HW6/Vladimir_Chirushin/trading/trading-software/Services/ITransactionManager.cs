using System;
using System.Timers;

namespace trading_software
{
    public interface ITransactionManager
    {
        void ManualAddTransaction();
        void AddTransaction(int SellerID, int BuyerID, int StockID, int stockAmount);
        void ReadAllTransactions();
        bool MakeRandomTransaction();

    }
}