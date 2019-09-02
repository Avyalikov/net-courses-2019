using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradingSimulator.Core.Models;
using TradingSimulator.Core.Repositories;
using TradingSimulatorConsoleApp.Data;

namespace TradingSimulatorConsoleApp.Repositories
{
    public class TransactionRepositories : ITransactionRepositories
    {
        private readonly TradingSimulatorDbContext dbContext;

        public TransactionRepositories(TradingSimulatorDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<UserEntity> GetUserList()
        {
            var users = dbContext.Users.ToList();
            return users;
        }

        public List<UserAndSharesEntity> GetUserAndShareList(UserEntity userEntity)
        {
            int idUser = userEntity.Id;
            var userAndShare = dbContext.UsersAndShares.Where(q => q.UserId == idUser).ToList();
            return userAndShare;
        }

        public List<SharesEntity> GetShares(UserAndSharesEntity addingSharesToThUserEntity)
        {
            int idShares = addingSharesToThUserEntity.ShareId;
            var shares = dbContext.Shares.Where(q => q.Id == idShares).ToList();
            return shares;
        }

        public List<UserAndSharesEntity> GetUserAndShareParameters(UserEntity userEntity, SharesEntity sharesEntity)
        {
            int idUser = userEntity.Id;
            int idShare = sharesEntity.Id;
            var userAndShare = dbContext.UsersAndShares.Where(q => q.UserId == idUser && q.ShareId == idShare).ToList();
            return userAndShare;
        }

        public void UpdateUserTable(UserEntity user)
        {
            this.dbContext.Update(user);
        }

        public void UpdateUserTable(UserAndSharesEntity userAndShares)
        {
            this.dbContext.Update(userAndShares);
        }

        public void SaveChanges()
        {
            this.dbContext.SaveChanges();
        }

        public void Add(TransactionHistoryEntity transactionHistoryEntity)
        {
            this.dbContext.TransactionHistories.Add(transactionHistoryEntity);
        }
    }
}
