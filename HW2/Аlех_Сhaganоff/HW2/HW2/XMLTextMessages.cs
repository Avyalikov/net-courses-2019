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
        public TextMessages getTextMessages()
        {
            Stream stream = null;
            XmlSerializer xmlSerazlizer;
            TextMessages textMessages;

            try
            {
                stream = new FileStream("textmessages-rus.xml", FileMode.Open);
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
    }
}
