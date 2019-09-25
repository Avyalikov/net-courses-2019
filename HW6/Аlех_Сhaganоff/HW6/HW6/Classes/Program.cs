using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace HW6.Classes
{
    public class Program
    {
        public bool SimulationIsWorking = false;

        public IDataInteraction dataInteraction;
        public ISimulation simulation;
        public IOutputProvider outputProvider;
        public IContextProvider contextProvider;
        public ILogger logger;

        public Program
            (IDataInteraction dataInteraction, 
            ISimulation simulation, 
            IOutputProvider outputProvider, 
            IContextProvider contextProvider, 
            ILogger logger)
        {
            this.dataInteraction = dataInteraction;
            this.simulation = simulation;
            this.outputProvider = outputProvider;
            this.contextProvider = contextProvider;
            this.dataInteraction = dataInteraction;
            this.logger = logger;
        }
    }
}
