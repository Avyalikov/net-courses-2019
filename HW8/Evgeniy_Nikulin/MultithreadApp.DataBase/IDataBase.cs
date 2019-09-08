namespace MultithreadApp.DataBase
{
    using System;
    using Repository.Interface;

    public interface IDataBase
    {
        ILinksRepository Links { get; }
        void SaceChanges();
        void Connect();
        void Disconnect();
    }
}