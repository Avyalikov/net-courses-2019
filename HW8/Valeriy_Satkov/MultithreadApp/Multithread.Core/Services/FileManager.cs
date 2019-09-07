namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FileManager : IFileManager
    {
        public FileStream FileStream(string filePath, FileMode fileMode)
        {
            return new FileStream(filePath, fileMode);
        }

        public StreamReader StreamReader(string path)
        {
            return new StreamReader(path);
        }
    }
}
