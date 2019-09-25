// <copyright file="ExchangeContextFactory.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace SharedContext.DAL
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// ExchangeContextFactory description
    /// </summary>
    public class ExchangeContextFactory : IDbContextFactory<ExchangeContext>
    {
        public ExchangeContext Create()
        {
           // return new ExchangeContext("");
            return new ExchangeContext(connection);
        }
    }

   

        
    }
}
