// <copyright file="StockModifier.cs" company="SKorol">
// Copyright (c) SKorol. All rights reserved.
// </copyright>

namespace Trading.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Trading.Core.Model;
    using Trading.Core.Repositories;
    using Trading.Core.DTO;
    using Trading.Core.IServices;

    /// <summary>
    /// StockModifier description
    /// </summary>
    public class StockService:IStockService
    {
        private IUnitOfWork unitOfWork;
      
        public StockService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
          
        }
        public void AddStock(StockInfo args)
        {
            var stockToAdd = new Stock()
            {
                StockPrefix =args.StockPrefix,
                Issuer = args.Issuer,
                StockType =args.ShareType,
                Price=args.Price
                
            };
            if (this.unitOfWork.Stocks.Get(s=>s.StockPrefix==args.StockPrefix&&s.Issuer==args.Issuer&&s.StockType==args.ShareType).Count()!=0)
            {
                throw new ArgumentException("This stock exists. Can't continue");
            };
           unitOfWork.Stocks.Add(stockToAdd);
            unitOfWork.Save();

        }

        public void Delete(int stockId)
        {
            var stockToDelete = this.GetEntityByID(stockId);
            this.unitOfWork.Stocks.Delete(stockToDelete);
            this.unitOfWork.Save();
        }

        public Stock GetEntityByID(int id)
        {
            if (this.unitOfWork.Stocks.Get(s=>s.StockID==id).Count()==0)
            {
                throw new ArgumentException("Stock doesn't exist");
            }
            return this.unitOfWork.Stocks.Get(s => s.StockID == id).Single();
        }

        public void Update( int stockId, StockInfo args)
        {
            var stockToUpdate = this.GetEntityByID(stockId);
            stockToUpdate.Price = args.Price;
            this.unitOfWork.Save(); 
        }
    }
}
