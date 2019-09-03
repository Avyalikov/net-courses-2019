namespace TradingAppWebAPI
{
    using TradingApp.Core.Models;
    using TradingApp.Core.Repos;
    using System.Linq;
    using System.Collections.Generic;

    class SharesRepository : DBComm, ISharesRepository
    {
        private readonly DBContext dBContext;

        public SharesRepository(DBContext dBContext) : base(dBContext)
        {
            this.dBContext = dBContext;
        }

        public void Insert(Share share)
        {
            dBContext.Shares.Add(share);
        }

        public Share GetShareByID(int shareID)
        {
            return dBContext.Shares.Where(x => x.ShareID == shareID).FirstOrDefault();
        }

        public IEnumerable<Share> GetAllShares()
        {
            return dBContext.Shares;
        }

        public bool DoesShareExists(int shareID)
        {
            return dBContext.Shares.Where(x => x.ShareID == shareID).FirstOrDefault() != null;
        }

        public bool DoesShareExists(string shareType)
        {
            return dBContext.Shares.Where(x => x.ShareType == shareType).FirstOrDefault() != null;
        }

        public string GetShareType(int shareID)
        {
            return dBContext.Shares.Where(x => x.ShareID == shareID).FirstOrDefault().ShareType;
        }

        public int GetShareIDByType(string shareType)
        {
            return dBContext.Shares.Where(x => x.ShareType == shareType).FirstOrDefault().ShareID;
        }

        public decimal GetSharePrice(int shareID)
        {
            return dBContext.Shares.Where(x => x.ShareID == shareID).FirstOrDefault().Price;
        }
    }
}
