using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Timers;
using Trading.Core.Dto;
using Trading.Core.Repositories;
using Trading.Core.Services;
using Trading.TradesEmulator.Models;

namespace Trading.TradesEmulator
{
    public class TradesEmulator : ITradesEmulator
    {
        private readonly IClientTableRepository clientTableRepository;
        private readonly ISharesTableRepository sharesTableRepository;
        private readonly ITransactionService transactionService;
        private static Timer aTimer;

        public TradesEmulator(
            IClientTableRepository clientTableRepository,
            ISharesTableRepository sharesTableRepository,
            ITransactionService transactionService)
        {
            this.clientTableRepository = clientTableRepository;
            this.sharesTableRepository = sharesTableRepository;
            this.transactionService = transactionService;
        }

        public void Run()
        {
            SetTimer();
            Console.ReadKey();
        }

        private TransactionArguments GenerateTransactionArguments()
        {

            Random random = new Random();
            var args = new TransactionArguments();
            args.SellerId = clientTableRepository[random.Next(0, 100) % clientTableRepository.Count].Id;
            do
            {
                args.BuyerId = clientTableRepository[random.Next(0, 100) % clientTableRepository.Count].Id;
            } while (args.SellerId == args.BuyerId);
            args.SharesId = sharesTableRepository[random.Next(0, 100) % sharesTableRepository.Count].Id;
            args.Quantity = random.Next(1, 15);
            return args;
        }

        private void StartTrades(Object source, ElapsedEventArgs e)
        {
            var transactionArgs = GenerateTransactionArguments();
            transactionService.MakeTransaction(transactionArgs);
        }
        private void SetTimer()
        {
            aTimer = new Timer(10000);
            aTimer.Elapsed += StartTrades;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
    }
}
