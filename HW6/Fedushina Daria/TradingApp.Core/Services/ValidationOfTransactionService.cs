using System;
using System.Collections.Generic;
using TradingApp.Core.Models;
using TradingApp.Core.Repositories;

namespace TradingApp.Core.Services
{
    public class ValidationOfTransactionService
    {
        private readonly IBalanceTableRepository balanceTableRepository;
        public ValidationOfTransactionService(IBalanceTableRepository balanceTableRepository)
        {
            this.balanceTableRepository = balanceTableRepository;
        }
        public bool CheckPermissionToSell(int sellerId)   //Checking if user is in "Black List"
        {
            bool result = true;
            List<BalanceEntity> sellerBalances;
            try
            {
                sellerBalances = balanceTableRepository.GetAll(sellerId);
                foreach (BalanceEntity balance in sellerBalances)
                {
                    if (balance.Balance < 0)
                    {
                        result = false;
                        break;
                    }

                }
                return result;
            }
            catch
            {
                throw new ArgumentException("Can't find any balances for seller. Check if this user registered");
            }            
        }

        public bool CheckPermissionToBuy(int buyerId)    //cheking if user is in "Orange List"
        {
            bool result = true;
            List<BalanceEntity> buyerBalances;
            try
            {
                buyerBalances = balanceTableRepository.GetAll(buyerId);
                foreach (BalanceEntity balance in buyerBalances)
                {
                    if (balance.Balance <= 0)
                    {
                        result = false;
                        break;
                    }
                }
                return result;
            }
            catch
            {
                throw new ArgumentException("Can't find any balances for seller. Check if this user registered");
            }
        }
    }
}