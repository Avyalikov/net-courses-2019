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
        private readonly ISaleHistoryTableRepository saleHistoryTableRepository;

        public SaleService(
            ISupplierTableRepository supplierTableRepository, 
            IGoodsTableRepository goodsTableRepository,
            ISoldGoodsTableRepository soldGoodsTableRepository,
            ISaleHistoryTableRepository saleHistoryTableRepository
            )
        {
            this.supplierTableRepository = supplierTableRepository;
            this.goodsTableRepository = goodsTableRepository;
            this.soldGoodsTableRepository = soldGoodsTableRepository;
            this.saleHistoryTableRepository = saleHistoryTableRepository;
        }

        public void HandleBuy(BuyArguments args)
        {
            this.ValidateBuyArguments(args);

            this.SubtractProductsInGoodsTable(args);
            this.StoreProductsInSoldTable(args);
            this.StoreProductsInSaleHistoryTable(args);
        }

        private void StoreProductsInSaleHistoryTable(BuyArguments args)
        {
            var productsInStore = this.goodsTableRepository.FindProductsByRequest(args);

            foreach (var arg in args.ItemsToBuy)
            {
                var product = productsInStore.First(f => f.Name == arg.Name);

                var productInSaleHistory = new SaleHistoryTableEntity()
                {
                    Id = product.Id,
                    Count = arg.Count,
                    PricePerItem = product.PricePerItem,
                    Name = product.Name,
                    SupplierId = product.SupplierId
                };

                this.saleHistoryTableRepository.Add(productInSaleHistory);
            }

            this.saleHistoryTableRepository.SaveChanges();
        }

        private void SubtractProductsInGoodsTable(BuyArguments args)
        {
            var productsInStore = this.goodsTableRepository.FindProductsByRequest(args);

            foreach (var arg in args.ItemsToBuy)
            {
                var product = productsInStore.First(f => f.Name == arg.Name);

                this.goodsTableRepository.SubtractProduct(product.Id, arg.Count);
            }

            this.goodsTableRepository.SaveChanges();
        }

        private void StoreProductsInSoldTable(BuyArguments args)
        {
            var productsInStore = this.goodsTableRepository.FindProductsByRequest(args);

            foreach (var arg in args.ItemsToBuy)
            {
                var product = productsInStore.First(f => f.Name == arg.Name);
                 
                var productInSoldGoods = new SoldGoodsTableEntity()
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
