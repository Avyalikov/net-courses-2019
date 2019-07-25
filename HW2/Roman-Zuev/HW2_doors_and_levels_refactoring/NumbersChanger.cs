using System;
using System.Collections.Generic;

namespace HW2_doors_and_levels_refactoring
{
    public class NumbersChanger : INumbersChanger
    {
        private readonly GameSettings gameSettings;
        private IInputOutputDevice ioDevice;
        private IPhraseProvider phraseProvider;

        public NumbersChanger(IInputOutputDevice ioDevice, IPhraseProvider phraseProvider, ISettingsProvider settingsProvider)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.ioDevice = ioDevice;
            this.phraseProvider = phraseProvider;
        }

        public int [] ChangeNumbers(int [] NumbersToChange, string ChangeValue, Stack<int> usernums)
        {
            int Temp;
            // validates entered string is a number
            if (!Int32.TryParse(ChangeValue, out Temp))
            {
                ioDevice.Print(phraseProvider.GetPhrase("YouHaveEnteredWrongValue"));
                return NumbersToChange;
            }
            // goint to previous level
            if (Temp == gameSettings.PreviousLevelNumber)
            {
                if (usernums.Count > 0)
                {
                    int divider = usernums.Pop();
                    for (int i = 0; i < NumbersToChange.Length; i++)
                    {
                        NumbersToChange[i] /= divider;
                    }
                }
                if (usernums.Count>0) ioDevice.Print(phraseProvider.GetPhrase("PreviousLevel"));
                return NumbersToChange;
            }
            //validating entered number
            foreach (int number in NumbersToChange)
            {
                //if value contains in the array -> multiply array numbers
                if (number == Temp)
                { 
                    if(gameSettings.MaxLevel == usernums.Count) // MaxLevelCheck
                    {
                        ioDevice.Print(phraseProvider.GetPhrase("ReachedMaxLevel"));
                        return NumbersToChange;
                    }
                    for (int i = 0; i < NumbersToChange.Length; i++)
                    {
                        //if (NumbersToChange[i] != gameSettings.PreviousLevelNumber)
                        //{
                            NumbersToChange[i] *= Temp;
                        //}
                    }
                    usernums.Push(Temp); // adding value to the stack
                    ioDevice.Print(phraseProvider.GetPhrase("NextLevel"));
                    return NumbersToChange;
                }
            }
            //if array doesn't contain entered number
            ioDevice.Print(phraseProvider.GetPhrase("YouHaveEnteredWrongValuePleaseEnterNumbersListed"));
            return NumbersToChange;
        }
    }
}