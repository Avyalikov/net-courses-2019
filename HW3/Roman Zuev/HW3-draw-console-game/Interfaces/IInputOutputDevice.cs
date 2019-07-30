// <copyright file="IInputOutputDevice.cs" company=".net courses 2019">
// All rights reserved.
// </copyright>
// <author>Roman Zuev</author>

namespace HW3_console_draw_game
{
    /// <summary>
    /// Defines the <see cref="IInputOutputDevice" />
    /// </summary>
    internal interface IInputOutputDevice
    {
        /// <summary>
        /// The InputValue
        /// </summary>
        /// <returns>The <see cref="string"/></returns>
        string InputValue();

        /// <summary>
        /// The Print
        /// </summary>
        /// <param name="printValue">The printValue<see cref="string"/></param>
        void Print(string printValue);
    }
}
