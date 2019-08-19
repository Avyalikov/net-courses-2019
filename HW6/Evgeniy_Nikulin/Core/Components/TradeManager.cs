namespace Core.Components
{
    using Core.Model;
    using Interfaces;
    using System;
    using System.Collections.Generic;

    public class TradeManager : ITradeMenager
    {
        private readonly Random rd = new Random();
        public List<Transaktion> Transaktion { get; private set; }

        private readonly IDbController db;

        public TradeManager(IDbController db)
        {
            this.db = db;

            Transaktion = new List<Transaktion>();
        }

        public void Trade()
        {
            int traderCount = db.GetTraderCount();
            int sellerId = rd.Next(1, traderCount + 1);
            int buyerId;
            do
            {
                buyerId = rd.Next(1, traderCount + 1);
            }
            while (sellerId == buyerId);

            int shareCount = db.GetSharesCount(sellerId);
            var share = db.GetShare(sellerId, rd.Next(shareCount));

            var trn = new Transaktion(
                seller: db.GetTraderCard(sellerId),
                buyer: db.GetTraderCard(buyerId),
                share: share,
                quantity: rd.Next(1, share.Quantity));

            Transaktion.Add(trn);
            db.SafeTransaction(trn);
        }
    }
}