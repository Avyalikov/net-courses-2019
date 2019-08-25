// <copyright file="StockExchange.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp
{
    using TradingApp.DAL;
    using Trading.Core.Repositories;
    using Trading.Core.Services;
    using Trading.Core.Model;
    using Trading.Core.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TradingApp.Repositories;
    using System.Threading;


    /// <summary>
    /// StockExchange description
    /// </summary>
    public class StockExchange
    {
        private readonly ExchangeContext db;

        private readonly ITableRepository<Client> clientTableRepository;
        private readonly ITableRepository<ClientStock> clientStockTableRepository;
        private readonly ITableRepository<Issuer> issuerTableRepository;
        private readonly ITableRepository<Order> orderTableRepository;
        private readonly ITableRepository<PriceHistory> priceHistoryTableRepository;
        private readonly ITableRepository<Stock> stockTableRepository;
        private readonly ITableRepository<TransactionHistory> transactionHistoryTableRepository;

        private readonly ClientService clientService;
        private readonly ClientStockService clientStockService;
        private readonly OrderService orderService;
        private readonly PriceHistoryService priceHistoryService;
        private readonly TransactionHistoryService transactionHistoryService;

        private readonly ILogger logger;

        public StockExchange(ExchangeContext db,
            ITableRepository<Client> clientTableRepository,
            ITableRepository<ClientStock> clientStockTableRepository,
            ITableRepository<Issuer> issuerTableRepository,
            ITableRepository<Order> orderTableRepository,
            ITableRepository<PriceHistory> priceHistoryTableRepository,
            ITableRepository<Stock> stockTableRepository,
            ITableRepository<TransactionHistory> transactionHistoryTableRepository,
            ClientService clientService,
            ClientStockService clientStockService,
            OrderService orderService,
            PriceHistoryService priceHistoryService,
            TransactionHistoryService transactionHistoryService,
            ILogger logger
            )
        {
            this.db = db;
            this.clientTableRepository = clientTableRepository;
            this.clientStockTableRepository = clientStockTableRepository;
            this.issuerTableRepository = issuerTableRepository;
            this.orderTableRepository = orderTableRepository;
            this.priceHistoryTableRepository = priceHistoryTableRepository;
            this.stockTableRepository = stockTableRepository;
            this.transactionHistoryTableRepository = transactionHistoryTableRepository;
            this.clientService = clientService;
            this.clientStockService = clientStockService;
            this.orderService = orderService;
            this.priceHistoryService = priceHistoryService;
            this.transactionHistoryService = transactionHistoryService;
            this.logger = logger;
        }



        public Client GetRandomClient()
        {
            Random random = new Random();
            int clientsAmount = this.clientTableRepository.Count();
            if (clientsAmount == 0)
            {
                throw new NullReferenceException("There are no clients to select from");
            }
            int number = random.Next(1, clientsAmount);
            Client client = clientTableRepository.GetElementAt(number);

            return client;
        }

        public ClientStock GetRandomClientStock(int clientID)
        {
            Random random = new Random();
            var clientStocks = this.clientStockTableRepository.Get(o => o.ClientID == clientID).ToList();
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
            int stocksAmount = stockTableRepository.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(1, stocksAmount);
            Stock stock = stockTableRepository.GetElementAt(number);

            return stock;
        }

        public void SimulatePriceChange(int stockId, decimal priceBeforeChanges, DateTime dateTimeX)
        {
            PriceArguments arguments = new PriceArguments()
            {
                DateTimeLookUp = dateTimeX,
                StockId = stockId

            };

            Random random = new Random();
            bool isPriceIncreaseExpectation = false;
            if (random.Next(100) > 50)
            {
                isPriceIncreaseExpectation = true;
            }
            double percent = (double)(random.Next(5)) / 100;

            DateTime dateTimeEnd = DateTime.Today.AddYears(200);
            decimal price;
            this.priceHistoryService.EditPriceDateEnd(stockId, dateTimeX);



            if (isPriceIncreaseExpectation)
            {
                price = priceBeforeChanges * (decimal)(1 + percent);
            }
            else
            {
                price = priceBeforeChanges * (decimal)(1 - percent);
            }

            PriceInfo priceInfo = new PriceInfo
            {
                StockId = stockId,
                DateTimeBegin = dateTimeX.AddSeconds(1),
                DateTimeEnd = dateTimeEnd,
                Price = price
            };

            this.priceHistoryService.AddPriceInfo(priceInfo);
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

                Order salerOrder = orderService.LastOrder();

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

                Order customerOrder = orderService.LastOrder();

                DateTime dealDateTime = DateTime.Now;
                decimal dealPrice = priceHistoryService.GetStockPriceByDateTime(new PriceArguments()
                {
                    StockId = clstock.StockID,
                    DateTimeLookUp = dealDateTime
                });

                clientService.EditClientBalance(saler.ClientID, (dealPrice * amountForSale));
                this.logger.Info($"Client {saler.ClientID} balance has been increased by {(dealPrice * amountForSale)}");
                clientService.EditClientBalance(customer.ClientID, (-1 * dealPrice * amountForSale));
                this.logger.Info($"Client {customer.ClientID} balance has been reduced by {(dealPrice * amountForSale)}");
                clientStockService.EditClientStocksAmount(saler.ClientID, clstock.StockID, -amountForSale);
                this.logger.Info($"Client {saler.ClientID} stock {salerOrder.StockID} amount has been reduced on {amountForSale}");
                clientStockService.EditClientStocksAmount(customer.ClientID, clstock.StockID, amountForSale);
                this.logger.Info($"Client {customer.ClientID} stock {salerOrder.StockID} amount has been increased on {amountForSale}");


               transactionHistoryService.AddTransactionInfo(new TransactionInfo()
                {
                    CustomerOrderId = customerOrder.OrderID,
                    SalerOrderId = salerOrder.OrderID,
                    TrDateTime = dealDateTime

                });
                this.logger.Info($"Transaction has been added to DB");

                orderService.SetIsExecuted(salerOrder);
                this.logger.Info($"Saler's order {salerOrder.OrderID} status has been set as 'IsExecuted'");
                orderService.SetIsExecuted(customerOrder);
                this.logger.Info($"Customer's order {customerOrder.OrderID} status has been set as 'IsExecuted'");
                this.logger.Info($"Deal is finished");

                this.SimulatePriceChange(salerOrder.StockID, dealPrice, dealDateTime);
                this.logger.Info($"Stock {salerOrder.StockID} price has been changed'");

                
                Thread.Sleep(10000);


            }
        }
    }
}

