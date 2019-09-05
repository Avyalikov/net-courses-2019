using System;
using System.IO;

namespace MultithreadConsoleApp.Components
{
    public static class IOFile
    {
        public static void WriteToFile(string textToWrite, string pathToWrite)
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

        public static string ReadFromFile(string pathToRead)
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
    }
}
   