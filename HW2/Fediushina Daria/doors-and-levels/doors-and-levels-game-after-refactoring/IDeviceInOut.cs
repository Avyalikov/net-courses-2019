namespace doors_and_levels_game_after_refactoring
{
    internal interface IDeviceInOut
    {
        void WriteOutput(string OutputData);
        string ReadOutput();
    }
}