using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
    public class TextMessages
    {
        public string IncorrectChoice { get; set; }
        public string IncorrectInput { get; set; }
        public string MaxLevelReached { get; set; }
        public string EndReached { get; set; }
        public string SettingLoadingError { get; set; }

        public TextMessages()
        {
            IncorrectChoice = "Please choose one of the numbers on the screen";
            IncorrectInput = "Incorrect input, please enter a single integer value";
            MaxLevelReached = "Maximum level reached, going back";
            EndReached = "End";
            SettingLoadingError = "Settings failed to load, using default values instead";
        }
    }
}