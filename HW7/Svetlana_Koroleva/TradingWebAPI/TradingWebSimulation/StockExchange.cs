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
    using System.Net;
    using System.IO;
    using Newtonsoft.Json;
    using System.Net.Http;
    using static System.Net.WebRequestMethods;

    /// <summary>
    /// StockExchange description
    /// </summary>
    public class StockExchange
    {
      
        static readonly HttpClient httpclient = new HttpClient();
        private readonly IUnitOfWork unitOfWork;

        private readonly IClientService clientService;
        private readonly IClientStockService clientStockService;
        private readonly IOrderService orderService;
        private readonly IStockService stockService;
        private readonly ITransactionHistoryService transactionHistoryService;

        private readonly ILogger logger;

        public StockExchange(
            IUnitOfWork unitOfWork,

            IClientService clientService,
            IClientStockService clientStockService,
            IOrderService orderService,
            IStockService stockService,
            ITransactionHistoryService transactionHistoryService,

             ILogger logger
            )
        {
            
            this.unitOfWork = unitOfWork;
            this.clientService = clientService;
            this.clientStockService = clientStockService;
            this.orderService = orderService;
            this.stockService = stockService;
            this.transactionHistoryService = transactionHistoryService;
            this.logger = logger;
        }



        public IEnumerable<Client> GetAllClients()
        {
            var response = httpclient.GetAsync("http://localhost:5000/clients/all").Result;
            string json = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<Client>>(json);
            }
            throw new Exception(response.StatusCode.ToString());
        }

        public IEnumerable<ClientStock> GetAllClientStocks(int clientId)
        {
            var response = httpclient.GetAsync($"http://localhost:5000/shares/getclientstock?clientId={clientId}").Result;
            string json = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<ClientStock>>(json);
            }
            throw new Exception(response.StatusCode.ToString());
        }

        public Order GetLastOrder()
        {
            var response =  httpclient.GetAsync("http://localhost:5000/transactions/lastorder").Result;
            string json = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
               return  JsonConvert.DeserializeObject<Order>(json);
            }
            throw new Exception(response.StatusCode.ToString());
        }


        public Stock GetStockById(int stockId)
        {
            var response = httpclient.GetAsync($"http://localhost:5000/shares/getstock?stockid={stockId}").Result;
            string json = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Stock>(json);
            }
            throw new Exception(response.StatusCode.ToString());
        }

        public Task AddOrder(OrderInfo orderInfo)
         {
                 return httpclient.PostAsJsonAsync($"http://localhost:5000/deal/addorder", orderInfo);
        }

        public Task AddClientStock(ClientStockInfo clientStockInfo)
        {
            return httpclient.PostAsJsonAsync($"http://localhost:5000/shares/add", clientStockInfo);
        }


        public void EditClient(int clientid, ClientInfo clientInfo)
        {
           
          httpclient.PostAsJsonAsync($"http://localhost:5000/clients/update?id={clientid}", clientInfo);
         
        }


        public void EditClientStockAmount(int clientid, int stockid, ClientStockInfo clientStockInfo)
        {
          httpclient.PostAsJsonAsync($"http://localhost:5000/shares/update?clientid={clientid}&stockid={stockid}", clientStockInfo);
            
        }

        public Client GetRandomClient()
        {
            Random random = new Random();
            var allClients = this.GetAllClients();
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
            var clientStocks = this.GetAllClientStocks(clientID);
            int stocksAmount = clientStocks.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(0, stocksAmount - 1);
            ClientStock clientStock = clientStocks.ToList()[number];
            return clientStock;
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
                var clstock = GetRandomClientStock(saler.ClientID);
                if (clstock == null || clstock.Quantity < 10)
                {
                    continue;
                   
                }
               
                //determine amount for sale
                int lotsAmount = clstock.Quantity / amountInLotForSale;
                Random random = new Random();
                int amountForSale = random.Next(1, lotsAmount) * amountInLotForSale;

               


                Task addsalerOrder = Task.Run(
                    () => this.AddOrder(new OrderInfo()
                {
                    ClientId = clstock.ClientID,
                    StockId = clstock.StockID,
                    Quantity = amountForSale,
                    OrderType = OrderInfo.OrdType.Sale
                }));

                addsalerOrder.Wait();

                this.logger.Info($"Order for sale stock {clstock.StockID} for client {clstock.ClientID} has been added to DB");

                int salerorderId = this.GetLastOrder().OrderID;

                Client customer;
                do
                {
                    customer = GetRandomClient();
                }
                while (customer.ClientID == saler.ClientID);



                Task addcustomerOrder=Task.Run(()=>this.AddOrder(new OrderInfo()
                {
                    ClientId = customer.ClientID,
                    StockId = clstock.StockID,
                    Quantity = amountForSale,
                    OrderType = OrderInfo.OrdType.Purchase
                }));

                addcustomerOrder.Wait();
                this.logger.Info($"Order for purchasing stock {clstock.StockID} for client {customer.ClientID} has been added to DB");

                int customerorderId = this.GetLastOrder().OrderID;
                DateTime dealDateTime = DateTime.Now;
                decimal dealPrice = this.GetStockById(clstock.StockID).Price;

                ClientInfo salerInfo = new ClientInfo() {
                    FirstName = saler.FirstName,
                    LastName=saler.LastName,
                    Phone=saler.Phone,
                    Balance=saler.Balance+(dealPrice * amountForSale)
                };

                this.EditClient(saler.ClientID, salerInfo);
                this.logger.Info($"Client {saler.ClientID} balance has been increased by {(dealPrice * amountForSale)}");

                ClientInfo customerInfo = new ClientInfo()
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Phone = customer.Phone,
                    Balance = customer.Balance - (dealPrice * amountForSale)
                };

                this.EditClient(customer.ClientID, customerInfo);
                this.logger.Info($"Client {customer.ClientID} balance has been reduced by {(dealPrice * amountForSale)}");


                ClientStockInfo salerStockInfo = new ClientStockInfo()
                {
                    ClientId = saler.ClientID,
                    StockId = clstock.StockID,
                    Amount =clstock.Quantity-amountForSale
                };
               this.EditClientStockAmount(saler.ClientID, clstock.StockID, salerStockInfo );
              
              
                this.logger.Info($"Client {saler.ClientID} stock {clstock.StockID} amount has been reduced on {amountForSale}");

                var customerStock = this.GetAllClientStocks(customer.ClientID).Where(s => s.StockID == clstock.StockID).SingleOrDefault();
                if (customerStock==null)
                {
                    ClientStockInfo clientStockInfo = new ClientStockInfo()
                    {
                        ClientId = customer.ClientID,
                        StockId = clstock.StockID,
                        Amount = amountForSale
                    };
                    Task addStock = Task.Run(() => AddClientStock(clientStockInfo));
                    addStock.Wait();
                }

                else
                {
                    ClientStockInfo clientStockInfo = new ClientStockInfo()
                    {
                        ClientId=customer.ClientID,
                        StockId=clstock.StockID,
                        Amount = customerStock.Quantity+amountForSale
                    };
                    this.EditClientStockAmount(customer.ClientID, clstock.StockID, clientStockInfo);
                }
                this.logger.Info($"Client {customer.ClientID} stock {clstock.StockID} amount has been increased on {amountForSale}");


               transactionHistoryService.AddTransactionInfo(new TransactionInfo()
                {
                    TrDateTime = dealDateTime

                });
                var lastTransaction = transactionHistoryService.GetLastTransaction();
                this.logger.Info($"Transaction {lastTransaction.TransactionHistoryID} has been added to DB");

                orderService.SetIsExecuted(salerorderId, lastTransaction.TransactionHistoryID);
               this.logger.Info($"Saler's order {salerorderId} status has been set as 'IsExecuted'");
                orderService.SetIsExecuted(customerorderId, lastTransaction.TransactionHistoryID);
               this.logger.Info($"Customer's order {customerorderId} status has been set as 'IsExecuted'");
                this.logger.Info($"Deal is finished");

                
                Thread.Sleep(10000);


            }
        }
    }
}

