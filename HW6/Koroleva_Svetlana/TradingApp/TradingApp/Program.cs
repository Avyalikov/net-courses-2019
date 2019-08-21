using TradingApp.DAL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Trading.Core.Services;
using Trading.Core.Repositories;
using TradingApp.Repositories;
using Trading.Core.DTO;
using Trading.Core.Model;

namespace TradingApp
{
    class Program
    {
        

        private static ExchangeContext db = new ExchangeContext();
        private static ITableRepository repository=new ClientTableRepository(db);
        private static ITableRepository repository2 = new PriceHistoryTableRepository(db);
        // private static IOnePKTableRepository repositorypart2 = new ClientTableRepository(db);

        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();
           // var logger = new Logger(log4net.LogManager.GetLogger("Logger"));




            using (db)
            {
               ClientService clientService = new ClientService( repository);
                PriceHistoryService priceHistoryService = new PriceHistoryService(repository2);
                /* clientService.AddClientToDB(new ClientInfo()
                 {
                     FirstName = "Fedor1000",
                     LastName = "Iv",
                     Phone = "1234567",
                     Balance = 1000,

                 }
                 );
                 clientService.AddClientToDB(new ClientInfo()
                 {
                     FirstName = "Fedor200",
                     LastName = "Iv",
                     Phone = "1234567",
                     Balance = 1000,

                 });*/
                //clientService.EditClientBalance(2,203);
                // var client= clientService.GetEntityByID(2);
                PriceArguments priceArguments = new PriceArguments()
                {
                    DateTimeLookUp = DateTime.Now,
                    StockId = 2
                };

                //var price = priceHistoryService.GetStockPriceByDateTime(priceArguments);
                priceHistoryService.SimulatePriceChange(2, DateTime.Now);

             
        };
               
              /*  IEnumerable<Client> clientsToAdd = new List<Client>() { c1, c2 };
                db.Clients.AddRange(clientsToAdd);
                db.SaveChanges();
               var client = clientService.GetEntityByID(2);
                */
               
               // logger.Info("Trading is started");
               
              //  logger.Info("Trading is finished");
               
            }
        }
    }

