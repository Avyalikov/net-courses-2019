namespace MultithreadLinksApp.Core.Repos
{
    using System;
    public interface IRepository
    {
        void SaveChanges();
        void WithTransaction(Action action);
    }
}
