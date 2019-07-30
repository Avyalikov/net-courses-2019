// <copyright file="GameSettings.cs" company="Valeriy Satkov">
// All rights reserved.
// </copyright>
// <author>Valeriy Satkov</author>

namespace GameWhichCanDraw
{
    using System;

    /// <summary>
    /// Game settings class
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// the Length of board
        /// </summary>
        private int length;

        /// <summary>
        /// the Length of board
        /// </summary>
        private int width;

        /// <summary>
        /// Gets or sets the Length of board
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }

            set
            {
                if (value < 7)
                {
                    throw new ArgumentException($"Incorrect Length = {value} in settings file. It should not be less than 7");
                }

                this.length = value;
            }
        }

        /// <summary>
        /// Gets or sets the Width of board
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value < 7)
                {
                    throw new ArgumentException($"Incorrect Width = {value} in settings file. It should not be less than 7");
                }

                this.width = value;
            }
        }

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
