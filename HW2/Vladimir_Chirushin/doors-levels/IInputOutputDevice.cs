namespace doors_levels
{ 
        public interface IInputOutputDevice
        {
            string ReadInput();
            void WriteOutput(string dataToOutput);
        }
}
