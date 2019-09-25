namespace Traiding.Core.Services
{
    using System;
    using Traiding.Core.Dto;
    using Traiding.Core.Models;
    using Traiding.Core.Repositories;

    public class SalesService
    {
        private IOperationTableRepository operationTableRepository;
        private IBalanceTableRepository balanceTableRepository;
        private IBlockedMoneyTableRepository blockedMoneyTableRepository;
        private ISharesNumberTableRepository sharesNumberTableRepository;
        private IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository;
        private IShareTableRepository shareTableRepository;

        public SalesService(IOperationTableRepository operationTableRepository, IBalanceTableRepository balanceTableRepository, IBlockedMoneyTableRepository blockedMoneyTableRepository, ISharesNumberTableRepository sharesNumberTableRepository, IBlockedSharesNumberTableRepository blockedSharesNumberTableRepository, IShareTableRepository shareTableRepository)
        {
            this.operationTableRepository = operationTableRepository;
            this.balanceTableRepository = balanceTableRepository;
            this.blockedMoneyTableRepository = blockedMoneyTableRepository;
            this.sharesNumberTableRepository = sharesNumberTableRepository;
            this.blockedSharesNumberTableRepository = blockedSharesNumberTableRepository;
            this.shareTableRepository = shareTableRepository;
        }



        /* Sale
         * 0.  Get info about purchase from program (Customer, Number of Shares, Total (money))
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
        public void Deal(int customerId, int sellerId, int shareId, int requiredSharesNumber)
        {
            OperationEntity operation = null;
            SharesNumberEntity customerSharesNumber = null;
            SharesNumberEntity sellerSharesNumber = null;
            ClientEntity customer = null;
            //ClientEntity seller = null;
            BalanceEntity customerBalance = null;
            BalanceEntity sellerBalance = null;
            ShareEntity share = null;
            BlockedMoneyEntity blockedMoney = null;
            bool blockedMoneyFlag = false;
            BlockedSharesNumberEntity blockedSharesNumber = null;
            bool blockedSharesNumberFlag = false;
            decimal customerBalanceAmount = 0M;            
            decimal total = 0M;
            int sellerSharesNumberNumber = 0;      
            
            operation = CreateOperation();
            try
            {
                sellerSharesNumber = SearchSharesNumberForBuy(shareId, requiredSharesNumber); // search required shares on stock exchange
                //seller = sellerSharesNumber.Client;
                //share = sellerSharesNumber.Share;
                share = GetShare(shareId);
                customerBalance = SearchBalanceByClientId(customerId);
                customer = customerBalance.Client;
                //sellerBalance = SearchBalanceByClientId(seller.Id);
                sellerBalance = SearchBalanceByClientId(sellerId);

                // get total
                CheckShareAndShareTypeStatuses(share);
                total = share.Type.Cost * requiredSharesNumber;

                // Blocked money
                customerBalanceAmount = customerBalance.Amount;
                if (customerBalanceAmount < total)
                {
                    throw new ArgumentException("Customer don't have enough money.");
                }                
                blockedMoneyFlag = ChangeBalance(customerBalance, customerBalanceAmount - total);
                blockedMoney = CreateBlockedMoney(new BlockedMoneyRegistrationInfo()
                {
                    Operation = operation,
                    ClientBalance = customerBalance,
                    Total = total
                });

                // Blocked shares Number
                sellerSharesNumberNumber = sellerSharesNumber.Number;                
                blockedSharesNumberFlag = ChangeSharesNumber(sellerSharesNumber, sellerSharesNumberNumber - requiredSharesNumber);
                blockedSharesNumber = CreateBlockedSharesNumber(new BlockedSharesNumberRegistrationInfo()
                {
                    ClientSharesNumber = sellerSharesNumber,
                    Operation = operation,
                    //Share = sellerSharesNumber.Share,
                    //ShareTypeName = sellerSharesNumber.Share.Type.Name,
                    //Cost = sellerSharesNumber.Share.Type.Cost,
                    Number = requiredSharesNumber
                });
            }
            catch
            {
                RemoveOperation(operation);

                if (blockedMoneyFlag)
                {
                    ChangeBalance(customerBalance, customerBalanceAmount + total);
                    if (blockedMoney != null)
                    {
                        RemoveBlockedMoney(blockedMoney);
                    }
                }

                if (blockedSharesNumberFlag)
                {
                    ChangeSharesNumber(sellerSharesNumber, sellerSharesNumberNumber + requiredSharesNumber);
                    if (blockedSharesNumber != null)
                    {
                        RemoveBlockedSharesNumber(blockedSharesNumber);
                    }
                }

                // throw new ArgumentException($"Deal was broken cause: {e.Message}");
                throw;
            }

            if (sellerSharesNumber.Number == 0)
            {
                RemoveSharesNumber(sellerSharesNumber);
            }
            
            ChangeBalance(sellerBalance, sellerBalance.Amount + total);

            customerSharesNumber = SearchOrCreateSharesNumberForAddition(customer, share);
            ChangeSharesNumber(customerSharesNumber, customerSharesNumber.Number + requiredSharesNumber);

            FillOperationColumns(blockedMoney, blockedSharesNumber);

            RemoveBlockedMoney(blockedMoney);
            RemoveBlockedSharesNumber(blockedSharesNumber);
        }

        /* 'Operation' methods
         */
        public OperationEntity CreateOperation()
        {
            var entityToAdd = new OperationEntity()
            {
                DebitDate = DateTime.Now,
                Customer = null,
                ChargeDate = DateTime.Now,
                Seller = null,
                Share = null,
                ShareTypeName = null,
                Cost = 1,
                Number = 1,
                Total = 1
            };

            this.operationTableRepository.Add(entityToAdd);

            this.operationTableRepository.SaveChanges();

            return entityToAdd;
        }             

        public void FillOperationColumns(BlockedMoneyEntity blockedMoney, BlockedSharesNumberEntity blockedSharesNumber)
        {
            /* Operation entity:
             * DateTime DebitDate { get; set; } // it's date from Customer blocked money
             * ClientEntity Customer { get; set; }
             * DateTime ChargeDate { get; set; } // it's date of finish
             * ClientEntity Seller { get; set; }
             * ShareEntity Share { get; set; }
             * string ShareTypeName { get; set; } // see ShareTypeEntity.Name (The name will be fixed here at the time of purchase)
             * decimal Cost { get; set; } // see ShareTypeEntity.Cost (The cost will be fixed here at the time of purchase)
             * int Number { get; set; } // Number of shares for deal
             * decimal Total { get; set; } // Total = Cost * Number
             */

            this.operationTableRepository.FillAllColumns(blockedMoney, blockedSharesNumber, DateTime.Now);

            this.operationTableRepository.SaveChanges();
        }

        public void RemoveOperation(OperationEntity operation)
        {
            this.operationTableRepository.Remove(operation.Id);

            this.operationTableRepository.SaveChanges();
        }

        /* 'Balance' methods
         */
        public bool ChangeBalance(BalanceEntity balance, decimal newAmount)
        {
            this.balanceTableRepository.ChangeAmount(balance.Id, newAmount);

            this.balanceTableRepository.SaveChanges();

            return true;
        }

        public BalanceEntity SearchBalanceByClientId(int clientId)
        {
            BalanceEntity balanceEntity = this.balanceTableRepository.SearchBalanceByClientId(clientId);
            if (balanceEntity == null)
            {
                throw new ArgumentException("Can't find client balance by Id");
            }
            if (balanceEntity.Status == false)
            {
                throw new ArgumentException("Operations on this balance have been blocked.");
            }

            return balanceEntity;
        }

        /* 'Blocked money' methods
         */
        public BlockedMoneyEntity CreateBlockedMoney(BlockedMoneyRegistrationInfo args)
        {
            var entityToAdd = new BlockedMoneyEntity()
            {
                CreatedAt = DateTime.Now,
                ClientBalance = args.ClientBalance,
                Operation = args.Operation,
                Customer = args.ClientBalance.Client,
                Total = args.Total
            };

            this.blockedMoneyTableRepository.Add(entityToAdd);

            this.blockedMoneyTableRepository.SaveChanges();

            return entityToAdd;
        }

        public void RemoveBlockedMoney(BlockedMoneyEntity blockedMoney)
        {
            this.blockedMoneyTableRepository.Remove(blockedMoney.Id);

            this.blockedMoneyTableRepository.SaveChanges();
        }

        /* 'Shares number' methods
         */
        public SharesNumberEntity CreateSharesNumber(SharesNumberRegistrationInfo args)
        {
            var entityToAdd = new SharesNumberEntity()
            {
                Client = args.Client,
                Share = args.Share,
                Number = args.Number
            };

            this.sharesNumberTableRepository.Add(entityToAdd);

            this.sharesNumberTableRepository.SaveChanges();

            return entityToAdd;
        }

        public bool ChangeSharesNumber(SharesNumberEntity sharesNumber, int newNumber)
        {
            this.sharesNumberTableRepository.ChangeNumber(sharesNumber.Id, newNumber);

            this.sharesNumberTableRepository.SaveChanges();

            return true;
        }

        public SharesNumberEntity SearchSharesNumberForBuy(int shareId, int requiredSharesNumber)
        {
            var result = this.sharesNumberTableRepository.SearchSharesNumberForBuy(shareId, requiredSharesNumber);
            if (result == null)
            {
                throw new ArgumentException("Can't find client with required shares number");
            }
            return result;
        }

        public SharesNumberEntity SearchOrCreateSharesNumberForAddition(ClientEntity client, ShareEntity share)
        {
            SharesNumberEntity result = this.sharesNumberTableRepository.SearchSharesNumberForAddition(client.Id, share.Id);
            if (result == null)
            {
                result = CreateSharesNumber(new SharesNumberRegistrationInfo()
                {
                    Client = client,
                    Share = share,
                    Number = 0
                });
            }
            return result;
        }        

        public void RemoveSharesNumber(SharesNumberEntity sharesNumber)
        {
            this.sharesNumberTableRepository.Remove(sharesNumber.Id);

            this.sharesNumberTableRepository.SaveChanges();
        }

        /* 'Blocked shares number' methods
         */
        public BlockedSharesNumberEntity CreateBlockedSharesNumber(BlockedSharesNumberRegistrationInfo args)
        {
            var entityToAdd = new BlockedSharesNumberEntity()
            {
                CreatedAt = DateTime.Now,
                ClientSharesNumber = args.ClientSharesNumber,
                Operation = args.Operation,
                Seller = args.ClientSharesNumber.Client,
                Share = args.ClientSharesNumber.Share,
                ShareTypeName = args.ClientSharesNumber.Share.Type.Name,
                Cost = args.ClientSharesNumber.Share.Type.Cost,
                Number = args.Number
            };

            this.blockedSharesNumberTableRepository.Add(entityToAdd);

            this.blockedSharesNumberTableRepository.SaveChanges();

            return entityToAdd;
        }
        public void RemoveBlockedSharesNumber(BlockedSharesNumberEntity blockedSharesNumber)
        {
            this.blockedSharesNumberTableRepository.Remove(blockedSharesNumber.Id);

            this.blockedSharesNumberTableRepository.SaveChanges();
        }

        /* 'Share' methods
         */
        public void CheckShareAndShareTypeStatuses(ShareEntity share)
        {
            if (share.Status == false)
            {
                throw new ArgumentException("Operations with this share have been blocked.");
            }
            if (share.Type.Status == false)
            {
                throw new ArgumentException("Operations with this share type have been blocked.");
            }
        }

        public ShareEntity GetShare(int shareId)
        {
            if (!this.shareTableRepository.ContainsById(shareId))
            {
                throw new ArgumentException("Can't find share by this Id. May it has not been registered.");
            }

            return this.shareTableRepository.Get(shareId);
        }
    }
}
