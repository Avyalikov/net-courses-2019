// <copyright file="GameSettings.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="GameSettings" />
    /// </summary>
    internal class GameSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameSettings"/> class.
        /// </summary>
        /// <param name="language">The language<see cref="string"/></param>
        /// <param name="exitCode">The exitCode<see cref="string"/></param>
        /// <param name="clearBoard">The clearBoard<see cref="string"/></param>
        /// <param name="firstFigure">The firstFigure<see cref="string"/></param>
        /// <param name="secondFigure">The secondFigure<see cref="string"/></param>
        /// <param name="thirdFigure">The thirdFigure<see cref="string"/></param>
        /// <param name="forthFigure">The forthFigure<see cref="string"/></param>
        public GameSettings(
            string language,
            string exitCode,
            string clearBoard,
            string firstFigure,
            string secondFigure,
            string thirdFigure,
            string forthFigure)
        {
            this.Language = language;
            this.ExitCode = exitCode;
            this.ClearBoard = clearBoard;
            this.FirstFigure = firstFigure;
            this.SecondFigure = secondFigure;
            this.Third = thirdFigure;
            this.ForthFigure = forthFigure;
        }

        /// <summary>
        /// Gets the Language
        /// </summary>
        internal string Language { get; }

        /// <summary>
        /// Gets the ExitCode
        /// </summary>
        internal string ExitCode { get; }

        /// <summary>
        /// Gets the ClearBoard
        /// </summary>
        internal string ClearBoard { get; }

        /// <summary>
        /// Gets the FirstFigure
        /// </summary>
        internal string FirstFigure { get; }

        /// <summary>
        /// Gets the SecondFigure
        /// </summary>
        internal string SecondFigure { get; }

        /// <summary>
        /// Gets the Third
        /// </summary>
        internal string Third { get; }

        /// <summary>
        /// Gets the ForthFigure
        /// </summary>
        internal string ForthFigure { get; }
    }
}
