namespace TradingSoftware.Services
{
    using System;
    using TradingSoftware.ConsoleClient.Devices;
    using TradingSoftware.ConsoleClient.Services;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class UserInteractionManager : IUserInteractionManager
    {
        private readonly IOutputDevice outputDevice;
        private readonly IInputDevice inputDevice;
        private readonly IClientManager clientManager;
        private readonly IShareManager shareManager;
        private readonly ITransactionManager transactionManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;
        private readonly ICommandParser commandParse;

        public UserInteractionManager(
            IOutputDevice outputDevice,
            IInputDevice inputDevice,
            IClientManager clientManager,
            IShareManager shareManager,
            ITransactionManager transactionManager,
            IBlockOfSharesManager blockOfSharesManager,
            ICommandParser commandParse)
        {
            this.outputDevice = outputDevice;
            this.inputDevice = inputDevice;
            this.clientManager = clientManager;
            this.shareManager = shareManager;
            this.transactionManager = transactionManager;
            this.blockOfSharesManager = blockOfSharesManager;
            this.commandParse = commandParse;
        }

        public void ManualAddClient()
        {
            this.outputDevice.WriteLine("Write name:");
            string name = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write PhoneNumber:");
            string phoneNumber = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write Balance:");
            decimal balance = 0;
            while (true)
            {
                if (decimal.TryParse(this.inputDevice.ReadLine(), out balance))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid balance");
                }
            }

            this.clientManager.AddClient(name, phoneNumber, balance);
        }

        public void ManualAddShare()
        {
            this.outputDevice.WriteLine("Write share Type:");
            string shareType = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write share price:");
            decimal sharePrice = 0;
            while (true)
            {
                if (decimal.TryParse(this.inputDevice.ReadLine(), out sharePrice))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid balance");
                }
            }

            this.shareManager.AddShare(shareType, sharePrice);
        }

        public void ManualAddTransaction()
        {
            this.outputDevice.Clear();
            this.commandParse.Parse(Command.ReadAllClients.ToString());
            this.outputDevice.WriteLine("Select seller:");
            string sellerInput = this.inputDevice.ReadLine();

            this.outputDevice.Clear();
            this.commandParse.Parse(Command.ReadAllClients.ToString());
            this.outputDevice.WriteLine("Select buyer:");
            string buyerInput = this.inputDevice.ReadLine();

            this.outputDevice.Clear();
            this.commandParse.Parse(Command.ReadAllShares.ToString());
            this.outputDevice.WriteLine("Select share:");
            string stocksInput = this.inputDevice.ReadLine();

            this.outputDevice.WriteLine("Write share amount:");
            int stockAmount = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out stockAmount))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid balance");
                }
            }

            int shareID = this.shareManager.GetShareID(stocksInput);

            int sellerID = this.clientManager.GetClientID(sellerInput);
            int buyerID = this.clientManager.GetClientID(buyerInput);

            Transaction transaction = new Transaction { dateTime = DateTime.Now, SellerID = sellerID, BuyerID = buyerID, ShareID = shareID, Amount = stockAmount };
            if (this.transactionManager.Validate(transaction))
            {
                this.transactionManager.AddTransaction(transaction);
            }
        }

        public void ManualAddNewBlockOfShare()
        {
            int shareID;
            while (true)
            {
                this.outputDevice.WriteLine("Write Stock Type:");
                string stockNameInput = this.inputDevice.ReadLine();
                shareID = this.shareManager.GetShareID(stockNameInput);
                if (shareID != 0)
                {
                    break;
                }

                this.outputDevice.WriteLine("Please enter valid Stock Type.");
            }

            int clientID;
            while (true)
            {
                this.outputDevice.WriteLine("Write client name:");
                string clientNameInput = this.inputDevice.ReadLine();
                clientID = this.clientManager.GetClientID(clientNameInput);
                if (clientID != 0)
                {
                    break;
                }

                this.outputDevice.WriteLine("Please enter valid Client name.");
            }

            this.outputDevice.WriteLine("Write stocks amount:");
            int amount = 0;
            while (true)
            {
                if (int.TryParse(this.inputDevice.ReadLine(), out amount))
                {
                    break;
                }
                else
                {
                    this.outputDevice.WriteLine("Please enter valid amount.");
                }
            }

            var blockOfShare = new BlockOfShares { ClientID = clientID, ShareID = shareID, Amount = amount };
            this.blockOfSharesManager.AddShare(blockOfShare);
        }
    }
}