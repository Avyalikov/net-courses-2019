using System;
using System.Collections.Generic;
using TradingApp.Core.Dto;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Services
{
    public class BalancesService
    {
        private readonly IBalanceTableRepository balanceTableRepository;
        public BalancesService(IBalanceTableRepository balanceTableRepository)
        {
            this.balanceTableRepository = balanceTableRepository;
        }
        
        public string CreateBalance(BalanceInfo args)
        {
            int balancesCount;
            try
            {
                List<BalanceEntity> userBalanceEntities = this.balanceTableRepository.GetAll(args.UserID);
                balancesCount = userBalanceEntities.Count;
            }
            catch
            {
                throw new ArgumentException($"Can't count balances for this user {args.UserID}. It might be not exists");
            }
            
            var entityToAdd = new BalanceEntity()
            {
                CreatedAt = DateTime.Now,
                UserID = args.UserID,
                BalanceID = args.UserID.ToString() +"0"+ balancesCount.ToString(),
                Balance = args.Balance,
                StockID = args.StockID,
                StockAmount = args.StockAmount,
            };
            if (this.balanceTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Balance with that number is already exists. Can't continue");
            }
            this.balanceTableRepository.Add(entityToAdd);
            this.balanceTableRepository.SaveChanges();
            return entityToAdd.BalanceID;
        }

        public BalanceEntity Get(string balanceId)
        {
            if (!this.balanceTableRepository.Contains(balanceId))
            {
                throw new ArgumentException("Can't find balance with this Id. It might be not exists");
            }
            return this.balanceTableRepository.Get(balanceId);
        }
        public List <BalanceEntity> GetAll(int userID)
        {
            return this.balanceTableRepository.GetAll(userID);
        }
    }
}