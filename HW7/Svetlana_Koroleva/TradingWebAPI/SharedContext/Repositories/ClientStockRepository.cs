// <copyright file="ClientStockTableRepository.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace SharedContext.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using SharedContext.DAL;

    /// <summary>
    /// ClientStockTableRepository description
    /// </summary>
   public class ClientStockRepository : CommonRepositoty<ClientStock>, IClientStockRepository
    {
        
        public ClientStockRepository(ExchangeContext db):base(db)
        {
            
        }

      
  
    }
}
