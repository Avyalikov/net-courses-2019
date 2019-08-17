using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimulator.Core.Services
{
    public class SaleService
    {
        private readonly ISupplierTableRepository supplierTableRepository;
        private readonly IGoodsTableRepository goodsTableRepository;
        private readonly ISoldGoodsTableRepository soldGoodsTableRepository;

        public SaleService(
            ISupplierTableRepository supplierTableRepository, 
            IGoodsTableRepository goodsTableRepository,
            ISoldGoodsTableRepository soldGoodsTableRepository
            )
        {
            this.supplierTableRepository = supplierTableRepository;
            this.goodsTableRepository = goodsTableRepository;
            this.soldGoodsTableRepository = soldGoodsTableRepository;
        }

        public void HandleBuy(BuyArguments args)
        {
            this.ValidateBuyArguments(args);

            this.StoreProductInSoldTable(args);
        }

        private void StoreProductInSoldTable(BuyArguments args)
        {
            var productsInStore = this.goodsTableRepository.FindProductsByRequest(args);

            foreach (var arg in args.ItemsToBuy)
            {
                var product = productsInStore.First(f => f.Name == arg.Name);
                 
                var productInSoldGoods = new ProductEntity()
                {
                    Id = product.Id,
                    Count = arg.Count,
                    PricePerItem = product.PricePerItem,
                    Name = product.Name,
                    SupplierId = product.SupplierId
                };

                this.soldGoodsTableRepository.Add(productInSoldGoods);
            }

            this.soldGoodsTableRepository.SaveChanges();
        }

        private void ValidateBuyArguments(BuyArguments args)
        {
            var productsInStore = this.goodsTableRepository.FindProductsByRequest(args);

            foreach (var arg in args.ItemsToBuy)
            {
                var product = productsInStore.First(f => f.Name == arg.Name);

                if (arg.Count > product.Count)
                {
                    throw new ArgumentException($"Can't handle this request, because products amount is not enough. Product with Name:{product.Name} has only {product.Count} items, but requested {arg.Count}.");
                }
            }
        }
    }
}
