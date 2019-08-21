namespace DataBase
{
    using System.Data.Entity;
    using Core.Model;

    class TradingDbInitializer : DropCreateDatabaseAlways<TradingDbContext>
    {
        protected override void Seed(TradingDbContext context)
        {
            Trader trader1 = new Trader
            {
                Card = new PersonalCard { Name = "Brian", Surname = "Kelly", Phone = "+1(310)938-63-48" },
                Money = 100m,
                Reputation = "White"
            };
            Trader trader2 = new Trader
            {
                Card = new PersonalCard { Name = "Yves", Surname = "Guillemot", Phone = "+33(66)671-69-74" },
                Money = 100m,
                Reputation = "White"
            };
            Trader trader3 = new Trader
            {
                Card = new PersonalCard { Name = "Fusajiro", Surname = "Yamauchi", Phone = "+81(726)374-54-93" },
                Money = 100m,
                Reputation = "White"
            };
            Trader trader4 = new Trader
            {
                Card = new PersonalCard { Name = "Trip", Surname = "Hawkins", Phone = "+1(650)220-68-41" },
                Money = 100m,
                Reputation = "White"
            };
            Trader trader5 = new Trader
            {
                Card = new PersonalCard { Name = "Akio", Surname = "Morita", Phone = "+81(3)483-81-43" },
                Money = 100m,
                Reputation = "White"
            };

            context.Traders.Add(trader1);
            context.Traders.Add(trader2);
            context.Traders.Add(trader3);
            context.Traders.Add(trader4);
            context.Traders.Add(trader5);

            Share share1 = new Share { Name = "Activision Blizzard", Price = 47.99m, Owner = trader1, Quantity = 100 };
            Share share2 = new Share { Name = "Activision Blizzard", Price = 47.99m, Owner = trader2, Quantity = 10 };
            Share share3 = new Share { Name = "Ubisoft", Price = 72.94m, Owner = trader2, Quantity = 100 };
            Share share4 = new Share { Name = "Ubisoft", Price = 72.94m, Owner = trader3, Quantity = 10 };
            Share share5 = new Share { Name = "Nintendo", Price = 45.95m, Owner = trader3, Quantity = 100 };
            Share share6 = new Share { Name = "Nintendo", Price = 45.95m, Owner = trader4, Quantity = 10 };
            Share share7 = new Share { Name = "Electronic Arts", Price = 92.23m, Owner = trader4, Quantity = 100 };
            Share share8 = new Share { Name = "Electronic Arts", Price = 92.23m, Owner = trader5, Quantity = 10 };
            Share share9 = new Share { Name = "Sony Corp", Price = 56.50m, Owner = trader5, Quantity = 100 };
            Share share10 = new Share { Name = "Sony Corp", Price = 56.50m, Owner = trader1, Quantity = 10 };

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