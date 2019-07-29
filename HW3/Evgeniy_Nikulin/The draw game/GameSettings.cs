//-----------------------------------------------------------------------
// <copyright file="GameSettings.cs" company="EPAM">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------

namespace The_draw_game
{
    /// <summary>
    /// Game settings class
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Gets or sets border height
        /// </summary>
        public int BorderHeight { get; set; }

        /// <summary>
        /// Gets or sets border width
        /// </summary>
        public int BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets border Stile
        /// </summary>
        public string BorserStile { get; set; }

        /// <summary>
        /// Gets or sets figure Stile
        /// </summary>
        public string FigureStile { get; set; }

        /// <summary>
        /// Gets or sets game language
        /// </summary>
        public string Language { get; set; }
    }
}