using HW7.Core;
using HW7.Core.Dto;
using HW7.Core.Models;
using HW7.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW7.Server.Repositories
{
    public class SharesRepository : ISharesRepository
    {
        private IContextProvider context;

        public SharesRepository(IContextProvider context)
        {
            this.context = context;
        }

        public Share AddShare(ShareToAdd share)
        {
            var newShare = context.Shares.Add(new Share
            {
                Name = share.Name,
                Price = share.Price,
            });

            context.SaveChanges();

            return newShare;
        }

        public List<int> GetAvailableShares(int traderId)
        {
            return context.Portfolios.Where(p => p.TraderID == traderId).Select(x => x.ShareId).ToList();
        }

        public decimal GetPrice(int shareId)
        {
            return context.Shares.Where(s => s.ShareId == shareId).Select(x => x.Price).FirstOrDefault();
        }

        public IQueryable<ShareWithPrice> GetSharesWithPrice(int traderId)
        {
            var trader = context.Traders.Find(traderId);

            if (trader == null)
            {
                return null;
            }

            return from p in context.Portfolios
                   where p.TraderID == traderId
                   join s in context.Shares on p.ShareId equals s.ShareId
                   select new ShareWithPrice { TraderId = p.TraderID, ShareId = p.ShareId, Quantity = p.Quantity, Price = s.Price };
        }

        public bool RemoveShare(int id)
        {
            var shareToRemove = context.Shares.Find(id);

            if (shareToRemove == null)
            {
                return false;
            }

            context.Shares.Remove(shareToRemove);
            
            context.SaveChanges();

            return true;
        }

        public Share UpdateShare(ShareToUpdate share)
        {
            var shareToChange = context.Shares.Find(share.ShareId);

            if (shareToChange == null)
            {
                return null;
            }

            shareToChange.Name = share.Name.Length == 0 ? shareToChange.Name : share.Name;
            shareToChange.Price = share.Price == 0 ? shareToChange.Price : share.Price;
           
            context.SaveChanges();

            return context.Shares.Find(share.ShareId);
        }
    }
}
