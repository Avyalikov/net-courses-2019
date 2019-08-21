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
namespace TradingApp
{
    class Program
    {
        

private static ExchangeContext db = new ExchangeContext();
        //private static ITableRepository repository=new ClientTableRepository(db);
        private static IOnePKTableRepository repositorypart2 = new ClientTableRepository(db);

        static void Main(string[] args)
        {
            //log4net.Config.XmlConfigurator.Configure();
           // var logger = new Logger(log4net.LogManager.GetLogger("Logger"));




            using (db)
            {
                ClientService clientService = new ClientService( repositorypart2);
                clientService.AddClient(new ClientInfo()
                {
                    FirstName = "Fedor",
                    LastName = "Iv",
                    Phone = "1234567",
                    Balance = 1000,

                }

                
                );
              
               var client = clientService.GetEntityByID(2);
                
               
               // logger.Info("Trading is started");
               
              //  logger.Info("Trading is finished");
               
            }
        }
    }
}
