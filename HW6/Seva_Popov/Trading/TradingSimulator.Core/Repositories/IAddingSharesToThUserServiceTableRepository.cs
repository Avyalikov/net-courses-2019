using System;
using System.Collections.Generic;
using System.Text;
using TradingSimulator.Core.Models;

namespace TradingSimulator.Core.Repositories
{
    public interface IAddingSharesToThUserServiceTableRepository
    {
        void Add(AddingSharesToThUserEntity addingSharesToThUserEntity);
        void SaveChanges();
    }
}
