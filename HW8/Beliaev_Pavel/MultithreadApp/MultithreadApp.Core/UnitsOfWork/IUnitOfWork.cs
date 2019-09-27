using MultithreadApp.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadApp.Core.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUrlRepository Urls { get; }
        void Save();
    }
}
