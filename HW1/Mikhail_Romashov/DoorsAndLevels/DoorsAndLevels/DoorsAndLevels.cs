using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorsAndLevels
{
    class DoorsAndLevels
    {
        private int[] m_arrayNum;           //array of numbers
        private Stack<int> m_levelCoeff;    //stack witch contains coefficient for each level

        public DoorsAndLevels()
        {
            m_arrayNum = new int[5] { 1, 2, 3, 4, 0 };
            m_levelCoeff = new Stack<int>();
        }

        public void Show()      //output array of numbers
        {
            Console.WriteLine("--------------");
            foreach (int num in m_arrayNum)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine("--------------");
        }

        public void CalcLevel(int coeff)
        {
            if (!m_arrayNum.Contains(coeff))    //array doesnt contains coeff
            {
                throw new Exception("Number is not in list! Choose again.");
            }


            if (coeff == 0)
            {
                if (m_levelCoeff.Count == 0)  //stack is empty
                {
                    throw new Exception("It is first level! Choose again.");
                }
                int divider = m_levelCoeff.Pop();
                for (int i = 0; i < m_arrayNum.Count(); i++)
                {
                    m_arrayNum[i] /= divider; // return for previous level
                }
            }
            else
            {

                for (int i = 0; i < m_arrayNum.Count(); i++)
                {

                    try
                    {
                        m_arrayNum[i] = checked(m_arrayNum[i] * coeff); // go to next level 
                    }
                    catch (OverflowException)
                    {
                        // if some value in m_arrayNum > maxValueInt32
                        this.GoToStart();
                        throw new Exception("Congratulations! You have reached the maximum value. Lets try again.");
                    }
                }
                m_levelCoeff.Push(coeff);
            }
        }

        private void GoToStart()
        {
            m_arrayNum[0] = 1;
            m_arrayNum[1] = 2;
            m_arrayNum[2] = 3;
            m_arrayNum[3] = 4;
            m_arrayNum[4] = 0;
            m_levelCoeff.Clear();
        }

    }
}
