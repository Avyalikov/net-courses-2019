using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Interfaces
{
    public interface IFileSystemManager
    {
        Task WriteToFile(string textToWrite, string pathToWrite);
        void DeleteFile(string pathToDelete);
    }
}
