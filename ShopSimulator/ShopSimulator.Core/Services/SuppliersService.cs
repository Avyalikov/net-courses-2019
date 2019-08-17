using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Models;
using ShopSimulator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.Core.Services
{ 
    public class SuppliersService
    {
        private readonly ISupplierTableRepository supplierTableRepository;

        public SuppliersService(ISupplierTableRepository supplierTableRepository)
        {
            this.supplierTableRepository = supplierTableRepository;
        }

        public int RegisterNewSupplier(SupplierRegistrationInfo args)
        {
            var entityToAdd = new SupplierEntity()
            {
                CreatedAt = DateTime.Now,
                Name = args.Name,
                Surname = args.Surname,
                PhoneNumber = args.PhoneNumber
            };

            if (this.supplierTableRepository.Contains(entityToAdd))
            {
                throw new ArgumentException("This supplier has been registered. Can't continue");
            }

            this.supplierTableRepository.Add(entityToAdd);

            this.supplierTableRepository.SaveChanges();

            return entityToAdd.Id;
        }

        public SupplierEntity GetSupplier(int supplierId)
        {
            if (!this.supplierTableRepository.ContainsById(supplierId))
            {
                throw new ArgumentException("Can't get supplier by this Id. May it has not been registered.");
            }

            return this.supplierTableRepository.Get(supplierId);
        }
    }
}
