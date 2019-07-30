namespace HW3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IReadInputProvider readInputProvider = new ConsoleInput();
            ISendOutputProvider sendOutputProvider = new ConsoleOutput();
            ITextMessagesProvider textMessagesProvider = new XMLTextMessages("eng.xml");
            ISettingsProvider settingsProvider = new XMLSettings("settings.xml");
            ICommands commands = new Commands();

            ConsoleDrawing consoleDrawing = new ConsoleDrawing(
                readInputProvider: readInputProvider,
                sendOutputProvider: sendOutputProvider,
                textMessagesProvider: textMessagesProvider,
                settingsProvider: settingsProvider,
                commands: commands);

            consoleDrawing.Run();
        }
    }
}
