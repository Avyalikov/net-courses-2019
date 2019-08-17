using ShopSimulator.ConsoleApp.Repositories;
using ShopSimulator.Core.Repositories;
using ShopSimulator.Core.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopSimulator.ConsoleApp.DependencyInjection
{
    public class ShopSimulatorRegistry : Registry
    {
        public ShopSimulatorRegistry()
        {
            this.For<ISupplierTableRepository>().Use<SupplierTableRepository>();
            this.For<ISoldGoodsTableRepository>().Use<SoldGoodsTableRepository>();
            this.For<ISaleHistoryTableRepository>().Use<SaleHistoryTableRepository>();
            this.For<IGoodsTableRepository>().Use<GoodsTableRepository>();
            this.For<ShopSimulatorDbContext>().Use<ShopSimulatorDbContext>();
            this.For<SaleService>().Use<SaleService>();
            this.For<SuppliersService>().Use<SuppliersService>();
            this.For<ShopSimulatorDbContext>().Use<ShopSimulatorDbContext>().Ctor<string>("connectionString").Is(ConfigurationManager.ConnectionStrings["shopSimulatorConnectionString"].ConnectionString);
        }
    }
}
