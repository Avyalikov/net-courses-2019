using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Core.Models;

namespace Trading.TradesEmulator.Models
{
    public class TradesEmulatorDbInitializer : DropCreateDatabaseIfModelChanges<TradesEmulatorDbContext>
    {
        protected override void Seed(TradesEmulatorDbContext context)
        {
            var client1 = new ClientEntity { Name = "Aaron Wolf", Phone = "(812) 904 33 10", Balance = 10000M, RegistationDateTime = DateTime.Now};
            var client2 = new ClientEntity { Name = "Betany Stutoff", Phone = "(954) 404 13 11", Balance = 9030M, RegistationDateTime = DateTime.Now };
            var client3 = new ClientEntity { Name = "Anton Zaycev", Balance = 1076M, RegistationDateTime = DateTime.Now };
            var client4 = new ClientEntity { Name = "Paul Beat", Phone = "(383) 01 21 312 ", Balance = 51003M, RegistationDateTime = DateTime.Now };
            var client5 = new ClientEntity { Name = "Victor Powers", Balance = 19200M, RegistationDateTime = DateTime.Now };
            var client6 = new ClientEntity { Name = "Liam Piam", Phone = "(800) 2000 501", Balance = 22120M, RegistationDateTime = DateTime.Now };
            var client7 = new ClientEntity { Name = "Varian Wrynn", Balance = 20000M, RegistationDateTime = DateTime.Now };
            var client8 = new ClientEntity { Name = "Alexander Bely", Balance = 8100M, RegistationDateTime = DateTime.Now };

            context.Clients.Add(client1);
            context.Clients.Add(client2);
            context.Clients.Add(client3);
            context.Clients.Add(client4);
            context.Clients.Add(client5);
            context.Clients.Add(client6);
            context.Clients.Add(client7);
            context.Clients.Add(client8);

            var shares1 = new SharesEntity { SharesType = "Sbeerbank", Price = 836M };
            var shares2 = new SharesEntity { SharesType = "MMM", Price = 2874M };
            var shares3 = new SharesEntity { SharesType = "Motherland", Price = 282M };
            context.Shares.Add(shares1);
            context.Shares.Add(shares2);
            context.Shares.Add(shares3);

            var clientShares1 = new ClientSharesEntity { Shares = shares1, Quantity = 43, Client = client1 };
            var clientShares2 = new ClientSharesEntity { Shares = shares1, Quantity = 30, Client = client2 };
            var clientShares3 = new ClientSharesEntity { Shares = shares1, Quantity = 12, Client = client3 };
            var clientShares4 = new ClientSharesEntity { Shares = shares1, Quantity = 2, Client = client4 };
            var clientShares5 = new ClientSharesEntity { Shares = shares1, Quantity = 92, Client = client5 };
            var clientShares6 = new ClientSharesEntity { Shares = shares1, Quantity = 122, Client = client6 };
            var clientShares7 = new ClientSharesEntity { Shares = shares1, Quantity = 0, Client = client7 };
            var clientShares8 = new ClientSharesEntity { Shares = shares1, Quantity = 0, Client = client8 };
            var clientShares9 = new ClientSharesEntity { Shares = shares2, Quantity = 2, Client = client1 };
            var clientShares10 = new ClientSharesEntity { Shares = shares2, Quantity = 24, Client = client2 };
            var clientShares11 = new ClientSharesEntity { Shares = shares2, Quantity = 43, Client = client3 };
            var clientShares12 = new ClientSharesEntity { Shares = shares2, Quantity = 21, Client = client4 };
            var clientShares13 = new ClientSharesEntity { Shares = shares2, Quantity = 24, Client = client5 };
            var clientShares14 = new ClientSharesEntity { Shares = shares2, Quantity = 36, Client = client6 };
            var clientShares15 = new ClientSharesEntity { Shares = shares2, Quantity = 1, Client = client7 };
            var clientShares16 = new ClientSharesEntity { Shares = shares2, Quantity = 0, Client = client8 };
            var clientShares17 = new ClientSharesEntity { Shares = shares3, Quantity = 87, Client = client1 };
            var clientShares18 = new ClientSharesEntity { Shares = shares3, Quantity = 11, Client = client2 };
            var clientShares19 = new ClientSharesEntity { Shares = shares3, Quantity = 39, Client = client3 };
            var clientShares20 = new ClientSharesEntity { Shares = shares3, Quantity = 48, Client = client4 };
            var clientShares21 = new ClientSharesEntity { Shares = shares3, Quantity = 101, Client = client5 };
            var clientShares22 = new ClientSharesEntity { Shares = shares3, Quantity = 98, Client = client6 };
            var clientShares23 = new ClientSharesEntity { Shares = shares3, Quantity = 2, Client = client7 };
            var clientShares24 = new ClientSharesEntity { Shares = shares3, Quantity = 34, Client = client8 };

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

            context.SaveChanges();
        }
    }
}
