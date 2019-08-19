using stockSimulator.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace stockSimulator.Core.Repositories
{
    public interface IClientTableRepository
    {
        void Add(ClientEntity entity);
        void SaveChanges();
    }
}
