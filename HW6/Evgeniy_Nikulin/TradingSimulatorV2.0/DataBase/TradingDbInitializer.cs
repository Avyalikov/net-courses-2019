namespace TradingSimulator.DataBase
{
    using System.Data.Entity;
    using Model;

    class TradingDbInitializer : DropCreateDatabaseAlways<TradingDbContext>
    {
        protected override void Seed(TradingDbContext context)
        {
            TraderEntity trader1 = new TraderEntity
            {
                Card = new CardEntity { Name = "Brian", Surname = "Kelly", Phone = "+1(310)938-63-48" },
                Money = 5000m
            };
            TraderEntity trader2 = new TraderEntity
            {
                Card = new CardEntity { Name = "Yves", Surname = "Guillemot", Phone = "+33(66)671-69-74" },
                Money = 5000m
            };
            TraderEntity trader3 = new TraderEntity
            {
                Card = new CardEntity { Name = "Fusajiro", Surname = "Yamauchi", Phone = "+81(726)374-54-93" },
                Money = 5000m
            };
            TraderEntity trader4 = new TraderEntity
            {
                Card = new CardEntity { Name = "Trip", Surname = "Hawkins", Phone = "+1(650)220-68-41" },
                Money = 5000m
            };
            TraderEntity trader5 = new TraderEntity
            {
                Card = new CardEntity { Name = "Akio", Surname = "Morita", Phone = "+81(3)483-81-43" },
                Money = 5000m
            };

            context.Traders.Add(trader1);
            context.Traders.Add(trader2);
            context.Traders.Add(trader3);
            context.Traders.Add(trader4);
            context.Traders.Add(trader5);

            ShareEntity share1 = new ShareEntity { Name = "Activision Blizzard", Price = 47.99m, Owner = trader1, Quantity = 100 };
            ShareEntity share2 = new ShareEntity { Name = "Activision Blizzard", Price = 47.99m, Owner = trader2, Quantity = 10 };
            ShareEntity share3 = new ShareEntity { Name = "Ubisoft", Price = 72.94m, Owner = trader2, Quantity = 100 };
            ShareEntity share4 = new ShareEntity { Name = "Ubisoft", Price = 72.94m, Owner = trader3, Quantity = 10 };
            ShareEntity share5 = new ShareEntity { Name = "Nintendo", Price = 45.95m, Owner = trader3, Quantity = 100 };
            ShareEntity share6 = new ShareEntity { Name = "Nintendo", Price = 45.95m, Owner = trader4, Quantity = 10 };
            ShareEntity share7 = new ShareEntity { Name = "Electronic Arts", Price = 92.23m, Owner = trader4, Quantity = 100 };
            ShareEntity share8 = new ShareEntity { Name = "Electronic Arts", Price = 92.23m, Owner = trader5, Quantity = 10 };
            ShareEntity share9 = new ShareEntity { Name = "Sony Corp", Price = 56.50m, Owner = trader5, Quantity = 100 };
            ShareEntity share10 = new ShareEntity { Name = "Sony Corp", Price = 56.50m, Owner = trader1, Quantity = 10 };

            context.Shares.Add(share1);
            context.Shares.Add(share2);
            context.Shares.Add(share3);
            context.Shares.Add(share4);
            context.Shares.Add(share5);
            context.Shares.Add(share6);
            context.Shares.Add(share7);
            context.Shares.Add(share8);
            context.Shares.Add(share9);
            context.Shares.Add(share10);

            base.Seed(context);
        }
    }
}