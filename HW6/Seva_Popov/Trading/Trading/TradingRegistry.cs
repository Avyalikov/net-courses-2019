using StructureMap;
using System;
using System.Collections.Generic;
using System.Text;
using Trading.Components;
using Trading.Data;
using Trading.Interfaces;

namespace Trading
{
   public class TradingRegistry : Registry
    {
        public TradingRegistry()
        {
            this.For<IInputData>().Use<InputData>();
            this.For<IOutputData>().Use<OutputData>();
            this.For<IPhraseProvider>().Use<PhraseProvider>();
            this.For<ISettingsProvider>().Use<SettingsProvider>();
            this.For<ITradingLogic>().Use<TradingLogic>();
            this.For<Settings>().Use(context => context.GetInstance<ISettingsProvider>().GetSettings());
            this.For<IApplicationContext>().Use<ApplicationContext>();        
        }
    }
}
