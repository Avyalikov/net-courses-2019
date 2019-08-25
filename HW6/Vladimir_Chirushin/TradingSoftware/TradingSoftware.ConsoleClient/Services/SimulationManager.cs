namespace TradingSoftware.ConsoleClient.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class SimulationManager : ISimulationManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IClientManager clientManager;
        private readonly IShareManager shareManager;
        private readonly ITransactionManager transactionManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;

        private Random random = new Random();

        public SimulationManager(
            IOutputDevice outputDevice,
            ITableDrawer tableDrawer,
            ITransactionManager transactionManager,
            IClientManager clientManager,
            IShareManager shareManager,
            IBlockOfSharesManager blockOfSharesManager)
        {
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
            this.transactionManager = transactionManager;
            this.clientManager = clientManager;
            this.shareManager = shareManager;
            this.blockOfSharesManager = blockOfSharesManager;
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
                this.outputDevice.WriteLine("Failed to create transaction:");
                IEnumerable<Transaction> query = new[] { transaction }.AsEnumerable<Transaction>();
                this.tableDrawer.Show(query);
                return false;
            }
        }
    }
}