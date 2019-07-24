namespace HW3
{
    using System;
        
    class ConsoleDrawing
    {
        private readonly TextMessages textMessages;
        private readonly Settings settings;
        
        private readonly IBoard board;
        private readonly IReadInputProvider readInputProvider;
        private readonly ISendOutputProvider sendOutputProvider;
        private readonly ITextMessagesProvider textMessagesProvider;
        private readonly ISettingsProvider settingsProvider;
        private readonly ICommands commands;

        delegate void Draw(IBoard board);
        Draw draw;

        public ConsoleDrawing
        (
            IReadInputProvider readInputProvider,
            ISendOutputProvider sendOutputProvider,
            ITextMessagesProvider textMessagesProvider,
            ISettingsProvider settingsProvider,
            ICommands commands
        )
        {
            this.readInputProvider = readInputProvider;
            this.sendOutputProvider = sendOutputProvider;
            this.textMessagesProvider = textMessagesProvider;
            this.settingsProvider = settingsProvider;
            this.commands = commands;

            try
            {
                this.settings = settingsProvider.GetSettings();
            }
            catch (Exception e)
            {
                sendOutputProvider.PrintOutput(e.ToString());
                sendOutputProvider.PrintOutput("Settings failed to load, using default values instead");
                this.settings = new Settings();
            }

            try
            {
                this.textMessages = textMessagesProvider.GetTextMessages();
            }
            catch (Exception e)
            {
                sendOutputProvider.PrintOutput(e.ToString());
                sendOutputProvider.PrintOutput("Language settings failed to load, using default values instead");
                this.textMessages = new TextMessages();
            }         

            this.settings.InitializeAllMenuKeys();

            this.board = new Board(this.settings.BoardSizeX, this.settings.BoardSizeY);
            draw += this.commands.DrawDashboard;
        }

        public void DisplayMenu()
        {
            sendOutputProvider.PrintOutput(textMessages.Menu);
            sendOutputProvider.PrintOutput(settings.KeyDrawDot + textMessages.DrawDot);
            sendOutputProvider.PrintOutput(settings.KeyDrawHorizontalLine + textMessages.DrawHorizontalLine);
            sendOutputProvider.PrintOutput(settings.KeyDrawVerticalLine + textMessages.DrawVerticalLine);
            sendOutputProvider.PrintOutput(settings.KeyDrawSnowFlake + textMessages.DrawSnowFlake);
            sendOutputProvider.PrintOutput(settings.KeyClear + textMessages.Clear);
            sendOutputProvider.PrintOutput(settings.KeyExit + textMessages.Exit);
        }

        public string InputProcessing()
        {
            string userInput = string.Empty;
            bool exitCondition = false;

            do
            {
                userInput = readInputProvider.ReadInput();

                if (!settings.AllMenuKeys.ContainsKey(userInput))
                {
                    sendOutputProvider.PrintOutput(textMessages.IncorrectOption);
                    DisplayMenu();
                }
                else
                {
                    exitCondition = true;
                }
            }
            while(!exitCondition);

            return userInput;
        }

        public bool ExecuteCommand(string command)
        {
            bool exitConfirmed = false;

            switch (command)
            {
                case nameof(settings.KeyDrawDot): draw += commands.DrawDot;  break;
                case nameof(settings.KeyDrawHorizontalLine): draw += commands.DrawHorizontalLine;  break;
                case nameof(settings.KeyDrawVerticalLine): draw += commands.DrawVerticalLine; break;
                case nameof(settings.KeyDrawSnowFlake): draw += commands.DrawSnowFlake;  break;
                case nameof(settings.KeyClear):
                    draw -= commands.DrawDot;
                    draw -= commands.DrawHorizontalLine;
                    draw -= commands.DrawVerticalLine;
                    draw -= commands.DrawSnowFlake;
                    break;
                case nameof(settings.KeyExit): exitConfirmed =  true; break;
            }

            return exitConfirmed;
        }

        public void Run()
        {
            string userInput = string.Empty;
            bool exitCondition = false;

            sendOutputProvider.PrintOutput(textMessages.Welcome);
            readInputProvider.ReadInput();

            do
            {
                if(draw!=null)
                {
                    try
                    {
                        draw.Invoke(this.board);
                    }
                    catch (Exception ex)
                    {
                        sendOutputProvider.PrintOutput(ex.ToString());
                        readInputProvider.ReadInput();
                    }
                    
                }

                DisplayMenu();
                userInput = InputProcessing();
                exitCondition = ExecuteCommand(settings.AllMenuKeys[userInput]);
            }
            while (!exitCondition);           
        }
    }
}
