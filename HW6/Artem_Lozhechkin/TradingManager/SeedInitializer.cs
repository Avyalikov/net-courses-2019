namespace TradingApp.ConsoleTradingManager
{
    using System.Data.Entity;
    using TradingApp.Core.Models;

    public class SeedInitializer : DropCreateDatabaseAlways<TradingAppDbContext>
    {
        protected override void Seed(TradingAppDbContext context)
        {

            TraderEntity trader1 = new TraderEntity { FirstName = "Igor", LastName = "Igorev", Balance = 500, PhoneNumber = "89999999901" };
            TraderEntity trader2 = new TraderEntity { FirstName = "Vitaly", LastName = "Vitaliev", Balance = -100, PhoneNumber = "89999999902" };
            TraderEntity trader3 = new TraderEntity { FirstName = "Andrey", LastName = "Andreev", Balance = 430, PhoneNumber = "89999999903" };
            TraderEntity trader4 = new TraderEntity { FirstName = "Vladimir", LastName = "Vladimirov", Balance = 390, PhoneNumber = "89999999904" };
            TraderEntity trader5 = new TraderEntity { FirstName = "Vasiliy", LastName = "Vasiliev", Balance = 450, PhoneNumber = "89999999905" };
            TraderEntity trader6 = new TraderEntity { FirstName = "Nikolay", LastName = "Nikolaev", Balance = 0, PhoneNumber = "89999999906" };

            CompanyEntity mcDonalds = new CompanyEntity { Name = "McDonalds" };
            CompanyEntity kfc = new CompanyEntity { Name = "KFC" };
            CompanyEntity burgerKing = new CompanyEntity { Name = "Burger King" };

            StockEntity mcDonaldsStock = new StockEntity { Company = mcDonalds, PricePerUnit = 50 };
            StockEntity kfcStock = new StockEntity { Company = kfc, PricePerUnit = 42 };
            StockEntity burgerKingStock = new StockEntity { Company = burgerKing, PricePerUnit = 40 };

            ShareTypeEntity normal = new ShareTypeEntity { Name = "Normal", Multiplier = 1M };
            ShareTypeEntity privelage = new ShareTypeEntity { Name = "Privelage", Multiplier = 1.5M };
            ShareTypeEntity special = new ShareTypeEntity { Name = "Special", Multiplier = 2.5M };

            ShareEntity kfcShare1 = new ShareEntity { Stock = kfcStock, Owner = trader1, Amount = 3, ShareType = normal };
            ShareEntity kfcShare2 = new ShareEntity { Stock = kfcStock, Owner = trader2, Amount = 2, ShareType = normal };
            ShareEntity kfcShare3 = new ShareEntity { Stock = kfcStock, Owner = trader4, Amount = 40, ShareType = special };

            ShareEntity mcDonaldsShare1 = new ShareEntity { Stock = mcDonaldsStock, Owner = trader1, Amount = 5, ShareType = privelage };
            ShareEntity mcDonaldsShare2 = new ShareEntity { Stock = mcDonaldsStock, Owner = trader3, Amount = 35, ShareType = special };
            ShareEntity mcDonaldsShare3 = new ShareEntity { Stock = mcDonaldsStock, Owner = trader6, Amount = 4, ShareType = normal };

            ShareEntity burgerKingShare1 = new ShareEntity { Stock = burgerKingStock, Owner = trader5, Amount = 25, ShareType = privelage };
            ShareEntity burgerKingShare2 = new ShareEntity { Stock = burgerKingStock, Owner = trader2, Amount = 4, ShareType = normal };
            ShareEntity burgerKingShare3 = new ShareEntity { Stock = burgerKingStock, Owner = trader1, Amount = 2, ShareType = normal };
            ShareEntity burgerKingShare4 = new ShareEntity { Stock = burgerKingStock, Owner = trader3, Amount = 6, ShareType = privelage };

            context.Traders.Add(trader1);
            context.Traders.Add(trader2);
            context.Traders.Add(trader3);
            context.Traders.Add(trader4);
            context.Traders.Add(trader5);
            context.Traders.Add(trader6);

            context.SaveChanges();

            context.Companies.Add(mcDonalds);
            context.Companies.Add(kfc);
            context.Companies.Add(burgerKing);

            context.SaveChanges();

            context.Stocks.Add(mcDonaldsStock);
            context.Stocks.Add(kfcStock);
            context.Stocks.Add(burgerKingStock);

            context.SaveChanges();

            context.ShareTypes.Add(normal);
            context.ShareTypes.Add(privelage);
            context.ShareTypes.Add(special);

            context.SaveChanges();

            context.Shares.Add(kfcShare1);
            context.Shares.Add(kfcShare2);
            context.Shares.Add(kfcShare3);

            context.SaveChanges();

            context.Shares.Add(mcDonaldsShare1);
            context.Shares.Add(mcDonaldsShare2);
            context.Shares.Add(mcDonaldsShare3);

            context.SaveChanges();

            context.Shares.Add(burgerKingShare1);
            context.Shares.Add(burgerKingShare2);
            context.Shares.Add(burgerKingShare3);
            context.Shares.Add(burgerKingShare4);

            context.SaveChanges();
        }
    }
}
