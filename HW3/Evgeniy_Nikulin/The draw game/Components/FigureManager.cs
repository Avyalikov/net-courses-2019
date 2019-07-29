//-----------------------------------------------------------------------
// <copyright file="FigureManager.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game.Components
{
    using DataTypes;
    using Interfaces;

    /// <summary>
    /// Figures class
    /// </summary>
    public class FigureManager : IFigureManager
    {
        /// <summary>
        /// IInputOutput field
        /// </summary>
        private readonly IInputOutput io;

        /// <summary>
        /// figureStile field
        /// </summary>
        private readonly string figureStile;

        /// <summary>
        /// First figure
        /// </summary>
        private Point point;

        /// <summary>
        /// Second figure
        /// </summary>
        private Line verticalLine;

        /// <summary>
        /// Third figure
        /// </summary>
        private Line horizontalLine;

        /// <summary>
        /// Fourth figure
        /// </summary>
        private Rectangle rectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="FigureManager" /> class.
        /// </summary>
        /// <param name="io">Input/Output component</param>
        /// <param name="figureStile">Figure stile</param>
        public FigureManager(
            IInputOutput io,
            string figureStile)
        {
            this.io = io;
            this.figureStile = figureStile;
        }

        /// <summary>
        ///  Draw first figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawFirst(IBoard board)
        {
            this.point = new Point(
                board.boardSizeX / 4,
                board.boardSizeY / 4);        

            this.io.Print(this.point, this.figureStile);
        }

        /// <summary>
        ///  Draw second figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawSecond(IBoard board)
        {
            if (this.verticalLine == null)
            {
                this.verticalLine = new Line(
                    new Point(
                        board.boardSizeX - (board.boardSizeX / 4),
                        2),
                    new Point(
                        board.boardSizeX - (board.boardSizeX / 4),
                        (board.boardSizeY / 2) - 2));
            }

            foreach (var point in this.verticalLine.Body)
            {
                this.io.Print(point, this.figureStile);
            }
        }

        /// <summary>
        ///  Draw third figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawThird(IBoard board)
        {
            if (this.horizontalLine == null)
            {
                this.horizontalLine = new Line(
                    new Point(
                        4,
                        board.boardSizeY - (board.boardSizeY / 4)),
                    new Point(
                        (board.boardSizeX / 2) - 4,
                        board.boardSizeY - (board.boardSizeY / 4)));
            }

            foreach (var point in this.horizontalLine.Body)
            {
                this.io.Print(point, this.figureStile);
            }
        }

        /// <summary>
        ///  Draw fourth figure
        /// </summary>
        /// <param name="board">Board component</param>
        public void DrawFourth(IBoard board)
        {
            if (this.rectangle == null)
            {
                this.rectangle = new Rectangle(
                    new Point(
                        (board.boardSizeX / 2) + 4,
                        (board.boardSizeY / 2) + 2),
                    new Point(
                        board.boardSizeX - 4,
                        board.boardSizeY - 2));
            }

            foreach (var point in this.rectangle.Body)
            {
                this.io.Print(point, this.figureStile);
            }
        }
    }
}