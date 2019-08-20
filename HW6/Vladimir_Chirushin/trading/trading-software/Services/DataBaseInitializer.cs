namespace trading_software
{
    public class DataBaseInitializer : IDataBaseInitializer
    {
        private readonly IClientManager clientManager;
        private readonly IStockManager stockManager;
        private readonly IBlockOfSharesManager blockOfSharesManager;

        public DataBaseInitializer(
            IClientManager clientManager, 
            IStockManager stockManager,
            IBlockOfSharesManager blockOfSharesManager)
        {
            this.clientManager = clientManager;
            this.stockManager = stockManager;
            this.blockOfSharesManager = blockOfSharesManager;
        }
        public void Initiate()
        {
            clientManager.AddClient(new Client { Name = "Tosin Abasi", PhoneNumber = "555-32-12", Balance = (decimal) 45938.12 });
            clientManager.AddClient(new Client { Name = "Jennifer Lawrence", PhoneNumber = "333-02-14", Balance = (decimal)43709.14 });
            clientManager.AddClient(new Client { Name = "Kilgore Trout", PhoneNumber = "939-12-22", Balance = (decimal)2356079.45 });
            clientManager.AddClient(new Client { Name = "Milla Jovovich", PhoneNumber = "555-02-43", Balance = (decimal)57803.39 });
            clientManager.AddClient(new Client { Name = "Matt Garstka", PhoneNumber = "493-09-75", Balance = (decimal)9056387.26 });
            clientManager.AddClient(new Client { Name = "David Lynch", PhoneNumber = "493-19-35", Balance = (decimal)9368.23 });
            clientManager.AddClient(new Client { Name = "Tim Rot", PhoneNumber = "555-05-54", Balance = (decimal)43789.75 });
            clientManager.AddClient(new Client { Name = "Tina Kandelaki", PhoneNumber = "796-32-46", Balance = (decimal)89358.93 });
            clientManager.AddClient(new Client { Name = "Martin Heidegger", PhoneNumber = "234-42-51", Balance = (decimal)438526.01 });
            clientManager.AddClient(new Client { Name = "Michel Foucault", PhoneNumber = "264-56-53", Balance = (decimal)463165.57 });
            clientManager.AddClient(new Client { Name = "Ludwig Wittgenstein", PhoneNumber = "546-86-43", Balance = (decimal)5623031.00 });
            clientManager.AddClient(new Client { Name = "Bertrand Russell", PhoneNumber = "363-23-49", Balance = (decimal)25378.11 });
            clientManager.AddClient(new Client { Name = "Kobo Abe", PhoneNumber = "539-42-53", Balance = (decimal)111078.34 });
            clientManager.AddClient(new Client { Name = "Cyrus Smith", PhoneNumber = "536-73-64", Balance = (decimal)173776.02 });
            clientManager.AddClient(new Client { Name = "Joseph Fourier", PhoneNumber = "375-45-37", Balance = (decimal)135645.12 });
            clientManager.AddClient(new Client { Name = "Friedrich Gauss", PhoneNumber = "315-53-23", Balance = (decimal)524621.11 });
            clientManager.AddClient(new Client { Name = "Wilhelm Leibniz", PhoneNumber = "333-45-29", Balance = (decimal)22824.16 });
            clientManager.AddClient(new Client { Name = "Pierre de Fermat", PhoneNumber = "749-12-75", Balance = (decimal)111078.37 });
            clientManager.AddClient(new Client { Name = "Leonhard Euler", PhoneNumber = "133-35-25", Balance = (decimal)555311.17 });
            clientManager.AddClient(new Client { Name = "Nikolai Lobachevsky", PhoneNumber = "866-85-24", Balance = (decimal)99954.16 });
            clientManager.AddClient(new Client { Name = "David Hilbert", PhoneNumber = "832-82-76", Balance = (decimal)462878.37 });

            stockManager.AddStock(new Stock { StockType = "Xilinx", Price = (decimal)104.23 });
            stockManager.AddStock(new Stock { StockType = "Texas Instruments", Price = (decimal)120.61});
            stockManager.AddStock(new Stock { StockType = "Boston Scientific Corp", Price = (decimal)43.46 });
            stockManager.AddStock(new Stock { StockType = "STMicroelectronics", Price = (decimal)15.68 });
            stockManager.AddStock(new Stock { StockType = "NXP Semiconductors", Price = (decimal)99.88 });
            stockManager.AddStock(new Stock { StockType = "Strandberg", Price = (decimal)4.10});
            stockManager.AddStock(new Stock { StockType = "Advanced Micro Devices", Price = (decimal)34.19 });
            stockManager.AddStock(new Stock { StockType = "National Instruments", Price = (decimal)43.76});
            stockManager.AddStock(new Stock { StockType = "Keysight Technologies", Price = (decimal)12.50 });
            stockManager.AddStock(new Stock { StockType = "Intel", Price = (decimal)45.98 });
            stockManager.AddStock(new Stock { StockType = "Nvidia", Price = (decimal)154.18 });
            stockManager.AddStock(new Stock { StockType = "Gigabyte Technology", Price = (decimal)46.80 });
            stockManager.AddStock(new Stock { StockType = "Ikea", Price = (decimal)12.50 });
            stockManager.AddStock(new Stock { StockType = "General Electric", Price = (decimal)9.09 });
            stockManager.AddStock(new Stock { StockType = "Lockheed Martin", Price = (decimal)377.18 });
            stockManager.AddStock(new Stock { StockType = "Siemens", Price = (decimal)49.42 });
            stockManager.AddStock(new Stock { StockType = "Mitsubishi Motors", Price = (decimal)4.13 });
            stockManager.AddStock(new Stock { StockType = "Fuji Electric", Price = (decimal)199 });
            stockManager.AddStock(new Stock { StockType = "Morgan Stanley", Price = (decimal)40.36   });
            stockManager.AddStock(new Stock { StockType = "Lotte Shopping", Price = (decimal)0.01 });

            GenerateRandomBlockShares();
        }

        private void GenerateRandomBlockShares()
        {
            int numberOfShares = 200;
            for (int i = 0; i < numberOfShares; i++)
            {
                blockOfSharesManager.CreateRandomShare();
            }
        }
    }
}