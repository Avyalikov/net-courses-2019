using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using ShopSimulator.Core.Services;

namespace ShopSimulator.Core.Tests
{

    [TestClass]
    public class SuppliersServiceTests
    {
        [TestMethod]
        public void ShouldRegisterNewSupplier()
        {
            // Arrange
            var supplierTableRepository = Substitute.For<ISupplierTableRepository>();
            supplierTableRepository
               .When(w => w.WithTransaction(Arg.Any<Action>()))
               .Do((callback) =>
               {
                   callback.Arg<Action>().Invoke();
               });

            supplierTableRepository.WithTransaction<bool>(Arg.Any<Func<bool>>()).Returns((callback) =>
            {
                var result = callback.Arg<Func<bool>>().Invoke();

                return result;
            });

            SuppliersService suppliersService = new SuppliersService(supplierTableRepository);
            SupplierRegistrationInfo args = new SupplierRegistrationInfo();
            args.Name = "John";
            args.Surname = "Smith";
            args.PhoneNumber = "+73165465464";

            // Act
            var supplierId = suppliersService.RegisterNewSupplier(args);

            // Assert
            supplierTableRepository.Received(1).Add(Arg.Is<SupplierEntity>(
                w => w.Name == args.Name 
                && w.Surname == args.Surname 
                && w.PhoneNumber == args.PhoneNumber));
            supplierTableRepository.Received(1).SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "This supplier has been registered. Can't continue")]
        public void ShouldNotRegisterNewSupplierIfItExists()
        {
            // Arrange
            var supplierTableRepository = Substitute.For<ISupplierTableRepository>();
            SuppliersService suppliersService = new SuppliersService(supplierTableRepository);
            SupplierRegistrationInfo args = new SupplierRegistrationInfo();
            args.Name = "John";
            args.Surname = "Smith";
            args.PhoneNumber = "+73165465464";

            // Act
            suppliersService.RegisterNewSupplier(args);

            supplierTableRepository.Contains(Arg.Is<SupplierEntity>(
                w => w.Name == args.Name
                && w.Surname == args.Surname
                && w.PhoneNumber == args.PhoneNumber)).Returns(true);

            suppliersService.RegisterNewSupplier(args);
        }

        [TestMethod]
        public void ShouldGetSupplierInfo()
        {
            // Arrange
            var supplierTableRepository = Substitute.For<ISupplierTableRepository>();
            supplierTableRepository.ContainsById(Arg.Is(55)).Returns(true);
            SuppliersService suppliersService = new SuppliersService(supplierTableRepository);

            // Act
            var supplier = suppliersService.GetSupplier(55);

            // Assert
            supplierTableRepository.Received(1).Get(55);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Can't get supplier by this Id. May it has not been registered.")]
        public void ShouldThrowExceptionIfCantFindSupplier()
        {
            // Arrange
            var supplierTableRepository = Substitute.For<ISupplierTableRepository>();
            supplierTableRepository.ContainsById(Arg.Is(55)).Returns(false);
            SuppliersService suppliersService = new SuppliersService(supplierTableRepository);

            // Act
            var supplier = suppliersService.GetSupplier(55);
             
        }
    }

    
}
