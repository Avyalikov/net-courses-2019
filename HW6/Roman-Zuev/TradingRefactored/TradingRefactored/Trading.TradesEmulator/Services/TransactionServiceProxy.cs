using System;
using System.Linq;
using Trading.Core.Dto;
using Trading.Core.Repositories;
using Trading.Core.Services;

namespace Trading.TradesEmulator.Services
{
    class TransactionServiceProxy : ITransactionService
    {
        private readonly TransactionService transactionService;
        private readonly LoggerLog4Net logger;
        private readonly IClientTableRepository clientTableRepository;
        private readonly ISharesTableRepository sharesTableRepository;

        public TransactionServiceProxy(
            TransactionService transactionService, 
            LoggerLog4Net logger, 
            IClientTableRepository clientTableRepository,
            ISharesTableRepository sharesTableRepository)
        {
            this.transactionService = transactionService;
            this.logger = logger;
            this.clientTableRepository = clientTableRepository;
            this.sharesTableRepository = sharesTableRepository;
            logger.InitLogger();
        }

        public void MakeTransaction(TransactionArguments args)
        {
            logger.Log.Info($"Starting transaction with following parameters:" +
                $"SellerId: {args.SellerId}, BuyerId: {args.BuyerId}, SharesId:{args.SharesId}, Quantity: {args.Quantity}");
            try
            {
                var seller = clientTableRepository.GetById(args.SellerId);
                var buyer = clientTableRepository.GetById(args.BuyerId);
                var sellersItem = seller.Portfolio.FirstOrDefault(p => p.Shares.Id == args.SharesId);
                var buyersItem = buyer.Portfolio.FirstOrDefault(p => p.Shares.Id == args.SharesId);
                decimal sum = sharesTableRepository.GetById(args.SharesId).Price * args.Quantity;
                logger.Log.Info(
                    $"Seller {seller.Name} has {seller.Balance}$ " +
                    $"and {sellersItem.Quantity} {sellersItem.Shares.SharesType} shares that cost" +
                    $"{sellersItem.Shares.Price}before the deal");
                logger.Log.Info(
                    $"Buyer {buyer.Name} has {buyer.Balance}$ " +
                    $"and {buyersItem.Quantity} {buyersItem.Shares.SharesType} before the deal");
                
                transactionService.MakeTransaction(args);

                logger.Log.Info($"Transaction succed.");
                logger.Log.Info($"Deal amount is : {sum} $");
                logger.Log.Info(
                    $"Seller {seller.Name} has {seller.Balance}$ " +
                    $"and {sellersItem.Quantity} {sellersItem.Shares.SharesType} after the deal");
                logger.Log.Info(
                    $"Buyer {buyer.Name} has {buyer.Balance}$ " +
                    $"and {buyersItem.Quantity} {buyersItem.Shares.SharesType} after the deal");
            }
            catch(Exception ex)
            {
                logger.Log.Error(ex.Message);
            }
        }
    }
}
