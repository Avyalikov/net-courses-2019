namespace trading_software
{
    using System;
    using System.Collections.Generic;

    public class BlockOfSharesManager : IBlockOfSharesManager
    {
        private readonly IInputDevice inputDevice;
        private readonly IOutputDevice outputDevice;
        private readonly ITableDrawer tableDrawer;
        private readonly IBlockOfSharesRepository blockOfSharesRepository;
        private readonly IClientRepository clientRepository;
        private readonly IStockRepository stockRepository;

        private Random random = new Random();

        public BlockOfSharesManager(
            IInputDevice inputDevice, 
            IOutputDevice outputDevice, 
            ITableDrawer tableDrawer, 
            IBlockOfSharesRepository blockOfSharesRepository,
            IClientRepository clientRepository,
            IStockRepository stockRepository)
        {
            this.inputDevice = inputDevice;
            this.outputDevice = outputDevice;
            this.tableDrawer = tableDrawer;
            this.blockOfSharesRepository = blockOfSharesRepository;
            this.clientRepository = clientRepository;
            this.stockRepository = stockRepository;
        }

        public void AddShare(int clientID, int stockID, int amount)
        {
            var block = new BlockOfShares
            {
                ClientID = clientID,
                StockID = stockID,
                Amount = amount
            };
            this.AddShare(block);
        }

        public void AddShare(BlockOfShares blockOfShares)
        {
            blockOfSharesRepository.Add(blockOfShares);
        }

        public void ManualAddNewShare()
        {
            int stockID;
            while (true)
            {
                outputDevice.WriteLine("Write Stock Type:");
                string stockNameInput = inputDevice.ReadLine();
                stockID = stockRepository.GetStockID(stockNameInput);
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
                clientID = clientRepository.GetClientID(clientNameInput);
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
                {
                    break;
                }    
                else
                    outputDevice.WriteLine("Please enter valid amount.");
            }

            AddShare(clientID, stockID, amount);
        }

        public void CreateRandomShare()
        {
            const int MaxAmountOfShares = 16;
            int numberOfClients = clientRepository.GetNumberOfClients();
            int numberOfStocks = stockRepository.GetNumberOfStocks();
            int ClientId = random.Next(1, numberOfClients);
            int StockId = random.Next(1, numberOfStocks);
            int Amount = random.Next(1, MaxAmountOfShares);
            AddShare(ClientId, StockId, Amount);
        }

        public void ShowAllShares()
        {
            IEnumerable<BlockOfShares> allShares = blockOfSharesRepository.GetAllBlockOfShares();
            tableDrawer.Show(allShares);
        }
    }
}