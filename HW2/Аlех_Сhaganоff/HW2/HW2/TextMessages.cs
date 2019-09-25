using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2
{
    public class TextMessages
    {
        public string IncorrectChoice { get; set; }
        public string IncorrectInputP1 { get; set; }
        public string IncorrectInputP2 { get; set; }
        public string MaxLevelReached { get; set; }
        public string EndReached { get; set; }
        public string SettingLoadingError { get; set; }

        public TextMessages()
        {
            IncorrectChoice = "Please choose one of the numbers on the screen";
            IncorrectInputP1 = "Incorrect input, please enter a single integer value; enter /'0/' or /'";
            IncorrectInputP2 = "/' to go back";
            MaxLevelReached = "Maximum level reached, going back";
            EndReached = "End";
            SettingLoadingError = "Settings failed to load, using default values instead";
        }
    }
}