using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class DoorsAndLevels
    {
        private readonly IInputOutputComponent ioComponent;
        private readonly IDoorsNumbersGenerator doorsNumbersGenerator;
        private readonly ISettingsProvider settingsProvider;
        private readonly IPhraseProvider phraseProvider;
        private readonly IStorageComponent stackStorageComponent;

        private readonly GameSettings gameSettings;

        private int[] m_arrayDoorsValue;           //array of doors value
//        private Stack<int> m_levelCoeff;    //stack witch contains coefficient for each level
        bool exitCode = false;          //flag to exit (if entered exit code)


        public DoorsAndLevels(
           IInputOutputComponent inputOutputComponent,
           IDoorsNumbersGenerator doorsNumbersGenerator,
           ISettingsProvider settingsProvider,
           IPhraseProvider phraseProvider,
           IStorageComponent stackStorageComponent
           )
        {
            this.ioComponent = inputOutputComponent;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
            this.settingsProvider = settingsProvider;
            this.phraseProvider = phraseProvider;
            this.stackStorageComponent = stackStorageComponent;

            this.gameSettings = this.settingsProvider.gameSettings();

            m_arrayDoorsValue = new int[gameSettings.doorsAmount];
            this.Reset();
        }
        public void Run()
        {
            //ioComponent.WriteOutputLine("Let`s start to game");
            ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Start"));
            do
            {
                //ioComponent.WriteOutputLine($"Choose one of the number for next level or {gameSettings.previousLevelCode} to previous level.");
                //ioComponent.WriteOutputLine($"For exit enter {gameSettings.exitCode}:");
                ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Intro"));
                this.Show();
                string resultStr = ioComponent.ReadInputLine();
                try
                {
                    int result = Convert.ToInt32(resultStr);
                    this.CalcLevel(result);
                }
                catch (FormatException)
                {
                    //ioComponent.WriteOutputLine($"The value '{resultStr}' is not a number.");
                    ioComponent.WriteOutputLine(phraseProvider.GetPhrase("BadValue"));
                }

            } while (!exitCode);
            //ioComponent.WriteOutputLine("Thank you for playing! Press any key to exit.");
            ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Exit"));
            ioComponent.ReadInputKey();
        }

       

        private void Show()      //output array of numbers
        {
            ioComponent.WriteOutputLine("--------------");
            foreach (int num in m_arrayDoorsValue)
            {
                ioComponent.WriteOutput(num + " ");
            }
            ioComponent.WriteOutputLine();
            ioComponent.WriteOutputLine("--------------");
        }

        private void CalcLevel(int doorValue)  //recalculation level using door value
        {
            if (doorValue == gameSettings.exitCode) //enetred exit code
            {
                exitCode = true;//exit flag
                return;
            }
            if (!m_arrayDoorsValue.Contains(doorValue))    //array doesnt contains coeff
            {
                //ioComponent.WriteOutputLine("Number is not in list!");
                ioComponent.WriteOutputLine(phraseProvider.GetPhrase("ValueNotInList"));
                return;
            }
            if (doorValue == gameSettings.previousLevelCode)
            {
                if (stackStorageComponent.GetSize() == 0)  //stack is empty
                {
                        //ioComponent.WriteOutputLine("It is first level!");
                        ioComponent.WriteOutputLine(phraseProvider.GetPhrase("FirstLevel"));
                        return;
                }
                int divider = stackStorageComponent.Pop();
                for (int i = 0; i < gameSettings.doorsAmount-1; i++)
                {
                        m_arrayDoorsValue[i] /= divider; // return for previous level             
                }
            }
            else
            {
                for (int i = 0; i < gameSettings.doorsAmount-1; i++)
                {

                    try
                    {
                        m_arrayDoorsValue[i] = checked(m_arrayDoorsValue[i] * doorValue); // go to next level 
                    }
                    catch (OverflowException)
                    {
                        // if some value in m_arrayDoorsValue > maxValueInt32
                        this.Reset();
                        stackStorageComponent.Clear();
                        //ioComponent.WriteOutputLine("Congratulations! You have reached the maximum value. Lets try again.");
                        ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Win"));
                        return;
                    }
                }
                stackStorageComponent.Push(doorValue);
            }
        }

        private void Reset()
        {  //generate new random array of the doors numbers using doors amount and previous level code
            m_arrayDoorsValue = doorsNumbersGenerator.GenerateDoorsNumbers(gameSettings.doorsAmount, gameSettings.previousLevelCode);
        }

    }
}