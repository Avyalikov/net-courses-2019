namespace MultithreadApp.DataBase.Repository.Interface
{
    using System;
    using Repository.Core;
    using Model;

    public interface ILinksRepository : IRepository<Links>
    {
        bool IsExist(string link);
    }
}