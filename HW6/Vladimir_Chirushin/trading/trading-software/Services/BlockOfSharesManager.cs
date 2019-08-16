using System;
using System.Collections.Generic;

namespace trading_software
{
    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IDataBaseDevice dataBaseDevice;
        Random random = new Random();
        public BlockOfSharesManager(IInputDevice inputDevice, IOutputDevice outputDevice, ITableDrawer tableDrawer, IDataBaseDevice dataBaseDevice)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
            this.dataBaseDevice = dataBaseDevice;
        }
        public void AddShare(int ClientID, int StockID, int amount)
        {
            var block = new BlockOfShares
            {
                ClientID = ClientID,
                StockID = StockID,
                Amount = amount
            };
            AddShare(block);
        }

        public void AddShare(BlockOfShares blockOfShares)
        {
            dataBaseDevice.Add(blockOfShares);
        }
        public void ManualAddNewShare()
        {
            int stockID;
            while (true)
            {
                outputDevice.WriteLine("Write Stock Type:");
                string stockNameInput = inputDevice.ReadLine();
                stockID = dataBaseDevice.GetStockID(stockNameInput);
                if (stockID != 0)
                {
                    break;
                }
                outputDevice.WriteLine("Please enter valid Stock Type.");

            }
            int clientID;
            while (true)
            {
                outputDevice.WriteLine("Write client name:");
                string clientNameInput = inputDevice.ReadLine();
                clientID = dataBaseDevice.GetClientID(clientNameInput);
                if (clientID != 0)
                {
                    break;
                }
                outputDevice.WriteLine("Please enter valid Client name.");
            }

            outputDevice.WriteLine("Write stocks amount:");
            int amount = 0;
            while (true)
            {
                if (int.TryParse(inputDevice.ReadLine(), out amount))
                    break;
                else
                    outputDevice.WriteLine("Please enter valid amount.");
            }
            AddShare(clientID, stockID, amount);
        }


        public void CreateRandomShare()
        {
            const int maxAmountOfShares = 16;
            int numberOfClients = dataBaseDevice.GetNumberOfClients();
            int numberOfStocks = dataBaseDevice.GetNumberOfStocks();
            int ClientId = random.Next(1, numberOfClients);
            int StockId = random.Next(1, numberOfStocks);
            int Amount = random.Next(1, maxAmountOfShares);
            AddShare(ClientId, StockId, Amount);
        }
        public void ShowAllShares()
        {
            IEnumerable<BlockOfShares> allShares = dataBaseDevice.GetAllBlockOfShares();
            tableDrawer.Show(allShares);
        }
    }
}
