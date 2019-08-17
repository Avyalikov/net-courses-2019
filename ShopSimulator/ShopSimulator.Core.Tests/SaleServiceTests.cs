using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using ShopSimulator.Core.Services;

namespace ShopSimulator.Core.Tests
{
    [TestClass]
    public class SaleServiceTests
    {
        ISupplierTableRepository supplierTableRepository;
        IGoodsTableRepository goodsTableRepository;
        ISoldGoodsTableRepository soldGoodsTableRepository;
        ISaleHistoryTableRepository saleHistoryTableRepository;
        List<ProductEntity> goodsTableData;

        [TestInitialize]
        public void Initialize()
        {
            supplierTableRepository = Substitute.For<ISupplierTableRepository>();
            supplierTableRepository.Get(5).Returns(new SupplierEntity()
            {
                Id = 5,
                Name = "John",
                Surname = "Smith"
            });
            supplierTableRepository.Get(40).Returns(new SupplierEntity()
            {
                Id = 40,
                Name = "Mark",
                Surname = "Deadman"
            });

            goodsTableRepository = Substitute.For<IGoodsTableRepository>();
            goodsTableRepository.FindProductsByRequest(Arg.Any<BuyArguments>())
                .Returns((callInfo) =>
                {
                    var buyArguments = callInfo.Arg<BuyArguments>();

                    var retVal = new List<ProductEntity>();

                    foreach (var itemToBuy in buyArguments.ItemsToBuy)
                    {
                        var product = this.goodsTableData.First(f => f.Name == itemToBuy.Name);

                        retVal.Add(product);
                    }

                    return retVal;
                });

            this.goodsTableData = new List<ProductEntity>()
            {
                new ProductEntity()
                {
                    Id = 1,
                    SupplierId = 5,
                    Name = "John's Product A",
                    Count = 4,
                    PricePerItem = 12.5m
                },
                new ProductEntity()
                {
                    Id = 2,
                    SupplierId = 5,
                    Name = "John's Product B",
                    Count = 3,
                    PricePerItem = 12.5m
                },
                new ProductEntity()
                {
                    Id = 3,
                    SupplierId = 40,
                    Name = "Mark's Product B",
                    Count = 3,
                    PricePerItem = 12.5m
                },
                new ProductEntity()
                {
                    Id = 4,
                    SupplierId = 40,
                    Name = "Pen with Blue color",
                    Count = 3,
                    PricePerItem = 12.5m
                },
                new ProductEntity()
                {
                    Id = 5,
                    SupplierId = 5,
                    Name = "Pen with Blue color",
                    Count = 3,
                    PricePerItem = 12.5m
                }
            };

            this.soldGoodsTableRepository = Substitute.For<ISoldGoodsTableRepository>();
            this.saleHistoryTableRepository = Substitute.For<ISaleHistoryTableRepository>();
        }

        [TestMethod]
        public void ShouldPopulateSoldGoodsTableOnceWeHaveSaleActivity()
        {
            // Arrange
            SaleService saleHandler = new SaleService(
                this.supplierTableRepository, 
                this.goodsTableRepository,
                this.soldGoodsTableRepository,
                this.saleHistoryTableRepository);

            var args = new BuyArguments();
            args.ItemsToBuy = new List<ItemToBuy>()
            {
                new ItemToBuy()
                {
                    Name = "John's Product A",
                    Count = 2
                },
                new ItemToBuy()
                {
                    Name = "Mark's Product B",
                    Count = 2
                }
            };
      
            // Act
            saleHandler.HandleBuy(args);

            // Assert
            this.soldGoodsTableRepository.Received(1).Add(Arg.Is<SoldGoodsTableEntity>(
                 w => w.Name == "John's Product A" && w.Count == 2));
            this.soldGoodsTableRepository.Received(1).Add(Arg.Is<SoldGoodsTableEntity>(
                w => w.Name == "Mark's Product B" && w.Count == 2));

            this.soldGoodsTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't handle this request, because products amount is not enough. Product with Name:John's Product A has only 4 items, but requested 2000.")]
        public void ShouldThrowExceptionIfProductAmountsIsNotEnough()
        {
            // Arrange
            SaleService saleHandler = new SaleService(
                this.supplierTableRepository,
                this.goodsTableRepository,
                this.soldGoodsTableRepository,
                this.saleHistoryTableRepository);

            var args = new BuyArguments();
            args.ItemsToBuy = new List<ItemToBuy>()
            {
                new ItemToBuy()
                {
                    Name = "John's Product A",
                    Count = 2000
                }
            };

            // Act
            saleHandler.HandleBuy(args);
        }

        [TestMethod]
        public void ShouldSubtractFromGoodsTableOnceWeHaveSaleActivity()
        {
            // Arrange
            SaleService saleHandler = new SaleService(
                this.supplierTableRepository,
                this.goodsTableRepository,
                this.soldGoodsTableRepository,
                this.saleHistoryTableRepository);

            var args = new BuyArguments();
            args.ItemsToBuy = new List<ItemToBuy>()
            {
                new ItemToBuy()
                {
                    Name = "John's Product A",
                    Count = 2
                },
                new ItemToBuy()
                {
                    Name = "Mark's Product B",
                    Count = 2
                }
            };

            // Act
            saleHandler.HandleBuy(args);

            // Assert
            foreach (var item in args.ItemsToBuy)
            {
                this.goodsTableRepository.Received(1).SubtractProduct(this.goodsTableData.First(f => f.Name == item.Name).Id, item.Count);
            }

            this.goodsTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        public void ShouldPopulateSaleHistoryTableOnceWeHaveSaleActivity()
        {
            // Arrange
            SaleService saleHandler = new SaleService(
                this.supplierTableRepository,
                this.goodsTableRepository,
                this.soldGoodsTableRepository,
                this.saleHistoryTableRepository);

            var args = new BuyArguments();
            args.ItemsToBuy = new List<ItemToBuy>()
            {
                new ItemToBuy()
                {
                    Name = "John's Product A",
                    Count = 2
                },
                new ItemToBuy()
                {
                    Name = "Mark's Product B",
                    Count = 2
                }
            };

            // Act
            saleHandler.HandleBuy(args);

            // Assert
            foreach (var item in args.ItemsToBuy)
            {
                this.saleHistoryTableRepository.Received(1).Add(Arg.Is<SaleHistoryTableEntity>(w=>w.Name == item.Name && w.Count == item.Count));
            }

            this.saleHistoryTableRepository.Received(1).SaveChanges();
        }
    }

    
}
