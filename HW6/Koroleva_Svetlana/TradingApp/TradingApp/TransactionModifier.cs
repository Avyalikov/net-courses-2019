using TradingApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingApp.Model;
using TradingApp.DAL;



namespace TradingApp
{
    class TransactionModifier : ITransactionModifier
    {
        ExchangeContext db;
       

        public TransactionModifier(ExchangeContext db)
        {
            this.db = db;
           
        }
        public void CommitTransaction(Order custOrder, Order salerOrder, DateTime dateTime)
        {
         TransactionHistory transaction = new TransactionHistory { CustomerOrderID=custOrder.OrderID, SalerOrderID = salerOrder.OrderID, DateTime=dateTime};
         db.transactionHistories.Add(transaction);
         db.SaveChanges();
        }

        public int GetSalerOrderID(TransactionHistory transaction)
        {
           return transaction.SalerOrderID;
             
        }
    }
}
