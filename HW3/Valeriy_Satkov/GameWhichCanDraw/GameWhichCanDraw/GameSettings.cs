// <copyright file="GameSettings.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw
{
    /// <summary>
    /// Game settings class
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Gets or sets the Length of board
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the Width of board
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the String for exit
        /// </summary>
        public string ExitCode { get; set; }

        /// <summary>
        /// Gets or sets the Key part of path to language source
        /// </summary>
        public string Language { get; set; }
    }
}
