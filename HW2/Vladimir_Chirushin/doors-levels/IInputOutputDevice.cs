using System;

namespace doors_levels
{ 
        public interface IInputOutputDevice
        {
            String ReadInput();
            void WriteOutput(String dataToOutput);
        }
}
