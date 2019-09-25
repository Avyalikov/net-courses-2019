namespace DoorsAndLevelsGame
{
    /// <summary>
    /// InputOutputDevice is a device for interacting with user. Should support Write and Read operations.
    /// </summary>
    interface IInputOutputDevice
    {
        /// <summary>
        /// This method writes a message into the output device.
        /// </summary>
        /// <param name="msg">Message for the output device.</param>
        void Write(string msg);
        /// <summary>
        /// This method writes a message into the output device adding the current line terminator.
        /// </summary>
        /// <param name="msg">Message for the output device.</param>
        void WriteLine(string msg);
        /// <summary>
        /// This method writes the current line terminator into the output device.
        /// </summary>
        void WriteLine();
        /// <summary>
        /// This method writes a special error message into the output device.
        /// </summary>
        /// <param name="msg">Error message for the output device.</param>
        void WriteError(string msg);
        /// <summary>
        /// This method reads data from the input device.
        /// </summary>
        /// <returns>Data from the input device.</returns>
        string Read();
    }
}
