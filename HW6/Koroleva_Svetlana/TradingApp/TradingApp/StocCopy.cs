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
    public class StockCopy
    {
        private readonly ExchangeContext db;
        private readonly ITableRepository<Client> clientTableRepository;
        private readonly ITableRepository<ClientStock> clientStockTableRepository;
        private readonly ITableRepository<Issuer> issuerTableRepository;
        private readonly ITableRepository<Order> orderTableRepository;
        private readonly ITableRepository<PriceHistory> priceHistoryTableRepository;
        private readonly ITableRepository<Stock> stockTableRepository;
        private readonly ITableRepository<TransactionHistory> transactionHistoryTableRepository;



        public StockCopy(ExchangeContext db,
            ITableRepository<Client> clientTableRepository,
       ITableRepository<ClientStock> clientStockTableRepository,
        ITableRepository<Issuer> issuerTableRepository,
       ITableRepository<Order> orderTableRepository,
         ITableRepository<PriceHistory> priceHistoryTableRepository,
        ITableRepository<Stock> stockTableRepository,
         ITableRepository<TransactionHistory> transactionHistoryTableRepository
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
        }




        public object RunTraiding()
        {
          //  log4net.Config.XmlConfigurator.Configure();
           // var logger = new Logger(log4net.LogManager.GetLogger("Logger"));
           

             //   ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
           //    return clientStockTableRepository.ContainsByPK( 222, 1 );


          PriceHistoryService priceHistoryService = new PriceHistoryService(priceHistoryTableRepository);
            // PriceHistoryTableRepository priceHistoryTableRepository = new PriceHistoryTableRepository(db);
            //  return priceHistoryTableRepository.GetElementAt(2);
            var ph = priceHistoryService.GetStockPriceByDateTime(new PriceArguments {
                DateTimeLookUp = DateTime.Now,
                StockId=2

            });

            return ph;

            /*int amountInLotForSale = 10;
            ClientService clientService = new ClientService(clientTableRepository);
            ClientStockService clientStockService = new ClientStockService(clientStockTableRepository);
            OrderService orderService = new OrderService(orderTableRepository);
            PriceHistoryService priceHistoryService = new PriceHistoryService(priceHistoryTableRepository);

            //Select random saler
            Client saler = clientService.GetRandomClient();
            //Select random stock for saler
            ClientStock clstock = clientStockService.GetRandomClientStock(saler.ClientID);
            if (clstock == null)
            {
                throw new NullReferenceException("This client has no stocks, select another saler");

            }

            //determine amount for sale
            int lotsAmount = clstock.Quantity / amountInLotForSale;
            Random random = new Random();
            int amountForSale = random.Next(1, lotsAmount) * amountInLotForSale;

            orderService.AddOrder(new OrderInfo()
            {
                ClientId = clstock.ClientID,
                StockId= clstock.StockID,
                Quantity=amountForSale,
               ordType=OrderInfo.OrdType.Sale
            });


            Order salerOrder = orderService.lastOrder();

            Client customer;
            do
            {
                customer = clientService.GetRandomClient();
            }
            while (customer.ClientID == saler.ClientID);

            orderService.AddOrder(new OrderInfo()
            {
                ClientId = customer.ClientID,
                StockId = clstock.StockID,
                Quantity = amountForSale,
                ordType = OrderInfo.OrdType.Purchase
            });

            Order customerOrder = orderService.lastOrder();

            DateTime dealDateTime = DateTime.Now;
            decimal dealPrice = priceHistoryService.GetStockPriceByDateTime(new PriceArguments()
            {
                StockId=clstock.StockID,
                DateTimeLookUp=dealDateTime
            });

            clientService.EditClientBalance(saler.ClientID, (dealPrice*amountForSale));
            clientService.EditClientBalance(customer.ClientID, (-1*dealPrice * amountForSale));

            clientStockService.EditClientStocksAmount(saler.ClientID, clstock.StockID, -amountForSale);
            clientStockService.EditClientStocksAmount(customer.ClientID, clstock.StockID, amountForSale);

            orderService.SetIsExecuted(salerOrder);
            orderService.SetIsExecuted(customerOrder);

            priceHistoryService.EditPriceDateEnd(salerOrder.StockID, dealDateTime);
            priceHistoryService.SimulatePriceChange(salerOrder.StockID, dealDateTime);

            Thread.Sleep(10000);*/


        }
        }
    }

