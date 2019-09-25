using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;

namespace TradingAppWebAPI
{
    public class DependencyResolver : IDependencyResolver
    {
        private IContainer container;
        public DependencyResolver()
        {
            container = new Container(new AppRegistry());
        }

        public IDependencyScope BeginScope()
        {
            return new StructureMapDependencyScope(container.GetNestedContainer());
        }

        public void Dispose()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface
                     ? container.TryGetInstance(serviceType)
                     : container.GetInstance(serviceType);
        }

    }

    public class StructureMapDependencyScope : IDependencyScope
    {
        private IContainer container;

        public StructureMapDependencyScope(IContainer container)
        {
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsAbstract || serviceType.IsInterface
                     ? container.TryGetInstance(serviceType)
                     : container.GetInstance(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetAllInstances(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }
        }
    }
}
