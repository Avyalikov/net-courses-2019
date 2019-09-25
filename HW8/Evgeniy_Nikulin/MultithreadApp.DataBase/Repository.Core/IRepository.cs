namespace MultithreadApp.DataBase.Repository.Core
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<TEntity> : IDisposable
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
    }
}