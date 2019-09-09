using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITransactionRepositories
    {
        List<UserEntity> GetUserList();
        List<UserAndSharesEntity> GetUserAndShareList(UserEntity userEntity);
        List<SharesEntity> GetShares(UserAndSharesEntity addingSharesToThUserEntity);
        List<UserAndSharesEntity> GetUserAndShareParameters(UserEntity userEntity, SharesEntity sharesEntity);
        void UpdateUserTable(UserEntity user);
        void UpdateUserTable(UserAndSharesEntity userAndShares);
        void SaveChanges();
        void Add(TransactionHistoryEntity transactionHistoryEntity);
    }
}
