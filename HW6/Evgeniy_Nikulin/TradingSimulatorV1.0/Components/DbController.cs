namespace TradingSimulator.Components
{
    using Core.Interfaces;
    using Core.Model;
    using DataBase;
    using System.Collections.Generic;
    using System.Linq;

    public class DbController : IDbController
    {
        private readonly IPhraseProvider provider;

        public DbController(IPhraseProvider provider)
        {
            this.provider = provider;
        }
        public int GetTraderCount()
        {
            int temp;

            using (var db = new TradingDbContext())
            {
                temp = db.Traders.Count();
            }

            return temp;
        }

        public PersonalCard GetTraderCard(int TraderID)
        {
            PersonalCard temp;

            using (var db = new TradingDbContext())
            {
                temp = db.Traders
                    .Where(t => t.ID == TraderID)
                    .Single()
                    .Card;
            }

            return temp;
        }

        public int GetSharesCount(int TraderID)
        {
            int temp;

            using (var db = new TradingDbContext())
            {
                temp = db.Traders
                    .Where(t => t.ID == TraderID)
                    .Single()
                    .ShareList
                    .Count();
            }

            return temp;
        }

        public Share GetShare(int TraderID, int index)
        {
            Share temp;

            using (var db = new TradingDbContext())
            {
                temp = db.Traders
                    .Where(t => t.ID == TraderID)
                    .Single()
                    .ShareList[index];
            }

            return temp;
        }

        public void SafeTransaction(Transaktion trn)
        {
            using (var db = new TradingDbContext())
            {
                var seller = db.Traders
                    .Where(t => t.Card.ID == trn.seller.ID)
                    .Single();
                var buyer = db.Traders
                    .Where(t => t.Card.ID == trn.buyer.ID)
                    .Single();
                var sellerShare = seller.ShareList
                    .Where(s => s.Name == trn.share.Name)
                    .Single();
                var buyerShare = buyer.ShareList
                    .Where(s => s.Name == trn.share.Name)
                    .SingleOrDefault();

                sellerShare.Quantity -= trn.share.Quantity;
                if (sellerShare.Quantity == 0)
                {
                    db.Shares.Remove(sellerShare);
                }

                if (buyerShare != default)
                {
                    buyerShare.Quantity += trn.share.Quantity;
                }
                else
                {
                    db.Shares.Add(
                        new Share
                        {
                            Name = sellerShare.Name,
                            Price = sellerShare.Price,
                            Owner = buyer,
                            Quantity = trn.share.Quantity
                        });
                }


                seller.Money += sellerShare.Price;
                buyer.Money -= sellerShare.Price;
                if (buyer.Money == 0m)
                {
                    buyer.Reputation = provider.GetPhrase(Phrase.Orange);
                }
                else if (buyer.Money < 0m)
                {
                    buyer.Reputation = provider.GetPhrase(Phrase.Black);
                }
                else
                {
                    buyer.Reputation = provider.GetPhrase(Phrase.White);
                }

                db.SaveChanges();
            }
        }

        public void AddTrader(string Name, string Surname, string Phone, decimal Money, string Reputation)
        {
            using (var db = new TradingDbContext())
            {
                db.Traders.Add(
                    new Trader
                    {
                        Card = new PersonalCard { Name = Name, Surname = Surname, Phone = Phone },
                        Money = Money,
                        Reputation = Reputation
                    });

                db.SaveChanges();
            }
        }

        public void AddShareToTrader(string shareName, decimal price, int quantity, int OwnerId)
        {
            using (var db = new TradingDbContext())
            {
                var owner = db.Traders
                    .Where(t => t.Card.ID == OwnerId)
                    .Single();

                db.Shares.Add(new Share
                {
                    Name = shareName,
                    Price = price,
                    Quantity = quantity,
                    Owner = owner
                });

                db.SaveChanges();
            }
        }

        public void ChangeShare(int shareId, string newName, decimal newPrice, int OwnerId)
        {
            using (var db = new TradingDbContext())
            {
                var owner = db.Traders
                    .Where(t => t.Card.ID == OwnerId)
                    .Single();

                var share = owner.ShareList
                    .Where(s => s.ID == shareId)
                    .Single();

                share.Name = newName;
                share.Price = newPrice;

                db.SaveChanges();
            }
        }

        public List<Trader> GetTradersList()
        {
            List<Trader> temp = new List<Trader>();

            using (var db = new TradingDbContext())
            {
                var traders = db.Traders
                    .Select(t => t)
                    .OrderBy(t => t.Card.ID)
                    .ToList();

                foreach (var t in traders)
                {
                    temp.Add(new Trader()
                    {
                        ID = t.ID,
                        Card = t.Card,
                        Money = t.Money,
                        Reputation = t.Reputation
                    });
                }
            }

            return temp;
        }

        public List<Share> GetShareList(int OnerId)
        {
            List<Share> temp;

            using (var db = new TradingDbContext())
            {
                temp = db.Traders
                    .Where(t => t.ID == OnerId)
                    .Single()
                    .ShareList
                    .ToList();
            }

            return temp;
        }
    }
}