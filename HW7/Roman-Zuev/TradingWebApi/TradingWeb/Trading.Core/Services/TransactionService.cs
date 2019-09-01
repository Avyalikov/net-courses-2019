using System;
using Trading.Core.Dto;
using Trading.Core.Models;
using Trading.Core.Repositories;

namespace Trading.Core.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IClientTableRepository clientTableRepository;
        private readonly IClientSharesTableRepository clientSharesTableRepository;
        private readonly ISharesTableRepository sharesTableRepository;
        private readonly ITransactionHistoryTableRepository transactionHistoryTableRepository;

        public TransactionService(
            IClientTableRepository clientTableRepository,
            IClientSharesTableRepository clientSharesTableRepository,
            ISharesTableRepository sharesTableRepository,
            ITransactionHistoryTableRepository transactionHistoryTableRepository
            )
        {
            this.clientTableRepository = clientTableRepository;
            this.clientSharesTableRepository = clientSharesTableRepository;
            this.sharesTableRepository = sharesTableRepository;
            this.transactionHistoryTableRepository = transactionHistoryTableRepository;
        }

        public void MakeTransaction(TransactionArguments args)
        {
            try
            {
                ValidateArguments(args);
                var seller = clientTableRepository.GetById(args.SellerId);
                var buyer = clientTableRepository.GetById(args.BuyerId);
                var sellersItem = GetItemFromPortfolio(seller, args.SharesId);
                var buyersItem = GetItemFromPortfolio(buyer, args.SharesId);
                decimal sum = sharesTableRepository.GetById(args.SharesId).Price * args.Quantity;
                seller.Balance += sum;
                sellersItem.Quantity -= args.Quantity;
                buyer.Balance -= sum;
                buyersItem.Quantity += args.Quantity;

                clientTableRepository.Change(seller);
                clientTableRepository.Change(buyer);
                clientSharesTableRepository.Update(sellersItem);
                clientSharesTableRepository.Update(buyersItem);
                clientTableRepository.SaveChanges();
                clientSharesTableRepository.SaveChanges();
                WriteTransactionHistory(args, sum);
            }
            catch (Exception ex)
            {
                throw new Exception($"Transaction failed : {ex.Message}");
            }
        }

        private void WriteTransactionHistory(TransactionArguments args, decimal sum)
        {
            var transaction = new TransactionHistoryEntity()
            {
                Seller = clientTableRepository.GetById(args.SellerId),
                Buyer = clientTableRepository.GetById(args.BuyerId),
                SelledItem = sharesTableRepository.GetById(args.SharesId),
                Quantity = args.Quantity,
                Total = sum,
                DateTime = DateTime.Now
            };
            transactionHistoryTableRepository.Add(transaction);
            transactionHistoryTableRepository.SaveChanges();
        }
        private ClientSharesEntity GetItemFromPortfolio(ClientEntity client, int sharesId)
        {
            foreach (var item in client.Portfolio)
            {
                if (item.Shares.Id == sharesId)
                {
                    return item;
                }
            }

            var newShares = new ClientSharesEntity()
            {
                Client = client,
                Shares = sharesTableRepository.GetById(sharesId),
                Quantity = 0
            };
            clientSharesTableRepository.Add(newShares);
            clientSharesTableRepository.SaveChanges();
            return newShares;
        }

        private void ValidateArguments(TransactionArguments args)
        {
            if (args.Quantity <= 0)
            {
                throw new ArgumentException($"Wrong quantity : {args.Quantity}");
            }
            if (args.SellerId == args.BuyerId)
            {
                throw new ArgumentException($"Seller and buyer have the same Id");
            }
            if (!clientTableRepository.ContainsById(args.SellerId))
            {
                throw new ArgumentException($"Client with Id {args.SellerId} doesn't exist");
            }
            if (!clientTableRepository.ContainsById(args.BuyerId))
            {
                throw new ArgumentException($"Client with Id {args.BuyerId} doesn't exist");
            }
            if (!sharesTableRepository.ContainsById(args.SharesId))
            {
                throw new ArgumentException($"Shares with Id {args.SharesId} don't exist");
            }
            var buyer = clientTableRepository.GetById(args.BuyerId);
            if (buyer.Balance <= 0)
            {
                throw new ArgumentException(string.Format($"Buyer is in the {0} zone",
                    buyer.Balance < 0 ? "Black" : "Orange"));
            }
            var seller = clientTableRepository.GetById(args.SellerId);
            foreach (var item in seller.Portfolio)
            {
                if (item.Shares.Id == args.SharesId && item.Quantity >= args.Quantity) return;
            }
            throw new ArgumentException($"Not enough shares to sell");
        }
    }
}