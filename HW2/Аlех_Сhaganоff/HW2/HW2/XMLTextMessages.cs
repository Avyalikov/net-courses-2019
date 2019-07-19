using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace HW2
{
    class XMLTextMessages : ITextMessagesProvider
    {
        string path;

        public TextMessages getTextMessages()
        {
            Stream stream = null;
            XmlSerializer xmlSerazlizer;
            TextMessages textMessages;

            try
            {
                stream = new FileStream(path, FileMode.Open);
                xmlSerazlizer = new XmlSerializer(typeof(TextMessages));
                textMessages = (TextMessages)xmlSerazlizer.Deserialize(stream);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                stream?.Close();
            }

            return textMessages;
        }

        public XMLTextMessages(string path)
        {
            this.path = path;
        }
    }
}
