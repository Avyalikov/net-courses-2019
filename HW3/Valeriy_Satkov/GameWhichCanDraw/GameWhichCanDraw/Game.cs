namespace GameWhichCanDraw
{
    using System;
    using GameWhichCanDraw.Interfaces;

    internal class Game
    {
        public void Start()
        {
            IBoard dashBoard = new Components.DashBoard(length: 25, width: 15);
            dashBoard.Create();

            Console.ReadLine(); // pause
        }
    }
}
