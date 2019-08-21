﻿// <copyright file="StockModifier.cs" company="SKorol">
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
      
        public StockService(ITableRepository tableRepository)
        {
            this.tableRepository = tableRepository;
          
        }
        public void AddStock(StockInfo args)
        {
            var stockToAdd = new Stock()
            {
                StockPrefix =args.StockPrefix,
                IssuerID = args.IssuerId,
                StockType =(StockType)args.ShareType
            };
            if (this.tableRepository.Contains(stockToAdd))
            {
                throw new ArgumentException("This stock exists. Can't continue");
            };
            tableRepository.Add(stockToAdd);
            tableRepository.SaveChanges();

        }
        public Stock GetStockByID(int id)
        {
            if (!this.tableRepository.ContainsByPK(id))
            {
                throw new ArgumentException("Stock doesn't exist");
            }
            return (Stock)this.tableRepository.Find(id);
        }

        public Stock GetRandomStock()
        {
            Random random = new Random();
            int stocksAmount = this.tableRepository.Count();
            if (stocksAmount == 0)
            {
                throw new NullReferenceException("There are no stocks to select from");
            }
            int number = random.Next(1, stocksAmount);
            Stock stock = (Stock)tableRepository.GetElementAt(number);

            return stock;
        }

        
       
    }
}