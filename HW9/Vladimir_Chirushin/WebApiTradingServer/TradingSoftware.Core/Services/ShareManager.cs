namespace TradingSoftware.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TradingSoftware.Core.Models;
    using TradingSoftware.Core.Repositories;

    public class ShareManager : IShareManager
    {
        private readonly ISharesRepository sharesRepository;

        public ShareManager(ISharesRepository sharesRepository)
        {
            this.sharesRepository = sharesRepository;
        }

        public void AddShare(string shareName, decimal sharePrice)
        {
            var share = new Share
            {
                ShareType = shareName,
                Price = sharePrice
            };
            this.AddShare(share);
        }

        public void AddShare(Share share)
        {
            if (!this.IsShareExist(share.ShareType))
            {
                this.sharesRepository.Insert(share);
            }
            else
            {
                throw new Exception($"There is already share with id = {share.ShareID}");
            }
        }

        public string GetShareType(int shareID)
        {
            if (this.IsShareExist(shareID))
            {
                return this.sharesRepository.GetShareType(shareID);
            }

            throw new Exception($"There is no shares with id = {shareID}");
        }

        public int GetShareID(string shareType)
        {
            if (this.IsShareExist(shareType))
            {
                return this.sharesRepository.GetShareID(shareType);
            }

            throw new Exception($"There is no shares with type = {shareType}");
        }

        public int GetNumberOfShares()
        {
            return this.sharesRepository.GetNumberOfShares();
        }

        public decimal GetSharePrice(int shareID)
        {
            if (this.IsShareExist(shareID))
            {
                return this.sharesRepository.GetSharePrice(shareID);
            }

            throw new Exception($"There is no shares with id = {shareID}");
        }

        public IEnumerable<Share> GetAllShares()
        {
            return this.sharesRepository.GetAllShares();
        }

        public bool IsShareExist(int shareID)
        {
            return this.sharesRepository.IsShareExist(shareID);
        }

        public bool IsShareExist(string shareType)
        {
            return this.sharesRepository.IsShareExist(shareType);
        }

        public void ChangeSharePrice(int shareID, decimal sharePrice)
        {
            if (this.IsShareExist(shareID))
            {
                this.sharesRepository.ChangeSharePrice(shareID, sharePrice);
            }
            else
            {
                throw new Exception($"There is no shares with id = {shareID}");
            }
        }
    }
}