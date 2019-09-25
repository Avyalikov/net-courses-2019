
namespace TradingApp.Core.ProxyForServices
{
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Services;
    using System.Collections.Generic;
    using TradingApp.Core.Models;
    using System;
    using TradingApp.Core.Logger;

    public class TransactionProxy : ITransactionServices
    {
        private readonly TransactionServices transaction;

        public TransactionProxy(TransactionServices transaction)
        {
            this.transaction = transaction;
        }

        public int AddNewTransaction(TransactionStoryInfo args)
        {
            return this.transaction.AddNewTransaction(args);
        }

        public void AddShareInPortfolio(TransactionStoryInfo args)
        {
            try
            {
                this.transaction.AddShareInPortfolio(args);
            }
            catch (ArgumentException ex)
            {
                Logger.Log.Error(ex.Message);
                throw ex;
            }
        }

        public List<TransactionStoryEntity> GetUsersTransaction(int userId)
        {
            return this.transaction.GetUsersTransaction(userId);
        }
    }
}
