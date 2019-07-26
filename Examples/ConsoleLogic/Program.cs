using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleLogic
{
    class Program
    {
        static void Main(string[] args)
        {
            IInputReader reader = new ConsoleReader();
            IOutputWriter writer = new ConsoleWriter();

            var logic = new CustomLogicComponent(reader, writer);

            logic.DivideExample();
        }
    }

    public interface IOutputWriter
    {
        void Write(string message);
    };

    public class ConsoleWriter : IOutputWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

    public interface IInputReader
    {
        string ReadLine();
    };

    public class ConsoleReader : IInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
     
    public class CustomLogicComponent
    {
        private readonly IInputReader reader;
        private readonly IOutputWriter writer;

        public CustomLogicComponent(IInputReader reader, IOutputWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void DivideExample()
        {
            this.writer.Write("Enter a number to be divided: "); // Read first argument
            var number01 = Convert.ToInt32(this.reader.ReadLine()); // Read first argument

            this.writer.Write("Enter another number to be divided"); // Read second argument
            var number02 = Convert.ToInt32(this.reader.ReadLine()); // Read second argument

            var number03 = number01 / number02;// Calculate result

            this.writer.Write("The result is: " + number03); // Shows result to user
        }

    }
}
