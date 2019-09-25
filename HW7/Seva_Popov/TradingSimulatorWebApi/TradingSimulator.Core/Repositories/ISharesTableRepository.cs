using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ISharesTableRepository
    {
        IEnumerable<SharesEntity> Get();
        SharesEntity Add(SharesEntity shares);
        SharesEntity Delete(int id);
        SharesEntity Put(SharesEntity shares);
    }
}
