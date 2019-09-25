namespace HW3
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class Settings
    {
        public Settings()
        {
            this.KeyDrawDot = "1";
            this.KeyDrawHorizontalLine = "2";
            this.KeyDrawVerticalLine = "3";
            this.KeyDrawSnowFlake = "4";
            this.KeyClear = "5";
            this.KeyExit = "0";

            this.BoardSizeX = 32;
            this.BoardSizeY = 16;
        }

        public string KeyDrawDot { get; set; }

        public string KeyDrawHorizontalLine { get; set; }

        public string KeyDrawVerticalLine { get; set; }

        public string KeyDrawSnowFlake { get; set; }

        public string KeyClear { get; set; }

        public string KeyExit { get; set; }

        public int BoardSizeX { get; set; }

        public int BoardSizeY { get; set; }

        [XmlIgnore]
        public Dictionary<string, string> AllMenuKeys { get; set; }

        public void InitializeAllMenuKeys()
        {
            this.AllMenuKeys = new Dictionary<string, string>()
            {
                { KeyDrawDot, nameof(KeyDrawDot) },
                { KeyDrawHorizontalLine, nameof(KeyDrawHorizontalLine) },
                { KeyDrawVerticalLine, nameof(KeyDrawVerticalLine) },
                { KeyDrawSnowFlake, nameof(KeyDrawSnowFlake) },
                { KeyClear, nameof(KeyClear) },
                { KeyExit, nameof(KeyExit) }
            };
        }
    }      
}