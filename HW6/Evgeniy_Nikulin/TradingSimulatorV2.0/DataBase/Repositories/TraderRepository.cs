namespace TradingSimulator.DataBase.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core.Dto;
    using Core.Repositories;
    using Model;

    public class TraderRepository : ITraderRepository
    {
        private readonly TradingDbContext db;

        public TraderRepository(TradingDbContext db) => this.db = db;

        public int GetTraderCount() =>
            this.db.Traders.Count();

        public List<Trader> GetTradersList()
        {
            var tradersEntity = this.db.Traders.ToList();

            List<Trader> temp = new List<Trader>();

            foreach (var trader in tradersEntity)
            {
                temp.Add(
                    new Trader()
                    {
                        Id = trader.ID,
                        name = trader.Card.Name,
                        surname = trader.Card.Surname,
                        phone = trader.Card.Phone,
                        money = trader.Money,
                    });
            }

            return temp;
        }

        public Trader GetTrader(int TraderID)
        {
            var trader = this.db.Traders
                .Where(t => t.ID == TraderID)
                .SingleOrDefault();

            if (trader == null)
            {
                throw new NullReferenceException($"Incorrect trader ID {TraderID}");
            }

            return new Trader()
            {
                Id = trader.ID,
                name = trader.Card.Name,
                surname = trader.Card.Surname,
                phone = trader.Card.Phone,
                money = trader.Money,
            };
        }

        public void Push(Trader trader)
        {
            var traderEntity = db.Traders
                .Where(t => t.ID == trader.Id)
                .SingleOrDefault();

            if (traderEntity == null)
            {
                db.Traders.Add(
                    new TraderEntity()
                    {
                        Card = new CardEntity()
                            {
                                Name = trader.name,
                                Surname = trader.surname,
                                Phone = trader.phone,
                            },
                        Money = trader.money,
                    });

                return;
            }

            traderEntity.Money = trader.money;
        }

        public void SaveChanges() => this.db.SaveChanges();
    }
}