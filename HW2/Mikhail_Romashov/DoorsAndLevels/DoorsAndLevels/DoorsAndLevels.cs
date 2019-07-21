using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class DoorsAndLevels
    {
        private int[] m_arrayDoorsValue;           //array of doors value
        private Stack<int> m_levelCoeff;    //stack witch contains coefficient for each level
        bool exitCode = false;          //flag to exit (if entered negative number)

        public void Run()
        {
            Console.WriteLine("Let`s start to game");

            do
            {
                Console.WriteLine("Choose one of the number for next level or \'0\' to previous level.");
                Console.WriteLine("For exit enter a negative number:");
                this.Show();
                string resultStr = Console.ReadLine();
                try
                {
                    int result = Convert.ToInt32(resultStr);
                    this.CalcLevel(result);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"The value '{resultStr}' is not a number.");
                }

            } while (!exitCode);
            Console.WriteLine("Thank you for playing! Enter any key to exit.");
            Console.ReadKey();
        }

        public DoorsAndLevels()
        {
            m_arrayDoorsValue = new int[5];
            m_levelCoeff = new Stack<int>();
            this.Reset();
        }

        private void Show()      //output array of numbers
        {
            Console.WriteLine("--------------");
            foreach (int num in m_arrayDoorsValue)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine("--------------");
        }

        private void CalcLevel(int doorValue)
        {
            if (doorValue < 0)
            {
                exitCode = true;
                return;
            }
            if (!m_arrayDoorsValue.Contains(doorValue))    //array doesnt contains coeff
            {
                Console.WriteLine("Number is not in list!");
                return;
            }


            if (doorValue == 0)
            {
                if (m_levelCoeff.Count == 0)  //stack is empty
                {
                    Console.WriteLine("It is first level!");
                    return;

                }
                int divider = m_levelCoeff.Pop();
                for (int i = 0; i < m_arrayDoorsValue.Count(); i++)
                {
                    m_arrayDoorsValue[i] /= divider; // return for previous level
                }
            }
            else
            {

                for (int i = 0; i < m_arrayDoorsValue.Count(); i++)
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
                        Console.WriteLine("Congratulations! You have reached the maximum value. Lets try again.");
                        return;
                    }
                }
                m_levelCoeff.Push(doorValue);
            }
        }

        private void Reset()
        {
            var random = new Random();
            int maxValue = 9;  //
            var listWithValues = new List<int>(Enumerable.Range(1, 9));


            for (int i = 0; i < 4; i++)
            {
                //Get random value from List and Remove that value from List
                int index = random.Next(1, maxValue);
                m_arrayDoorsValue[i] = listWithValues[index - 1];
                listWithValues.RemoveAt(index - 1);
                maxValue--; //max index value decrease
            }

            m_arrayDoorsValue[4] = 0;
        }

    }
}
