// <copyright file="StockModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Modifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;

    /// <summary>
    /// StockModifier description
    /// </summary>
    public class StockService
    {
        private ITableRepository tableRepository;
        private readonly IOnePKTableRepository onePKTableRepository;
        public StockService(ITableRepository tableRepository, IOnePKTableRepository onePKTableRepository)
        {
            this.tableRepository = tableRepository;
            this.onePKTableRepository = onePKTableRepository;
        }
        public void AddStock(StockInfo args)
        {
            var stock = new Stock() {StockPrefix=args.StockPrefix, IssuerID= args.IssuerId, StockType=(StockType)args.ShareType };
            tableRepository.Add(stock);
            tableRepository.SaveChanges();

        }
        public Stock GetStockByID(int id)
        {
            if (!this.onePKTableRepository.ContainsByID(id))
            {
                throw new ArgumentException("Stock doesn't exist");
            }
            return (Stock)this.onePKTableRepository.GetEntityByID(id);
        }

       /* public Stock GetRandomStock()
        {
            Random random = new Random();
           Stock stock = null;
            var stocks = db.Stocks.Select(o => o).ToList();
            if (stocks != null)
            {
                int number = random.Next(stocks.Count() - 1);
                stock = stocks[number];
            }
            return stock;
        }*/
    }
}
