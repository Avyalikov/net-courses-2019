namespace HW3
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    class XMLTextMessages : ITextMessagesProvider
    {
        string path;

        public TextMessages GetTextMessages()
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
