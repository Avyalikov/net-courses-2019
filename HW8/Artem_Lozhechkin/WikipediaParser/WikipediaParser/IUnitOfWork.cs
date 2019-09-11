using System;
using WikipediaParser.Repositories;

namespace WikipediaParser
{
    public interface IUnitOfWork : IDisposable
    {
        ILinksTableRepository LinksTableRepository { get; }
        void Dispose(bool disposing);
    }
}