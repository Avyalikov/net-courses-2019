using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Delegates
{
    public class MulticastDelegateSample
    {
        delegate void ProgressDelegate(string message, int progressPercentage);

        const string fileName = "logfile.txt";

        void WriteProgressToFile(string message, int progressPercentage)
        {
            File.AppendAllText(fileName, string.Format("{0} ({1}%) - {2}", DateTime.UtcNow, progressPercentage, message));
        }

        void WriteProgressToConsole(string message, int progressPercentage)
        {
            Console.WriteLine("{0} ({1}%) - {2}", DateTime.UtcNow, progressPercentage, message);
        }

        void ProgressEmulator(ProgressDelegate progress)
        {
            int stepCount = (int)(new Random(new TimeSpan().Seconds).NextDouble() * 10);

            progress("Start", 0);

            for (int step = 0; step < stepCount; step++)
            {
                progress("Progress", (int)(step * 100 / stepCount));
                System.Threading.Thread.Sleep(100);
            }

            progress("Finish", 100);
        }

        public void TestProgress()
        {
            ProgressDelegate p = null;
            p += WriteProgressToConsole;
            p += WriteProgressToFile;

            ProgressEmulator(p);
        }
    }
}
