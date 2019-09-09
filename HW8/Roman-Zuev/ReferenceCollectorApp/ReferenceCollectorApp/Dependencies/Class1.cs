using ReferenceCollectorApp.Context;
using ReferenceCollectorApp.Repositories;
using ReferenceCollectorApp.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceCollectorApp.Dependencies
{
    public class ReferenceCollectorRegistry : Registry
    {
        public ReferenceCollectorRegistry()
        {
            this.For<IReferenceCollectorService>().Use<ReferenceCollectorService>();
            this.For<IReferenceTable>().Use<ReferencesTable>();
            this.For<ReferenceCollectorDbContext>().Use<ReferenceCollectorDbContext>();
            this.For<IReferenceCollector>().Use<ReferenceCollector>();
        }
    }
}
