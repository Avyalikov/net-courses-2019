namespace WikipediaParser
{
    using System;
    using WikipediaParser.Repositories;

    public interface IUnitOfWork : IDisposable
    {
        ILinksTableRepository LinksTableRepository { get; }
        void Dispose(bool disposing);
    }
}