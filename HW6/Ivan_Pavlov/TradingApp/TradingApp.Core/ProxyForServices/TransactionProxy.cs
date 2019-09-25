
namespace TradingApp.Core.ProxyForServices
{
    using TradingApp.Core.ServicesInterfaces;
    using TradingApp.Core.Dto;
    using TradingApp.Core.Services;

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
    }
}
