namespace HW3
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public class XMLTextMessages : ITextMessagesProvider
    {
        private string path;

        public XMLTextMessages(string path)
        {
            this.path = path;
        }

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
    }
}
