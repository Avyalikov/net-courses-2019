using System;
using System.IO;

namespace Delegates
{
    public interface IProgressIndicator
    {
        void Progress(string message, int progressPercentage);
    }

    public class WriteLogToFile : IProgressIndicator
    {
        string fileName;

        public WriteLogToFile(string fileName)
        {
            this.fileName = fileName;
        }

        public void Progress(string message, int progressPercentage)
        {
            File.AppendAllText(fileName, string.Format("{0} ({1}%) - {2}", DateTime.UtcNow, progressPercentage, message));
        }
    }

    public class WriteLogToConsole : IProgressIndicator
    {
        public void Progress(string message, int progressPercentage)
        {
            Console.WriteLine("{0} ({1}%) - {2}", DateTime.UtcNow, progressPercentage, message);
        }
    }

    public static class FileCopyUtility
    {
        private const int bufferSize = 10000;

        static public void Copy(string srcPath, string dstPath, IProgressIndicator progressIndicator)
        {
            var fi = new FileInfo(srcPath);

            var srcFile = new FileStream(srcPath, FileMode.Open);
            var dstFile = new FileStream(dstPath, FileMode.CreateNew);

            byte[] buffer = new byte[bufferSize];
            int readedSize;
            int step = 0;

            progressIndicator.Progress("Start copy", 0);

            do
            {
                readedSize = srcFile.Read(buffer, 0, bufferSize);
                dstFile.Write(buffer, 0, bufferSize);
                step++;

                progressIndicator.Progress("Progress copy", (int)((step * readedSize) * 100 / fi.Length));

            } while (readedSize != 0);

            progressIndicator.Progress("Finish copy", 100);
        }
    }
}
