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
    using Traiding.ConsoleApp.Models;
    using Traiding.ConsoleApp.Strategies;

    public class TraidingSimulator
    {
        private readonly RequestSender requestSender;

        public TraidingSimulator(RequestSender requestSender)
        {
            this.requestSender = requestSender;
        }

        public void Start(CancellationToken token)
        {
            int count = 10;
            int clientsCount;
            int randCustomerId;
            int randSellerId;
            int shareId;
            int sharesNumber;
            IEnumerable<SharesNumberEntity> sellerSharesNumberList;
            Random rand = new Random();

            while (!token.IsCancellationRequested)
            {
                clientsCount = requestSender.GetClients(10, 1).Count();
                //sharesCount = this.reportsService.GetSharesCount();
                randCustomerId = rand.Next(1, clientsCount);
                randSellerId = 0;
                sellerSharesNumberList = new List<SharesNumberEntity>();
                while (randSellerId == 0
                    || randSellerId == randCustomerId
                    || sellerSharesNumberList.Count() == 0)
                {
                    randSellerId = rand.Next(1, clientsCount);
                    sellerSharesNumberList = requestSender.GetSharesNumbersByClientId(randSellerId);
                }

                var sellerSharesNumberFirst = sellerSharesNumberList.First();
                shareId = sellerSharesNumberFirst.Share.Id;
                int sellerSharesNumber = sellerSharesNumberFirst.Number;
                sharesNumber = 1;
                if (sellerSharesNumber > 1)
                {
                    sharesNumber++;
                }

                if (count == 0)
                {
                    //Console.WriteLine("Now!");
                    var operationInputData = new OperationInputData
                    {
                        CustomerId = randCustomerId,
                        SellerId = randSellerId,
                        ShareId = shareId,
                        RequiredSharesNumber = sharesNumber
                    };

                    requestSender.Deal(operationInputData);
                    count = 10;
                }
                else
                {
                    //Console.WriteLine(count);
                    count--;
                }

                Thread.Sleep(400);
            }
        }
    }
}
