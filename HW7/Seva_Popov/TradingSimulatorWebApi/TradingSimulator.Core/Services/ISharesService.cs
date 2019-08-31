using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Services
{
    public interface ISharesService
    {
        IEnumerable<SharesEntity> Get();
        SharesEntity Add(SharesEntity shares);
        SharesEntity Delete(int id);
        SharesEntity Put(SharesEntity shares);
    }
}
