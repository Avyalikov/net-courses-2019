namespace trading_software
{
    public class DataBaseInitializer
    {
        // You can initiate your database with this test data.
        // Add this method at first line of Run() method of Trading engine
        // Compile, Run and then delete line with Initiate() from Run()
        public void Initiate()
        {
            using (var db = new TradingContext())
            {
                db.Clients.Add(new Client { Name = "Tosin Abasi", PhoneNumber = "555-32-12", Balance = (decimal) 45938.12 });
                db.Clients.Add(new Client { Name = "Jennifer Lawrence", PhoneNumber = "333-02-14", Balance = (decimal)43709.14 });
                db.Clients.Add(new Client { Name = "Kilgore Trout", PhoneNumber = "939-12-22", Balance = (decimal)2356079.45 });
                db.Clients.Add(new Client { Name = "Milla Jovovich", PhoneNumber = "555-02-43", Balance = (decimal)57803.39 });
                db.Clients.Add(new Client { Name = "Matt Garstka", PhoneNumber = "493-09-75", Balance = (decimal)9056387.26 });
                db.Clients.Add(new Client { Name = "David Lynch", PhoneNumber = "493-19-35", Balance = (decimal)9368.23 });
                db.Clients.Add(new Client { Name = "Tim Rot", PhoneNumber = "555-05-54", Balance = (decimal)43789.75 });
                db.Clients.Add(new Client { Name = "Tina Kandelaki", PhoneNumber = "796-32-46", Balance = (decimal)89358.93 });
                db.Clients.Add(new Client { Name = "Martin Heidegger", PhoneNumber = "234-42-51", Balance = (decimal)438526.01 });
                db.Clients.Add(new Client { Name = "Michel Foucault", PhoneNumber = "264-56-53", Balance = (decimal)463165.57 });
                db.Clients.Add(new Client { Name = "Ludwig Wittgenstein", PhoneNumber = "546-86-43", Balance = (decimal)5623031.00 });
                db.Clients.Add(new Client { Name = "Bertrand Russell", PhoneNumber = "363-23-49", Balance = (decimal)25378.11 });
                db.Clients.Add(new Client { Name = "Kobo Abe", PhoneNumber = "539-42-53", Balance = (decimal)436078.34 });

                db.Stocks.Add(new Stock { StockType = "Xilinx", Price = (decimal)104.23 });
                db.Stocks.Add(new Stock { StockType = "Texas Instruments", Price = (decimal)120.61});
                db.Stocks.Add(new Stock { StockType = "Boston Scientific Corp", Price = (decimal)43.46 });
                db.Stocks.Add(new Stock { StockType = "STMicroelectronics", Price = (decimal)15.68 });
                db.Stocks.Add(new Stock { StockType = "NXP Semiconductors", Price = (decimal)99.88 });
                db.Stocks.Add(new Stock { StockType = "Strandberg", Price = (decimal)4.10});
                db.Stocks.Add(new Stock { StockType = "Advanced Micro Devices", Price = (decimal)34.19 });
                db.Stocks.Add(new Stock { StockType = "National Instruments", Price = (decimal)43.76});
                db.Stocks.Add(new Stock { StockType = "Keysight Technologies", Price = (decimal)12.50 });
                db.Stocks.Add(new Stock { StockType = "Intel", Price = (decimal)45.98 });
                db.Stocks.Add(new Stock { StockType = "Nvidia", Price = (decimal)154.18 });
                db.Stocks.Add(new Stock { StockType = "Gigabyte Technology", Price = (decimal)46.80 });
            }
        }
    }
}
