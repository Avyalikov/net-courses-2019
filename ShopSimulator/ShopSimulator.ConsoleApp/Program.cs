using ShopSimulator.ConsoleApp.DependencyInjection;
using ShopSimulator.Core.Dto;
using ShopSimulator.Core.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(new ShopSimulatorRegistry());

            var supplierService = container.GetInstance<SuppliersService>();

            supplierService.RegisterNewSupplier(new SupplierRegistrationInfo()
            {
                Name = "Denis",
                Surname = "Chesnokov",
                PhoneNumber = "12345"
            });
        }
    }
}
