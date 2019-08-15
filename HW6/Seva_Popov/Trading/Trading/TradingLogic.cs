using System;
using System.Collections.Generic;
using System.Text;
using Trading.Interfaces;

namespace Trading
{
    class TradingLogic : ITradingLogic
    {
        private readonly IPhraseProvider phraseProvider;
        private readonly IOutputData outputData;
        private readonly IInputData inputData;
        private readonly Settings settings;

        public TradingLogic(
            IPhraseProvider phraseProvider,
            IOutputData outputData,
            IInputData inputData,
            Settings settings)
        {
            this.outputData = outputData;
            this.inputData = inputData;
            this.phraseProvider = phraseProvider;
            this.settings = settings;
        }

        public void Run()
        {
           
        }
    }
}
