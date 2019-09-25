using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace HW2
{
    public class XMLSettings : ISettingsProvider
    {
        string path;
        public Settings getSettings()
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
            catch(Exception)
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