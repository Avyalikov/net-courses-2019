namespace HW3
{
    using System.IO;
    using System.Xml.Serialization;
    using System;

    public class XMLSettings : ISettingsProvider
    {
        string path;
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

        public XMLSettings(string path)
        {
            this.path = path;
        }
    }
}