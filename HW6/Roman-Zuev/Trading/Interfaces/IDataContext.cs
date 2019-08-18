using System;
using System.Linq;

namespace Trading.DataModel
{
    internal interface IDataContext : IDisposable
    {
        IQueryable<Client> Clients();
        IQueryable<ClientShares> ClientShares();
        IQueryable<Shares> Shares();
    }
}