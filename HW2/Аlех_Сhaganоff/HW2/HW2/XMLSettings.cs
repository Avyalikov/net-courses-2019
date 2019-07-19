using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

using MyType = System.Int32;

namespace HW2
{
    public class XMLSettings : ISettingsProvider
    {
        public Settings getSettings()
        {
            Stream stream = null;
            XmlSerializer xmlSerazlizer;
            Settings settings;

            try
            {
                stream = new FileStream("settings.xml", FileMode.Open);
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
    }
}