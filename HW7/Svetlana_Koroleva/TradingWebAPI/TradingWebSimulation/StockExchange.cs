// <copyright file="StockExchange.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingWebSimulation
{
    using SharedContext.DAL;
    using Trading.Core.Repositories;
    using Trading.Core.Services;
    using Trading.Core.Model;
    using Trading.Core.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SharedContext.Repositories;
    using System.Threading;
    using Trading.Core.IServices;
    using Trading.Core;
    using SharedContext;

    /// <summary>
    /// StockExchange description
    /// </summary>
    public class StockExchange
    {
       // private readonly ExchangeContext db;

      /*  private readonly IClientRepository clientRepository;
        private readonly IClientStockRepository clientStockRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IStockRepository stockRepository;
        private readonly ITransactionHistoryRepository transactionHistoryRepository;*/



        private readonly IUnitOfWork unitOfWork;

        private readonly IClientService clientService;
        private readonly IClientStockService clientStockService;
        private readonly IOrderService orderService;
        private readonly IStockService stockService;
        private readonly ITransactionHistoryService transactionHistoryService;

        private readonly ILogger logger;

        public StockExchange(/*ExchangeContext db,
            IClientRepository clientRepository,
        IClientStockRepository clientStockRepository,
            IOrderRepository orderRepository,
            IStockRepository stockRepository,
            ITransactionHistoryRepository transactionHistoryRepository,*/

            IUnitOfWork unitOfWork,

            IClientService clientService,
            IClientStockService clientStockService,
            IOrderService orderService,
            IStockService stockService,
            ITransactionHistoryService transactionHistoryService,

             ILogger logger
            )
        {
            /*this.db = db;
            this.clientRepository = clientRepository;
            this.clientStockRepository = clientStockRepository;
            this.orderRepository = orderRepository;
            this.stockRepository = stockRepository;
            this.transactionHistoryRepository = transactionHistoryRepository;*/
            this.unitOfWork = unitOfWork;
            this.clientService = clientService;
            this.clientStockService = clientStockService;
            this.orderService = orderService;
            this.stockService = stockService;
            this.transactionHistoryService = transactionHistoryService;
            this.logger = logger;
        }



        public Client GetRandomClient()
        {
            Random random = new Random();
            var allClients = this.unitOfWork.Clients.GetAll();
            int clientsAmount = allClients.Count();
            if (clientsAmount == 0)
            {
                throw new NullReferenceException("There are no clients to select from");
            }
            int number = random.Next(0, clientsAmount-1);
            Client client = allClients.ToList().ElementAt(number);
            return client;
        }

        public ClientStock GetRandomClientStock(int clientID)
        {
            Random random = new Random();
            var clientStocks = this.unitOfWork.ClientStocks.Get(o => o.ClientID == clientID).ToList();
            int stocksAmount = clientStocks.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(0, stocksAmount - 1);
            ClientStock clientStock = clientStocks.ToList()[number];
            return clientStock;
        }

        public Stock GetRandomStock()
        {
            Random random = new Random();
            var allStocks = this.unitOfWork.Stocks.GetAll();
            int stocksAmount = allStocks.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(0, stocksAmount-1);
            Stock stock = allStocks.ElementAt(number);

            return stock;
        }


        public void RunTraiding()
        {
            
            int loopcount = 10;

            for (int i = 0; i < loopcount; i++)
            {
              
                int amountInLotForSale = 10;


                //Select random saler
                Client saler = GetRandomClient();
                //Select random stock for saler
                ClientStock clstock = GetRandomClientStock(saler.ClientID);
                if (clstock == null || clstock.Quantity < 10)
                {
                    continue;
                   
                }

                //determine amount for sale
                int lotsAmount = clstock.Quantity / amountInLotForSale;
                Random random = new Random();
                int amountForSale = random.Next(1, lotsAmount) * amountInLotForSale;

                orderService.AddOrder(new OrderInfo()
                {
                    ClientId = clstock.ClientID,
                    StockId = clstock.StockID,
                    Quantity = amountForSale,
                    OrderType = OrderInfo.OrdType.Sale
                });
                this.logger.Info($"Order for sale stock {clstock.StockID} for client {clstock.ClientID} has been added to DB");

                //Order salerOrder = orderService.LastOrder();
                int salerorderId = orderService.LastOrder().OrderID;

                Client customer;
                do
                {
                    customer = GetRandomClient();
                }
                while (customer.ClientID == saler.ClientID);

                orderService.AddOrder(new OrderInfo()
                {
                    ClientId = customer.ClientID,
                    StockId = clstock.StockID,
                    Quantity = amountForSale,
                    OrderType = OrderInfo.OrdType.Purchase
                });
                this.logger.Info($"Order for purchasing stock {clstock.StockID} for client {customer.ClientID} has been added to DB");

                //Order customerOrder = orderService.LastOrder();
                int customerorderId = orderService.LastOrder().OrderID;
                DateTime dealDateTime = DateTime.Now;
                decimal dealPrice = this.stockService.GetEntityByID(clstock.StockID).Price;
                   
                clientService.EditClientBalance(saler.ClientID, (dealPrice * amountForSale));
                this.logger.Info($"Client {saler.ClientID} balance has been increased by {(dealPrice * amountForSale)}");
                clientService.EditClientBalance(customer.ClientID, (-1 * dealPrice * amountForSale));
                this.logger.Info($"Client {customer.ClientID} balance has been reduced by {(dealPrice * amountForSale)}");
                clientStockService.EditClientStocksAmount(saler.ClientID, clstock.StockID, -amountForSale);
                //this.logger.Info($"Client {saler.ClientID} stock {salerOrder.StockID} amount has been reduced on {amountForSale}");
                clientStockService.EditClientStocksAmount(customer.ClientID, clstock.StockID, amountForSale);
               // this.logger.Info($"Client {customer.ClientID} stock {salerOrder.StockID} amount has been increased on {amountForSale}");


               transactionHistoryService.AddTransactionInfo(new TransactionInfo()
                {
                    TrDateTime = dealDateTime

                });
                var lastTransaction = transactionHistoryService.GetLastTransaction();
                this.logger.Info($"Transaction {lastTransaction.TransactionHistoryID} has been added to DB");

                orderService.SetIsExecuted(salerorderId, lastTransaction.TransactionHistoryID);
               // this.logger.Info($"Saler's order {salerOrder.OrderID} status has been set as 'IsExecuted'");
                orderService.SetIsExecuted(customerorderId, lastTransaction.TransactionHistoryID);
              //  this.logger.Info($"Customer's order {customerOrder.OrderID} status has been set as 'IsExecuted'");
                this.logger.Info($"Deal is finished");

                
                Thread.Sleep(10000);


            }
        }
    }
}

