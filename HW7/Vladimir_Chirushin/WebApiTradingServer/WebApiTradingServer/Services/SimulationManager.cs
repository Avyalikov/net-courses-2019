namespace WebApiTradingServer.Services
{
    using System;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class SimulationManager
    {
        private readonly IClientManager clientManager;
        private readonly IShareManager shareManager;
        private readonly ITransactionManager transactionManager;

        private Random random = new Random();

        public SimulationManager(
            ITransactionManager transactionManager,
            IClientManager clientManager,
            IShareManager shareManager,
            IBlockOfSharesManager blockOfSharesManager)
        {
            this.transactionManager = transactionManager;
            this.clientManager = clientManager;
            this.shareManager = shareManager;
        }

        public bool MakeRandomTransaction()
        {
            const int StockAmountMax = 15;
            int stockAmount = this.random.Next(1, StockAmountMax);

            Transaction transaction =
                new Transaction
                {
                    dateTime = DateTime.Now,
                    SellerID = this.random.Next(1, this.clientManager.GetNumberOfClients()),
                    BuyerID = this.random.Next(1, this.clientManager.GetNumberOfClients()),
                    ShareID = this.random.Next(1, this.shareManager.GetNumberOfShares()),
                    Amount = stockAmount
                };
            if (this.transactionManager.Validate(transaction))
            {
                this.transactionManager.TransactionAgent(transaction);
                this.transactionManager.AddTransaction(transaction);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}