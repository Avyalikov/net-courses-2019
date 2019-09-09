// <copyright file="StockExchangeInitializer.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace TradingApp.DAL
{
   
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;

    /// <summary>
    /// StockExchangeInitializer description
    /// </summary>
    public class StockExchangeInitializer: System.Data.Entity.CreateDatabaseIfNotExists<ExchangeContext>
    {
        protected override void Seed(ExchangeContext context)
        {
            base.Seed(context);
            var issuers = new List<Issuer>
            {
                new Issuer{CompanyName="PJSC Sberbank", Address="Moscow"},
                new Issuer {CompanyName = "PJSC Rosneft", Address = "Moscow" }
        };
            issuers.ForEach(i => context.Issuers.Add(i));
            context.SaveChanges();

            var stocks = new List<Stock>
            {
                new Stock{IssuerID=1, StockType= StockType.Common, StockPrefix ="SBER"},
                new Stock{IssuerID=1, StockType= StockType.Preference, StockPrefix ="SBERP"},
                new Stock{IssuerID=2, StockType= StockType.Common, StockPrefix ="ROSN"}
            };
            stocks.ForEach(s=> context.Stocks.Add(s));
            context.SaveChanges();

            var priceHistories = new List<PriceHistory>
            {
                new PriceHistory{StockID=1, DateTimeBegin=DateTime.Now, DateTimeEnd=DateTime.Today.AddYears(200), Price=220},
                new PriceHistory{StockID=2, DateTimeBegin=DateTime.Now, DateTimeEnd=DateTime.Today.AddYears(200), Price=194},
                new PriceHistory{StockID=3, DateTimeBegin=DateTime.Now, DateTimeEnd=DateTime.Today.AddYears(200), Price=405}
            };
            priceHistories.ForEach(p => context.PriceHistories.Add(p));
            context.SaveChanges();

            var clients = new List<Client>
            {
                new Client{LastName="Petr", FirstName="Ivanov", Phone="1234567", Balance=10000, RegistrationDateTime=DateTime.Now },
                new Client{LastName="Ivan", FirstName="Petrov", Phone="1234577", Balance=10000, RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Pavel", FirstName="Vasiliev", Phone="1234587", Balance=10000, RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Bob", FirstName="Abramov", Phone="1234597", Balance=10000,RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Ivan", FirstName="Deripasov", Phone="1234561", Balance=10000, RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Ivan", FirstName="Grevov", Phone="1234562", Balance=10000, RegistrationDateTime=DateTime.Now },
                new Client{LastName="Ivan", FirstName="Sidorov", Phone="1234563", Balance=10000, RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Michael", FirstName="Smit", Phone="1234564", Balance=10000, RegistrationDateTime=DateTime.Now },
                new Client{LastName="Paul", FirstName="Shon", Phone="1234565", Balance=10000, RegistrationDateTime=DateTime.Now  },
                new Client{LastName="Jack", FirstName="Li", Phone="1234566", Balance=10000, RegistrationDateTime=DateTime.Now }
            };

            clients.ForEach(c => context.Clients.Add(c));
            context.SaveChanges();

            var clientStocks = new List<ClientStock>
            {
                new ClientStock{ClientID=1, StockID=1, Quantity=100},
                new ClientStock{ClientID=1, StockID=2, Quantity=100},
                new ClientStock{ClientID=1, StockID=3, Quantity=100},
                new ClientStock{ClientID=2, StockID=1, Quantity=100},
                new ClientStock{ClientID=2, StockID=3, Quantity=100},
                new ClientStock{ClientID=3, StockID=1, Quantity=100},
                new ClientStock{ClientID=4, StockID=2, Quantity=100},
                new ClientStock{ClientID=5, StockID=3, Quantity=100},
                new ClientStock{ClientID=6, StockID=1, Quantity=100},
                new ClientStock{ClientID=7, StockID=2, Quantity=100},
                new ClientStock{ClientID=8, StockID=3, Quantity=100},
                new ClientStock{ClientID=9, StockID=1, Quantity=100}
            };
            clientStocks.ForEach(c => context.ClientStocks.Add(c));
            context.SaveChanges();

         

        }
    }
}
