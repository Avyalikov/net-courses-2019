namespace HW3
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Settings
    {
        public string KeyDrawDot  { get; set; }
        public string KeyDrawHorizontalLine { get; set; }
        public string KeyDrawVerticalLine { get; set; }
        public string KeyDrawSnowFlake { get; set; }
        public string KeyClear { get; set; }
        public string KeyExit { get; set; }

        public int BoardSizeX { get; set; }
        public int BoardSizeY { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> AllMenuKeys { get; set; }

        public Settings()
        {
            KeyDrawDot = "1";
            KeyDrawHorizontalLine = "2";
            KeyDrawVerticalLine = "3";
            KeyDrawSnowFlake = "4";
            KeyClear = "5";
            KeyExit = "0";
            
            BoardSizeX = 32;
            BoardSizeY = 16;
        }

        public void InitializeAllMenuKeys()
        {
            AllMenuKeys = new Dictionary<string, string>()
            {
                { KeyDrawDot, nameof(KeyDrawDot) },
                { KeyDrawHorizontalLine, nameof(KeyDrawHorizontalLine) },
                { KeyDrawVerticalLine,nameof(KeyDrawVerticalLine) },
                { KeyDrawSnowFlake,nameof(KeyDrawSnowFlake) },
                { KeyClear,nameof(KeyClear) },
                { KeyExit,nameof(KeyExit) }
            };
        }
    }      
}