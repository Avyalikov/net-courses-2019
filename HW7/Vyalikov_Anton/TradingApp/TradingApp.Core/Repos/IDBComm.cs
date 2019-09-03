namespace TradingApp.Core.Repos
{
    using System;
    public interface IDBComm
    {
        void SaveChanges();
        void WithTransaction(Action func);
    }
}
