using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Dto;
using Traiding.Core.Models;
using Traiding.Core.Repositories;

namespace Traiding.Core.Services
{
    public class SalesService
    {
        private IOperationTableRepository operationTableRepository;
        private IBalanceTableRepository balanceTableRepository;
        private IBlockedMoneyTableRepository blockedMoneyTableRepository;
        private ISharesNumberTableRepository sharesNumberTableRepository;
        private IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository;

        public SalesService(IOperationTableRepository operationTableRepository)
        {
            this.operationTableRepository = operationTableRepository;
        }

        public SalesService(IBalanceTableRepository balanceTableRepository)
        {
            this.balanceTableRepository = balanceTableRepository;
        }

        public SalesService(IBlockedMoneyTableRepository blockedMoneyTableRepository)
        {
            this.blockedMoneyTableRepository = blockedMoneyTableRepository;
        }

        public SalesService(ISharesNumberTableRepository sharesNumberTableRepository)
        {
            this.sharesNumberTableRepository = sharesNumberTableRepository;
        }

        public SalesService(IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository)
        {
            this.blockedSharesNumberTableRepository = blockedSharesNumberTableRepository;
        }

        /* Sale
         * 0.  Get info about purchase from program (Customer, Seller, Number of Shares, Total (money))
         * 1.  Create empty operation
         * 2.1 Get Customer balance info
         * 2.2 - Customer balance amount
         * 3.  Create blocked money
         * 4.1 Get Seller shares number info
         * 4.2 - Seller shares number
         * 5.  Create blocked shares number // after that action purchase can't cancel
         * 6.1 Get Seller balance info
         * 6.2 + Seller balance amount
         * 7.1 Get Customer shares number info
         * 7.2 + Customer shares number
         * 8.  Fill operation columns
         * 9.  Remove blocked money
         * 10. Remove blocked shares number
         */
        
        /* 'Operation' methods
         */
        public int CreateOperation()
        {
            var entityToAdd = new OperationEntity();

            this.operationTableRepository.Add(entityToAdd);

            this.operationTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsOperationById(int entityId)
        {
            if (!this.operationTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find operation with this Id. May it has not been registered.");
            }
        }

        public OperationEntity GetOperation(int entityId)
        {
            ContainsOperationById(entityId);

            return this.operationTableRepository.Get(entityId);
        }

        public void FillOperationColumns(int operationId, int blockedMoneyEntityId, int blockedSharesNumberEntityId)
        {
            ContainsOperationById(operationId);

            this.operationTableRepository.FillCustomerColumns(operationId, blockedMoneyEntityId);
            this.operationTableRepository.FillSellerColumns(operationId, blockedSharesNumberEntityId);
            this.operationTableRepository.SetChargeDate(operationId, DateTime.Now);

            this.operationTableRepository.SaveChanges();
        }

        public void RemoveOperation(int entityId)
        {
            ContainsOperationById(entityId);

            this.operationTableRepository.Remove(entityId);

            this.operationTableRepository.SaveChanges();
        }

        /* 'Balance' methods
         */
        public void ContainsBalanceById(int entityId)
        {
            if (!this.balanceTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find balance of client by this Id. May it has not been registered.");
            }
        }

        public BalanceEntity GetBalance(int entityId)
        {
            ContainsBalanceById(entityId);

            return this.balanceTableRepository.Get(entityId);
        }

        public void ChangeBalance(int entityId, decimal newAmount)
        {
            ContainsBalanceById(entityId);

            this.balanceTableRepository.ChangeAmount(entityId, newAmount);

            this.balanceTableRepository.SaveChanges();
        }

        /* 'Blocked money' methods
         */
        public int CreateBlockedMoney(BlockedMoneyRegistrationInfo args)
        {
            var entityToAdd = new BlockedMoneyEntity()
            {
                CreatedAt = DateTime.Now,
                ClientBalance = args.ClientBalance,
                Operation = args.Operation,
                Customer = args.ClientBalance.Client,
                Total = args.Total
            };

            if (this.blockedMoneyTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Bloked money with this data has been registered. Can't continue.");
            }

            this.blockedMoneyTableRepository.Add(entityToAdd);

            this.blockedMoneyTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsBlockedMoneyById(int entityId)
        {
            if (!this.blockedMoneyTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find bloked money with this Id. May it has not been registered.");
            }
        }

        public BlockedMoneyEntity GetBlockedMoney(int entityId)
        {
            ContainsBlockedMoneyById(entityId);

            return this.blockedMoneyTableRepository.Get(entityId);
        }

        public void RemoveBlockedMoney(int entityId)
        {
            ContainsBlockedMoneyById(entityId);

            this.blockedMoneyTableRepository.Remove(entityId);

            this.blockedMoneyTableRepository.SaveChanges();
        }

        /* 'Shares number' methods
         */
        public void ContainsSharesNumberById(int entityId)
        {
            if (!this.sharesNumberTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find shares number of client by this Id. May it has not been registered.");
            }
        }

        public SharesNumberEntity GetSharesNumber(int entityId)
        {
            ContainsSharesNumberById(entityId);

            return this.sharesNumberTableRepository.Get(entityId);
        }

        public void ChangeSharesNumber(int entityId, int newNumber)
        {
            ContainsSharesNumberById(entityId);

            this.sharesNumberTableRepository.ChangeNumber(entityId, newNumber);

            this.sharesNumberTableRepository.SaveChanges();
        }

        /* 'Blocked shares number' methods
         */
        public int CreateBlockedSharesNumber(BlockedSharesNumberRegistrationInfo args)
        {
            var entityToAdd = new BlockedSharesNumberEntity()
            {
                CreatedAt = DateTime.Now,
                ClientSharesNumber = args.ClientSharesNumber,
                Operation = args.Operation,
                Seller = args.ClientSharesNumber.Client,
                Share = args.Share,
                ShareTypeName = args.ShareTypeName,
                Cost = args.Cost,
                Number = args.Number
            };

            if (this.blockedSharesNumberTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("Blocked Shares Number with this data has been registered. Can't continue.");
            }

            this.blockedSharesNumberTableRepository.Add(entityToAdd);

            this.blockedSharesNumberTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public void ContainsBlockedSharesNumberById(int entityId)
        {
            if (!this.blockedSharesNumberTableRepository.ContainsById(entityId))
            {
                throw new ArgumentException("Can't find bloked shares number with this Id. May it has not been registered.");
            }
        }

        public object GetBlockedSharesNumber(int entityId)
        {
            ContainsBlockedSharesNumberById(entityId);

            return this.blockedSharesNumberTableRepository.Get(entityId);
        }

        public void RemoveBlockedSharesNumber(int entityId)
        {
            ContainsBlockedSharesNumberById(entityId);

            this.blockedSharesNumberTableRepository.Remove(entityId);

            this.blockedSharesNumberTableRepository.SaveChanges();
        }
    }
}
