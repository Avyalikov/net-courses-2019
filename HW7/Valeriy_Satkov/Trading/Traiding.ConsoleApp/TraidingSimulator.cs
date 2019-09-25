namespace Traiding.ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Traiding.ConsoleApp.DependencyInjection;
    using Traiding.ConsoleApp.Dto;
    using Traiding.ConsoleApp.Logger;
    using Traiding.ConsoleApp.Models;
    using Traiding.ConsoleApp.Strategies;

    public class TraidingSimulator
    {
        private readonly RequestSender requestSender;
        private readonly ILoggerService log;

        public TraidingSimulator(RequestSender requestSender, ILoggerService loggerService)
        {
            this.requestSender = requestSender;
            this.log = loggerService;

            log.InitLogger();
        }

        public void randomDeal(CancellationToken token, int frequencyInSec)
        {
            int count = frequencyInSec;
            int clientsCount;
            int randCustomerId;
            int randSellerId;
            int shareId;
            int sharesNumber;
            IEnumerable<SharesNumberEntity> sellerSharesNumberList;
            Random rand = new Random();

            while (!token.IsCancellationRequested)
            {
                if (count != 0)
                {
                    log.Info(count.ToString());
                    count--;
                    Thread.Sleep(1000);
                    continue;
                }

                log.Info("Now!");

                clientsCount = requestSender.GetClients(10, 1).Count();
                //sharesCount = this.reportsService.GetSharesCount();
                randCustomerId = rand.Next(1, clientsCount);
                log.Info($"Random customer Id is {randCustomerId}");
                randSellerId = 0;
                sellerSharesNumberList = new List<SharesNumberEntity>();
                int sellerSharesNumberListCount = 0;
                while (randSellerId == 0
                    || randSellerId == randCustomerId
                    || sellerSharesNumberListCount == 0)
                {
                    randSellerId = rand.Next(1, clientsCount);
                    sellerSharesNumberList = requestSender.GetSharesNumbersByClientId(randSellerId);
                    sellerSharesNumberListCount = sellerSharesNumberList.Count();
                    log.Info($"Try find seller: sellerId = {randSellerId}, Shares Numbers Count by seller = {sellerSharesNumberListCount}");
                }

                var sellerSharesNumberFirst = sellerSharesNumberList.First();
                shareId = sellerSharesNumberFirst.Share.Id;
                int sellerSharesNumber = sellerSharesNumberFirst.Number;
                sharesNumber = 1;
                if (sellerSharesNumber > 1)
                {
                    sharesNumber++;
                }

                var operationInputData = new OperationInputData
                {
                    CustomerId = randCustomerId,
                    SellerId = randSellerId,
                    ShareId = shareId,
                    RequiredSharesNumber = sharesNumber
                };

                log.Info($"Created OperationInputData with CustomerId = {randCustomerId}, SellerId = { randSellerId}, ShareId = {shareId}, RequiredSharesNumber = { sharesNumber} ");

                var resultString = requestSender.Deal(operationInputData);

                if (!string.IsNullOrWhiteSpace(resultString))
                {
                    log.Info($"Deal was failed cause: {resultString}.");
                }
                else
                {
                    log.Info("Deal was successful.");
                }

                count = frequencyInSec;

                Thread.Sleep(1000);
            }
        }
    }
}
