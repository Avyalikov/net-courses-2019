using HW6.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW6.Classes
{
    public class Program
    {
        public bool SimulationIsWorking = false;

        public ISimulation simulation;
        public IOutputProvider outputProvider;

        public Program(ISimulation simulation, IOutputProvider outputProvider)
        {
            this.simulation = simulation;
            this.outputProvider = outputProvider;
        }
    }
}
