namespace HW3
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    
    public class XMLSettings : ISettingsProvider
    {
        private string path;      

        public XMLSettings(string path)
        {
            this.path = path;
        }

        public Settings GetSettings()
        {
            Stream stream = null;
            XmlSerializer xmlSerazlizer;
            Settings settings;

            try
            {
                stream = new FileStream(path, FileMode.Open);
                xmlSerazlizer = new XmlSerializer(typeof(Settings));
                settings = (Settings)xmlSerazlizer.Deserialize(stream);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream?.Close();
            }

            return settings;
        }
    }
}