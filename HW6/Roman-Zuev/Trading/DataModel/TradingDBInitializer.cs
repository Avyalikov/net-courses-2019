namespace Trading.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class TradingDBInitializer : DropCreateDatabaseIfModelChanges<TradingDBContext>
    {
        protected override void Seed(TradingDBContext context)
        {
            var client1 = new Client { Name = "Aaron Wolf", PhoneNumber = "(812) 904 33 10", Balance = 10000M, Zone = "Free" };
            var client2 = new Client { Name = "Betany Stutoff", PhoneNumber = "(954) 404 13 11", Balance = 9030M, Zone = "Free" };
            var client3 = new Client { Name = "Anton Zaycev", Balance = 1076M, Zone = "Free" };
            var client4 = new Client { Name = "Paul Beat", PhoneNumber = "(383) 01 21 312 ", Balance = 51003M, Zone = "Free" };
            var client5 = new Client { Name = "Victor Powers", Balance = 19200M, Zone = "Free" };
            var client6 = new Client { Name = "Liam Piam", PhoneNumber = "(800) 2000 501", Balance = 22120M, Zone = "Free" };
            var client7 = new Client { Name = "Varian Wrynn", Balance = 20000M, Zone = "Free" };
            var client8 = new Client { Name = "Alexander Bely", Balance = 8100M, Zone = "Free" };

            context.Clients.Add(client1);
            context.Clients.Add(client2);
            context.Clients.Add(client3);
            context.Clients.Add(client4);
            context.Clients.Add(client5);
            context.Clients.Add(client6);
            context.Clients.Add(client7);
            context.Clients.Add(client8);

            var shares1 = new Shares { SharesType = "Sbeerbank", Price = 836M };
            var shares2 = new Shares { SharesType = "MMM", Price = 2874M };
            var shares3 = new Shares { SharesType = "Motherland", Price = 282M };
            context.Shares.Add(shares1);
            context.Shares.Add(shares2);
            context.Shares.Add(shares3);

            var clientShares1 = new ClientShares { Shares = shares1, Quantity = 43, Client = client1 };
            var clientShares2 = new ClientShares { Shares = shares1, Quantity = 30, Client = client2};
            var clientShares3 = new ClientShares { Shares = shares1, Quantity = 12, Client = client3};
            var clientShares4 = new ClientShares { Shares = shares1, Quantity = 2, Client = client4 };
            var clientShares5 = new ClientShares { Shares = shares1, Quantity = 92, Client = client5 };
            var clientShares6 = new ClientShares { Shares = shares1, Quantity = 122, Client = client6 };
            var clientShares7 = new ClientShares { Shares = shares1, Quantity = 0, Client = client7 };
            var clientShares8 = new ClientShares { Shares = shares1, Quantity = 0, Client = client8 };
            var clientShares9 = new ClientShares { Shares = shares2, Quantity = 2, Client = client1 };
            var clientShares10 = new ClientShares { Shares = shares2, Quantity = 24, Client = client2 };
            var clientShares11 = new ClientShares { Shares = shares2, Quantity = 43, Client = client3 };
            var clientShares12 = new ClientShares { Shares = shares2, Quantity = 21, Client = client4 };
            var clientShares13 = new ClientShares { Shares = shares2, Quantity = 24, Client = client5 };
            var clientShares14 = new ClientShares { Shares = shares2, Quantity = 36, Client = client6 };
            var clientShares15 = new ClientShares { Shares = shares2, Quantity = 1, Client = client7 };
            var clientShares16 = new ClientShares { Shares = shares2, Quantity = 0, Client = client8 };
            var clientShares17 = new ClientShares { Shares = shares3, Quantity = 87, Client = client1 };
            var clientShares18 = new ClientShares { Shares = shares3, Quantity = 11, Client = client2 };
            var clientShares19 = new ClientShares { Shares = shares3, Quantity = 39, Client = client3 };
            var clientShares20 = new ClientShares { Shares = shares3, Quantity = 48, Client = client4 };
            var clientShares21 = new ClientShares { Shares = shares3, Quantity = 101, Client = client5 };
            var clientShares22 = new ClientShares { Shares = shares3, Quantity = 98, Client = client6 };
            var clientShares23 = new ClientShares { Shares = shares3, Quantity = 2, Client = client7 };
            var clientShares24 = new ClientShares { Shares = shares3, Quantity = 34, Client = client8 };

            context.ClientShares.Add(clientShares1);
            context.ClientShares.Add(clientShares2);
            context.ClientShares.Add(clientShares3);
            context.ClientShares.Add(clientShares4);
            context.ClientShares.Add(clientShares5);
            context.ClientShares.Add(clientShares6);
            context.ClientShares.Add(clientShares7);
            context.ClientShares.Add(clientShares8);
            context.ClientShares.Add(clientShares9);
            context.ClientShares.Add(clientShares10);
            context.ClientShares.Add(clientShares11);
            context.ClientShares.Add(clientShares12);
            context.ClientShares.Add(clientShares13);
            context.ClientShares.Add(clientShares14);
            context.ClientShares.Add(clientShares15);
            context.ClientShares.Add(clientShares16);
            context.ClientShares.Add(clientShares17);
            context.ClientShares.Add(clientShares18);
            context.ClientShares.Add(clientShares19);
            context.ClientShares.Add(clientShares20);
            context.ClientShares.Add(clientShares21);
            context.ClientShares.Add(clientShares22);
            context.ClientShares.Add(clientShares23);
            context.ClientShares.Add(clientShares24);

            //var clientFund1 = new ClientFund { Client = client1, Balance = 10000M, Zone = "Free"};
            //var clientFund2 = new ClientFund { Client = client2, Balance = 9030M, Zone = "Free"};
            //var clientFund3 = new ClientFund { Client = client3, Balance = 51003M, Zone = "Free"};
            //var clientFund4 = new ClientFund { Client = client4, Balance = 1076M, Zone = "Free"};
            //var clientFund5 = new ClientFund { Client = client5, Balance = 19200M, Zone = "Free"};
            //var clientFund6 = new ClientFund { Client = client6, Balance = 22120M, Zone = "Free"};
            //var clientFund7 = new ClientFund { Client = client7, Balance = 20000M, Zone = "Free"};
            //var clientFund8 = new ClientFund { Client = client8, Balance = 8100M, Zone = "Free"};

            //context.ClientFunds.Add(clientFund1);
            //context.ClientFunds.Add(clientFund2);
            //context.ClientFunds.Add(clientFund3);
            //context.ClientFunds.Add(clientFund4);
            //context.ClientFunds.Add(clientFund5);
            //context.ClientFunds.Add(clientFund6);
            //context.ClientFunds.Add(clientFund7);
            //context.ClientFunds.Add(clientFund8);

            context.SaveChanges();
        }
    }
}