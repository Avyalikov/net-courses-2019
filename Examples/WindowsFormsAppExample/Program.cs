using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppExample
{
    public enum Color
    {
        Red,
        Green
    }

    public class Curve
    {
        public string Name { get; set; }

        public Color Color { get; set; }

        public string Style { get; set; }

        public override string ToString()
        {
            return $"This is the curve: { Name }, { Style }";
        }
    }
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
