namespace HW3
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class TextMessages
    {
        public TextMessages()
        {
            Welcome = "Welcome! Please press enter to begin";
            Menu = "Please choose:";
            DrawDot = " - draw dot";
            DrawHorizontalLine = " - draw horizontal line";
            DrawVerticalLine = " - draw vertical line";
            DrawSnowFlake = " - draw snowflake";
            Clear = " - clear";
            Exit = " - exit";
            IncorrectOption = "Please select an option from the list";  
        }

        public string Welcome { get; set; }

        public string Menu { get; set; }

        public string DrawDot { get; set; }

        public string DrawHorizontalLine { get; set; }

        public string DrawVerticalLine { get; set; }

        public string DrawSnowFlake { get; set; }

        public string Clear { get; set; }

        public string Exit { get; set; }

        public string IncorrectOption { get; set; }

        [XmlIgnore]
        public List<string> AllMenuItems { get; set; }
    }
}