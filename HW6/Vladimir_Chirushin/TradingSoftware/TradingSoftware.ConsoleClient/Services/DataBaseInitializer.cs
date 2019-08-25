namespace TradingSoftware.ConsoleClient.Services
{
    using System;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Services;

    public class DataBaseInitializer : IDataBaseInitializer
    {
        private readonly IClientManager clientManager;
        private readonly IShareManager shareManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;

        private Random random = new Random();

        public DataBaseInitializer(
            IClientManager clientManager, 
            IShareManager shareManager,
            IBlockOfSharesManager blockOfSharesManager)
        {
            this.clientManager = clientManager;
            this.shareManager = shareManager;
            this.blockOfSharesManager = blockOfSharesManager;
        }

        public void Initiate()
        {
            this.clientManager.AddClient(new Client { Name = "Tosin Abasi", PhoneNumber = "555-32-12", Balance = (decimal)45938.12 });
            this.clientManager.AddClient(new Client { Name = "Jennifer Lawrence", PhoneNumber = "333-02-14", Balance = (decimal)43709.14 });
            this.clientManager.AddClient(new Client { Name = "Kilgore Trout", PhoneNumber = "939-12-22", Balance = (decimal)2356079.45 });
            this.clientManager.AddClient(new Client { Name = "Milla Jovovich", PhoneNumber = "555-02-43", Balance = (decimal)57803.39 });
            this.clientManager.AddClient(new Client { Name = "Matt Garstka", PhoneNumber = "493-09-75", Balance = (decimal)9056387.26 });
            this.clientManager.AddClient(new Client { Name = "David Lynch", PhoneNumber = "493-19-35", Balance = (decimal)9368.23 });
            this.clientManager.AddClient(new Client { Name = "Tim Rot", PhoneNumber = "555-05-54", Balance = (decimal)43789.75 });
            this.clientManager.AddClient(new Client { Name = "Tina Kandelaki", PhoneNumber = "796-32-46", Balance = (decimal)89358.93 });
            this.clientManager.AddClient(new Client { Name = "Martin Heidegger", PhoneNumber = "234-42-51", Balance = (decimal)438526.01 });
            this.clientManager.AddClient(new Client { Name = "Michel Foucault", PhoneNumber = "264-56-53", Balance = (decimal)463165.57 });
            this.clientManager.AddClient(new Client { Name = "Ludwig Wittgenstein", PhoneNumber = "546-86-43", Balance = (decimal)5623031.00 });
            this.clientManager.AddClient(new Client { Name = "Bertrand Russell", PhoneNumber = "363-23-49", Balance = (decimal)25378.11 });
            this.clientManager.AddClient(new Client { Name = "Kobo Abe", PhoneNumber = "539-42-53", Balance = (decimal)111078.34 });
            this.clientManager.AddClient(new Client { Name = "Cyrus Smith", PhoneNumber = "536-73-64", Balance = (decimal)173776.02 });
            this.clientManager.AddClient(new Client { Name = "Joseph Fourier", PhoneNumber = "375-45-37", Balance = (decimal)135645.12 });
            this.clientManager.AddClient(new Client { Name = "Friedrich Gauss", PhoneNumber = "315-53-23", Balance = (decimal)524621.11 });
            this.clientManager.AddClient(new Client { Name = "Wilhelm Leibniz", PhoneNumber = "333-45-29", Balance = (decimal)22824.16 });
            this.clientManager.AddClient(new Client { Name = "Pierre de Fermat", PhoneNumber = "749-12-75", Balance = (decimal)111078.37 });
            this.clientManager.AddClient(new Client { Name = "Leonhard Euler", PhoneNumber = "133-35-25", Balance = (decimal)555311.17 });
            this.clientManager.AddClient(new Client { Name = "Nikolai Lobachevsky", PhoneNumber = "866-85-24", Balance = (decimal)99954.16 });
            this.clientManager.AddClient(new Client { Name = "David Hilbert", PhoneNumber = "832-82-76", Balance = (decimal)462878.37 });
            
            this.shareManager.AddShare(new Share { ShareType = "Xilinx", Price = (decimal)104.23 });
            this.shareManager.AddShare(new Share { ShareType = "Texas Instruments", Price = (decimal)120.61 });
            this.shareManager.AddShare(new Share { ShareType = "Boston Scientific Corp", Price = (decimal)43.46 });
            this.shareManager.AddShare(new Share { ShareType = "STMicroelectronics", Price = (decimal)15.68 });
            this.shareManager.AddShare(new Share { ShareType = "NXP Semiconductors", Price = (decimal)99.88 });
            this.shareManager.AddShare(new Share { ShareType = "Strandberg", Price = (decimal)4.10 });
            this.shareManager.AddShare(new Share { ShareType = "Advanced Micro Devices", Price = (decimal)34.19 });
            this.shareManager.AddShare(new Share { ShareType = "National Instruments", Price = (decimal)43.76 });
            this.shareManager.AddShare(new Share { ShareType = "Keysight Technologies", Price = (decimal)12.50 });
            this.shareManager.AddShare(new Share { ShareType = "Intel", Price = (decimal)45.98 });
            this.shareManager.AddShare(new Share { ShareType = "Nvidia", Price = (decimal)154.18 });
            this.shareManager.AddShare(new Share { ShareType = "Gigabyte Technology", Price = (decimal)46.80 });
            this.shareManager.AddShare(new Share { ShareType = "Ikea", Price = (decimal)12.50 });
            this.shareManager.AddShare(new Share { ShareType = "General Electric", Price = (decimal)9.09 });
            this.shareManager.AddShare(new Share { ShareType = "Lockheed Martin", Price = (decimal)377.18 });
            this.shareManager.AddShare(new Share { ShareType = "Siemens", Price = (decimal)49.42 });
            this.shareManager.AddShare(new Share { ShareType = "Mitsubishi Motors", Price = (decimal)4.13 });
            this.shareManager.AddShare(new Share { ShareType = "Fuji Electric", Price = (decimal)199 });
            this.shareManager.AddShare(new Share { ShareType = "Morgan Stanley", Price = (decimal)40.36 });
            this.shareManager.AddShare(new Share { ShareType = "Lotte Shopping", Price = (decimal)0.01 });

            this.GenerateRandomBlockShares();
        }

        public void CreateRandomShare()
        {
            const int MaxAmountOfShares = 10;
            int clientID = this.random.Next(1, this.clientManager.GetNumberOfClients());
            int shareID = this.random.Next(1, this.shareManager.GetNumberOfShares());
            int sharesAmount = this.random.Next(1, MaxAmountOfShares);
            var block = new BlockOfShares
            {
                ClientID = clientID,
                ShareID = shareID,
                Amount = sharesAmount
            };

            if (!this.blockOfSharesManager.IsClientHasStockType(clientID, shareID))
            {
                this.blockOfSharesManager.AddShare(block);
            }

            this.blockOfSharesManager.ChangeShareAmountForClient(block);
        }

        private void GenerateRandomBlockShares()
        {
            int numberOfShares = 200;
            for (int i = 0; i < numberOfShares; i++)
            {
                this.CreateRandomShare();
            }
        }
    }
}