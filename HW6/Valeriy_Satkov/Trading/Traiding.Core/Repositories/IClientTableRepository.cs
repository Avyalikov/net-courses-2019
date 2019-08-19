using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IClientTableRepository
    {
        bool Contains(ClientEntity entity);
        bool ContainsById(int entityId);
        void Add(ClientEntity entity);
        void SaveChanges();
        ClientEntity Get(int clientId);
    }
}
