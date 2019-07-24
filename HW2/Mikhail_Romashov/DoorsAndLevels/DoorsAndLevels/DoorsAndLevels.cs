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
        }
        public void Run()
        {
            this.Reset();
            ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Start"));
            do
            {
                ioComponent.WriteOutputLine(phraseProvider.GetPhrase("FirstIntro") + gameSettings.previousLevelCode);
                ioComponent.WriteOutputLine(phraseProvider.GetPhrase("SecondIntro") + gameSettings.exitCode);
                this.Show();
                string resultStr = ioComponent.ReadInputLine();
                try
                {
                    int result = Convert.ToInt32(resultStr);
                    this.CalcLevel(result);
                }
                catch (FormatException)
                {
                    ioComponent.WriteOutputLine(phraseProvider.GetPhrase("BadValue"));
                }
                catch (OverflowException)
                {
                    ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Overflow"));
                }

            } while (!exitCode);
            ioComponent.WriteOutputLine(phraseProvider.GetPhrase("Exit"));
            ioComponent.ReadInputKey();
        }

       

        private void Show()
        {
            ioComponent.WriteOutputLine("--------------");
            foreach (int num in m_arrayDoorsValue)
            {
                ioComponent.WriteOutput(num + " ");
            }
            ioComponent.WriteOutputLine();
            ioComponent.WriteOutputLine("--------------");
        }

        private void CalcLevel(int doorValue)
        {
            if (doorValue == gameSettings.exitCode) //if enetred exit code
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
                    { // "checked" - to check out of range int32
                        m_arrayDoorsValue[i] = checked(m_arrayDoorsValue[i] * doorValue);  
                    }
                    catch (OverflowException)
                    {
                        // if some value in m_arrayDoorsValue > maxValueInt32
                        this.Reset();
                        stackStorageComponent.Clear();
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