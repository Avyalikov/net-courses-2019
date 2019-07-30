namespace ConsoleDrawGame.Interfaces
{
    internal interface IBoard
    {
        int BoardSizeX { get; set; }

        int BoardSizeY { get; set; }

        /// <summary>Prints board.</summary>
        void PrintBoard();

        /// <summary>Prints simple dot.</summary>
        void PrintDot();

        /// <summary>Prints horizontal line.</summary>
        void PrintHorizontal();

        /// <summary>Prints Vertical line.</summary>
        void PrintVertical();

        /// <summary>Prints Other Curve.</summary>
        void PrintOtherCurve();

    }
}
