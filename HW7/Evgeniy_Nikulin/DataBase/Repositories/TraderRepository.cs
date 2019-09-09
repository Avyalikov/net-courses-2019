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
        public TraderRepository(TradingDbContext db) => 
            this.db = db;

        public int GetTraderCount() =>
            this.db.Traders.Count();

        public List<Trader> GetTradersList() =>     
            this.db.Traders
            .ToList()
            .ToTrader();

        public Trader GetTrader(int TraderID) =>
            this.db.Traders
            .Where(t => t.ID == TraderID)
            .SingleOrDefault()
            .ToTrader();

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

            traderEntity.Card.Name = trader.name;
            traderEntity.Card.Surname = trader.surname;
            traderEntity.Card.Phone = trader.phone;
            traderEntity.Money = trader.money;
        }

        public void Remove(Trader share)
        {
            var traderEntity = db.Traders
                .Where(s => s.Card.ID == share.Id)
                .Single();

            db.Traders.Remove(traderEntity);
        }

        public void SaveChanges() => this.db.SaveChanges();
    }
}