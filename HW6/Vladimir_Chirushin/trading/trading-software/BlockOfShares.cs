using System.Collections.Generic;
using System.Data.Entity;
namespace trading_software
{
    public class BlockOfShares
    {
        public int BlockOfSharesID { get; set; }
        public virtual Client ClienInBLock { get; set; }
        public virtual Stock StockInBlock { get; set; }
        public int NumberOfShares { get; set; }
    }
}
