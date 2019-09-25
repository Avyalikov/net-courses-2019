using MultithreadConsoleApp.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MultithreadConsoleApp.Components
{
    public class FileSystemManager : IFileSystemManager
    { 
        public async Task WriteToFile(string textToWrite, string pathToWrite)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathToWrite, false, System.Text.Encoding.Default))
                {
                    await sw.WriteAsync(textToWrite);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
        public void DeleteFile(string pathToDelete)
        {
            try
            {
                File.Delete(pathToDelete);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
   