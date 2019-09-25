using HW7.Core.Dto;
using HW7.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core.Repositories
{
    public interface ISharesRepository
    {
        Share AddShare(ShareToAdd share);

        IQueryable<ShareWithPrice> GetSharesWithPrice(int shareId);

        Share UpdateShare(ShareToUpdate share);

        bool RemoveShare(int id);

        decimal GetPrice(int shareId);

        List<int> GetAvailableShares(int traderId);
    }
}
