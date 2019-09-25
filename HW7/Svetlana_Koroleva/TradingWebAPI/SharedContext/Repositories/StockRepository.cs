// <copyright file="StockTableRepository.cs" company="SKorol">
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
    using Trading.Core.DTO;

    /// <summary>
    /// StockTableRepository description
    /// </summary>
    public class StockRepository : CommonRepositoty<Stock>, IStockRepository
    {
        public StockRepository(ExchangeContext db) : base(db)
        {
        }


    }
}
