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

        private readonly GameSettings gameSettings;

        private int[] m_arrayDoorsValue;           //array of doors value
        private Stack<int> m_levelCoeff;    //stack witch contains coefficient for each level
        bool exitCode = false;          //flag to exit (if entered negative number)


        public DoorsAndLevels(
           IInputOutputComponent inputOutputComponent,
           IDoorsNumbersGenerator doorsNumbersGenerator,
           ISettingsProvider settingsProvider
           )
        {
            ioComponent = inputOutputComponent;
            this.doorsNumbersGenerator = doorsNumbersGenerator;
            this.settingsProvider = settingsProvider;

            this.gameSettings = this.settingsProvider.gameSettings();

            m_arrayDoorsValue = new int[gameSettings.doorsAmount];
            m_levelCoeff = new Stack<int>();
            this.Reset();
        }
        public void Run()
        {
            ioComponent.WriteOutputLine("Let`s start to game");
            do
            {
                ioComponent.WriteOutputLine($"Choose one of the number for next level or {gameSettings.previousLevelCode} to previous level.");
                ioComponent.WriteOutputLine($"For exit enter {gameSettings.exitCode}:");
                this.Show();
                string resultStr = ioComponent.ReadInputLine();
                try
                {
                    int result = Convert.ToInt32(resultStr);
                    this.CalcLevel(result);
                }
                catch (FormatException)
                {
                    ioComponent.WriteOutputLine($"The value '{resultStr}' is not a number.");
                }

            } while (!exitCode);
            ioComponent.WriteOutputLine("Thank you for playing! Press any key to exit.");
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

        private void CalcLevel(int doorValue)
        {
            if (doorValue == gameSettings.exitCode)
            {
                exitCode = true;
                return;
            }
            if (!m_arrayDoorsValue.Contains(doorValue))    //array doesnt contains coeff
            {
                ioComponent.WriteOutputLine("Number is not in list!");
                return;
            }


            if (doorValue == gameSettings.previousLevelCode)
            {
                if (m_levelCoeff.Count == 0)  //stack is empty
                {
                    ioComponent.WriteOutputLine("It is first level!");
                    return;
                }
                int divider = m_levelCoeff.Pop();
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
                        m_levelCoeff.Clear();
                        ioComponent.WriteOutputLine("Congratulations! You have reached the maximum value. Lets try again.");
                        return;
                    }
                }
                m_levelCoeff.Push(doorValue);
            }
        }

        private void Reset()
        {
            m_arrayDoorsValue = doorsNumbersGenerator.GenerateDoorsNumbers(gameSettings.doorsAmount, gameSettings.previousLevelCode);
        }

    }
}