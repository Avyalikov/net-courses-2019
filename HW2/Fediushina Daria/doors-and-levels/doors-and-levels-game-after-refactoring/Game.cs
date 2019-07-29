using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace doors_and_levels_game_after_refactoring
{
    class Game
    {

        private readonly ISettingsProvider settingsProvider;
        private readonly IDeviceInOut ioDevice;
        private readonly IPhraseProvider phraseProvider;        
        private readonly INumbersArrayGenerator doorsNumbersGenerator;

        private readonly GameSettings gameSettings;
        

        private int[] doorsNumbers;
        private List<int> userNumbers;
        public Game(ISettingsProvider settingsProvider, IDeviceInOut ioDevice, IPhraseProvider phraseProvider, INumbersArrayGenerator doorsNumbersGenerator)
        {
            this.gameSettings = settingsProvider.GetGameSettings();
            this.phraseProvider = phraseProvider;
            this.ioDevice = ioDevice;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
        }

        public void Run()
        {
            doorsNumbers = doorsNumbersGenerator.GenerateIntArr(gameSettings.DoorsAmount, gameSettings.NumRange); // array of doors numbers for the start
            userNumbers = new List<int> { 1 };

            ioDevice.WriteOutput(phraseProvider.getPhrase("Welcome"));
            var stop = true;
            while (stop)
            {
                ioDevice.WriteOutput(phraseProvider.getPhrase("Doors"));
                string result = string.Join(" ", doorsNumbers);
                ioDevice.WriteOutput(result);
                ioDevice.WriteOutput(phraseProvider.getPhrase("SelectDoor"));
                string userInput = ioDevice.ReadOutput();

                Boolean isSuccsess = int.TryParse(userInput, out int t);
                if (isSuccsess)
                {
                    if (Array.IndexOf(doorsNumbers, t) != -1)   /*if it is a valid number*/
                    {
                        for (int i = 0; i < doorsNumbers.Length; i++)
                        {
                            var temp = doorsNumbers[i];
                            doorsNumbers[i] *= t;

                            if (doorsNumbers[i] < temp) //if at least one number is to big
                            {
                                ioDevice.WriteOutput(phraseProvider.getPhrase("GameOver"));
                                stop=false;
                                break;
                            }
                        }
                        userNumbers.Add(t);
                    }
                    else if (t == gameSettings.BackDoor)        /*if user put 0 */
                    {
                        if (userNumbers.Count != 0)
                        {
                            for (int i = 0; i < doorsNumbers.Length; i++)
                            {
                                int divNum = userNumbers.LastOrDefault();                                
                                try
                                {
                                    doorsNumbers[i] /= divNum;
                                }
                                catch (Exception ex)
                                {
                                    throw new ArgumentException(
                                        $"Division by zero happened", ex);
                                }
                            }
                            userNumbers.RemoveAt(userNumbers.Count - 1);
                        }
                        else
                        {
                            ioDevice.WriteOutput(phraseProvider.getPhrase("GameOver"));
                            break;
                        }
                    }
                    else
                    {
                        ioDevice.WriteOutput(phraseProvider.getPhrase("WrongNumber"));
                    }
                }
                else
                {
                    if (userInput == gameSettings.ExitCode)
                    {
                        ioDevice.WriteOutput(phraseProvider.getPhrase("GameOver"));
                        break;
                    }
                    ioDevice.WriteOutput(phraseProvider.getPhrase("PutNumber"));
                }
            }
            ioDevice.WriteOutput(phraseProvider.getPhrase("Quit"));
            ioDevice.ReadOutput();
        }
    }
}
   