using System;
using System.IO;

namespace MultithreadConsoleApp.Components
{
    public class IOFile
    { 
        public void WriteToFile(string textToWrite, string pathToWrite)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(pathToWrite, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(textToWrite);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }

        public string ReadFromFile(string pathToRead)
        {
            string result = string.Empty;
            try
            {
                using (StreamReader sr = new StreamReader(pathToRead))
                {
                    result = sr.ReadToEnd();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            return result;
        }

        public void DeleteFile(string pathToDelete)
        {
            string result = string.Empty;
            try
            {
                File.Delete(pathToDelete);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ;
            }
        }
    }
}
   