using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface ITransactionRepositories
    {
        List<UserEntity> GetUserList();
        List<AddingSharesToThUserEntity> GetUserAndShareList(UserEntity userEntity);
        List<SharesEntity> GetShares(AddingSharesToThUserEntity addingSharesToThUserEntity);
        void SaveChanges();
        void UpdateUserTable(UserEntity user);
        void UpdateUserTable(AddingSharesToThUserEntity userAndShares);
        List<AddingSharesToThUserEntity> GetUserAndShareParameters(UserEntity userEntity, SharesEntity sharesEntity);
        void Add(TransactionHistoryEntity transactionHistoryEntity);
    }
}
