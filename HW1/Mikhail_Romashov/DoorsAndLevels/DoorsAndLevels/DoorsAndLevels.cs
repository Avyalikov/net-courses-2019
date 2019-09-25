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
            m_arrayNum = new int[5];
            m_levelCoeff = new Stack<int>();
            this.Reset();
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
                        this.Reset();
                        m_levelCoeff.Clear();
                        throw new Exception("Congratulations! You have reached the maximum value. Lets try again.");
                    }
                }
                m_levelCoeff.Push(coeff);
            }
        }

        private void Reset()
        {
            var random = new Random();
            int sizeLst = 9;
            List<int> lstWithNum = new List<int>(sizeLst);
            for (int i = 0; i < sizeLst; i++) //Add to List values 1..9
                lstWithNum.Add(i+1);

            for (int i = 0; i < 4; i++)
            {
                //Get random value from List and Remove that value from List
                int randNum = random.Next(1, sizeLst);
                m_arrayNum[i] = lstWithNum[randNum - 1];
                lstWithNum.RemoveAt(randNum - 1);
                sizeLst--; //Size List decrement
            }

            m_arrayNum[4] = 0;
        }

    }
}
