namespace Multithread.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IFileManager
    {
        StreamReader StreamReader(string path);

        FileStream FileStream(string filePath, FileMode fileMode);
    }
}
