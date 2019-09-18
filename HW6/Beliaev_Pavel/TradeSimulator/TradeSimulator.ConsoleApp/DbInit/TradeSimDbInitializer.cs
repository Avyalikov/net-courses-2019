using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeSimulator.Core.Models;

namespace TradeSimulator.ConsoleApp.DbInit
{   

    public class TradeSimDbInitializer : System.Data.Entity.DropCreateDatabaseAlways<TradeSimDbContext>
    {
        public override void InitializeDatabase(TradeSimDbContext context)
        {
            SqlConnection.ClearAllPools();
            base.InitializeDatabase(context);
        }

        protected override void Seed(TradeSimDbContext context)
        {
            base.Seed(context);
            
            var stockPrices = new List<StockPriceEntity>
            {
                new StockPriceEntity{TypeOfStock = "SBER", PriceOfStock=100m},
                new StockPriceEntity{TypeOfStock = "BER", PriceOfStock=90m},
                new StockPriceEntity{TypeOfStock = "REN", PriceOfStock=80m},
                new StockPriceEntity{TypeOfStock = "RAIF", PriceOfStock=70m}
            };
            stockPrices.ForEach(p => context.StockPrice.Add(p));
            context.SaveChanges();

            var clients = new List<ClientEntity>
            {
                new ClientEntity{Name="Petr", Surname="Ivanov", PhoneNumber="1234567"},
                new ClientEntity{Name="Ivan", Surname="Petrov", PhoneNumber="1234577"},
                new ClientEntity{Name="Pavel", Surname="Vasiliev", PhoneNumber="1234587"},
                new ClientEntity{Name="Bob", Surname="Abramov", PhoneNumber="1234597"},
                new ClientEntity{Name="Ivan", Surname="Deripasov", PhoneNumber="1234561"},
                new ClientEntity{Name="Ivan", Surname="Grevov", PhoneNumber="1234562"},
                new ClientEntity{Name="Ivan", Surname="Sidorov", PhoneNumber="1234563"},
                new ClientEntity{Name="Michael", Surname="Smit", PhoneNumber="1234564"},
                new ClientEntity{Name="Paul", Surname="Shon", PhoneNumber="1234565"},
                new ClientEntity{Name="Jack", Surname="Li", PhoneNumber="1234566"}
            };
            clients.ForEach(c => context.Client.Add(c));
            context.SaveChanges();

            var accounts = new List<AccountEntity>
            {
                new AccountEntity{ClientId = clients.ElementAt(0).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(1).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(2).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(3).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(4).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(5).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(6).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(7).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(8).Id, Balance=1000m, Zone = "white"},
                new AccountEntity{ClientId = clients.ElementAt(9).Id, Balance=1000m, Zone = "white"}
            };
            accounts.ForEach(c => context.Account.Add(c));
            context.SaveChanges();
            
            var clientStocks = new List<StockOfClientEntity>();
            for (int i = 0; i < accounts.Count; i++)
            {
                clientStocks.Add(new StockOfClientEntity { TypeOfStocks = "SBER", quantityOfStocks = 10, AccountForStock = accounts[i]});
                clientStocks.Add(new StockOfClientEntity { TypeOfStocks = "BER", quantityOfStocks = 10, AccountForStock = accounts[i]});
                clientStocks.Add(new StockOfClientEntity { TypeOfStocks = "REN", quantityOfStocks = 10, AccountForStock = accounts[i]});
                clientStocks.Add(new StockOfClientEntity { TypeOfStocks = "RAIF", quantityOfStocks = 10, AccountForStock = accounts[i]});

                clientStocks.ForEach(c => context.StockOfClient.Add(c));
                context.SaveChanges();

                clientStocks.RemoveAll(r=>r.quantityOfStocks == 10);
            }
        }
    }
}
