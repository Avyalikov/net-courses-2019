// <copyright file="GameSettings.cs" company="IPavlov">
// Copyright (c) IPavlov. All rights reserved.
// </copyright>

namespace ConsoleDraw
{
    /// <summary>
    /// game setting class.
    /// </summary>
    public class GameSettings
    {
        private int lenght;
        private int width;

        /// <summary>
        /// Gets or sets x-coordinate.
        /// </summary>
        public int Length
        {
            get
            {
                return this.lenght;
            }

            set
            {
                if (value < 10)
                {
                    this.lenght = 10;
                }
                else
                {
                    this.lenght = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets y-coordinate.
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }

            set
            {
                if (value < 10)
                {
                    this.width = 10;
                }
                else
                {
                    this.width = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets exit code.
        /// </summary>
        public string ExitCode { get; set; }

        /// <summary>
        /// Gets or sets lang.
        /// </summary>
        public string Language { get; set; }
    }
}
