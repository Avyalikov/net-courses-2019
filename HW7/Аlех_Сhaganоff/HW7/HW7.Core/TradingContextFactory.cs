using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW7.Core
{
    #region snippet_IDbContextFactory
    public class TradingContextFactory : IDbContextFactory<TradingContext>
    {
        public TradingContext Create()
        {
            return new TradingContext("name=DefaultConnection");
        }
    }
    #endregion
}
