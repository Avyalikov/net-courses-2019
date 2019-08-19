namespace TradingSimulator.Core.Components
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using TradingSimulator.Interfaces;
    using TradingSimulator.Model;

    public class TraderMenager : ITraderMenager
    {
        private readonly Random rd = new Random();
        public List<Transaktion> transaktion = new List<Transaktion>();

        public readonly IInputOutput io;

        private readonly int StartPointX;
        private readonly int StartPointY;

        public TraderMenager(IInputOutput io, int StartPointX, int StartPointY)
        {
            this.io = io;

            this.StartPointX = StartPointX;
            this.StartPointY = StartPointY;
        }

        private Transaktion Trade()
        {
            Transaktion trn;

            using (var db = new TradingDbContext())
            {
                var brokersCount = db.Brokers.Count();
                int sellerId = rd.Next(1, brokersCount + 1);
                int buyerId;
                do
                {
                    buyerId = rd.Next(1, brokersCount + 1);
                }
                while (sellerId == buyerId);

                var seller = db.Brokers.Where(b => b.ID == sellerId).Single();
                var buyer = db.Brokers.Where(b => b.ID == buyerId).Single();
                var share = seller.ShareList[rd.Next(seller.ShareList.Count)];

                trn = new Transaktion
                {
                    sellerName = seller.Card.Name,
                    sellerSurname = seller.Card.Surname,
                    buyerName = buyer.Card.Name,
                    buyerSurname = buyer.Card.Surname,
                    shareName = share.Name,
                    quantity = rd.Next(1, share.Quantity)
                };
            }

            return trn;
        }
        private void ManageTransaktion(Transaktion trn)
        {
            using (var db = new TradingDbContext())
            {
                var seller = db.Brokers
                    .Where(b => b.Card.Name == trn.sellerName && b.Card.Surname == trn.sellerSurname)
                    .Single();
                var buyer = db.Brokers
                    .Where(b => b.Card.Name == trn.buyerName && b.Card.Surname == trn.buyerSurname)
                    .Single();
                var sellerShare = seller.ShareList
                    .Where(s => s.Name == trn.shareName)
                    .Single();
                var buyerShare = buyer.ShareList
                    .Where(s => s.Name == trn.shareName)
                    .SingleOrDefault();

                sellerShare.Quantity -= trn.quantity;
                if (sellerShare.Quantity == 0)
                {
                    db.Shares.Remove(sellerShare);
                }

                if (buyerShare != default)
                {
                    buyerShare.Quantity += trn.quantity;
                }
                else
                {
                    db.Shares.Add(
                        new Share
                        {
                            Name = sellerShare.Name,
                            Price = sellerShare.Price,
                            Owner = buyer,
                            Quantity = trn.quantity
                        });
                }


                seller.Money += sellerShare.Price;
                buyer.Money -= sellerShare.Price;

                db.SaveChanges();
            }

        }

        public void Run()
        {
            for (int i = StartPointY; i < StartPointY + 5; i++)
            {
                io.Print("> ", StartPointX - 1, i);
            }

            new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    transaktion.Add(Trade());
                    ManageTransaktion(transaktion.Last());
                    Draw(StartPointX, StartPointY);
                }
            }).Start();
        }

        void Draw(int x, int y)
        {
            io.Clear(x, y, x + 100, y + 5);

            foreach (var item in transaktion.TakeLast(5))
            {
                io.Print(item.ToString(), x, y);
                y++;
            }
        }

        void AddTrader(string Name, string Surname, string Phone)
        {
            using (var db = new TradingDbContext())
            {
                db.Brokers.Add(
                    new Broker {
                        Money = 10000,
                        Card = new PersonalCard
                        {
                            Name = Name,
                            Surname = Surname,
                            Phone = Phone
                        }
                    });

                db.SaveChanges();
            }
        }
    }
}