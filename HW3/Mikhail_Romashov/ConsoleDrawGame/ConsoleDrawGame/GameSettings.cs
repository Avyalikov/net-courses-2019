//-----------------------------------------------------------------------
// <copyright file="GameSettings.cs" company="Epam">
//     Copyright (c) Epam. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ConsoleDrawGame
{
    /// <summary>
    /// Class which include options for game
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Gets or sets length of board
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets width of board
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets max length of board
        /// </summary>
        public int MaxLength { get; set; }

        /// <summary>
        /// Gets or sets max length of board
        /// </summary>
        public int MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets code to exit from application
        /// </summary>
        public string ExitCode { get; set; }
    }
}