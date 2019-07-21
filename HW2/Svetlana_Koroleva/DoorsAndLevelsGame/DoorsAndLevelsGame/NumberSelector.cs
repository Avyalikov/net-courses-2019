using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevelsGame
{
    public class NumberSelector: INumberSelector
    {      

        public int EnteringNumber()
        {
            IInputOutputComponent ioComp = new ConsoleIOComponent();
            IPhraseProvider phraseProvider = new JSONPhraseProvider();
            bool isNumber = false;
            int enteredNum;
            do
            {
                if (int.TryParse(Console.ReadLine(), out enteredNum))
                {

                    ioComp.WriteOutput(phraseProvider.GetPhrase("Selected") + $"{enteredNum}");
                    isNumber = true;

                }

                else
                {
                    ioComp.WriteOutput(phraseProvider.GetPhrase("NotNumber"));


                }
            }

            while (!isNumber);
            return enteredNum;
        }
    }
}
