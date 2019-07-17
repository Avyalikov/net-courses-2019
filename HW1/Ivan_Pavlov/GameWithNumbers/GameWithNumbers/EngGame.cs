using System;
using System.Collections.Generic;

namespace GameWithNumbers
{
    public class EngGame
    {
        
        Random rnd = new Random();
        private int[] EngArr = new int[5];
        Stack<int> Divider = new Stack<int>();

        public EngGame()
        {
            for(int i = 0; i < EngArr.Length - 1; i++)
            {
                EngArr[i] = rnd.Next(1, 9);
            }
            EngArr[EngArr.Length - 1] = 0;

            Console.Write("We have numbers: ");
            Print();
        }

        public void Play(int a)
        {

            if (!CheckDoor(a))
            {
                Console.WriteLine("Use other door pls");
                Print();
            }
            else
            {
                if (a != 0)
                {
                    Console.Write("Your number: {0}. We have next numbers:  ", a);
                    for (int i = 0; i < EngArr.Length; i++)
                        EngArr[i] *= a;
                    Divider.Push(a);
                    Print();
                }
                else if (a == 0)
                {
                    try
                    {
                        LeftLevel(Divider.Pop());
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine("You realy want to lose? {0}", e.Message);
                    }
                }
            }
        }

        private bool CheckDoor(int a)
        {
            foreach (int i in EngArr)
                if (i == a)
                    return true;

            return false;
        }

        private void LeftLevel(int a)
        {            
            Console.Write("Your number: 0. We have previous numbers: ");
            for (int i = 0; i < EngArr.Length; i++)
                EngArr[i] /= a;
            Print();
        }

        private void Print()
        {
            foreach (int i in EngArr)
                Console.Write("{0} ", i);
        }
    }
}
