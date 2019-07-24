using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyType = System.Int32;

namespace HW2
{
 

    public class Game
    {
        private readonly TextMessages textMessages;
        private readonly Settings settings;

        private readonly IStorageProvider storageProvider;
        private readonly IReadInputProvider readInputProvider;
        private readonly ISendOutputProvider sendOutputProvider;
        private readonly IRandomProvider randomProvider;
        private readonly ITextMessagesProvider textMessagesProvider;
        private readonly ISettingsProvider settingsProvider;      

        private MyType[] currentNumbers;
        private MyType inputValue = 1;

        public Game
            (

            IStorageProvider storageProvider,
            IReadInputProvider readInputProvider,
            ISendOutputProvider sendOutputProvider,
            IRandomProvider randomProvider,
            ITextMessagesProvider textMessagesProvider,
            ISettingsProvider settingsProvider
            )
        {

            this.storageProvider = storageProvider;
            this.readInputProvider = readInputProvider;
            this.sendOutputProvider = sendOutputProvider;
            this.randomProvider = randomProvider;
            this.textMessagesProvider = textMessagesProvider;
            this.settingsProvider = settingsProvider;

            try
            {
                this.textMessages = textMessagesProvider.getTextMessages();
            }
            catch (Exception e)
            {
                sendOutputProvider.printOutput(e.ToString());
                sendOutputProvider.printOutput("Language settings failed to load, using default values instead");
                this.textMessages = new TextMessages();
            }

            try
            {
                this.settings = settingsProvider.getSettings();
            }
            catch (Exception e)
            {
                sendOutputProvider.printOutput(e.ToString());
                sendOutputProvider.printOutput(textMessages.SettingLoadingError);
                this.settings = new Settings();
            }
            
            this.currentNumbers = new MyType[settings.NumberOfValues];
        }

        private void checkInput()
        {
            bool inputCheck = false;

            do
            {
                string input = readInputProvider.readInput();

                if (input==settings.GoBack)
                {
                    inputValue = 0;
                    inputCheck = true;
                }
                else
                {
                    try
                    {
                        inputValue = (MyType)Convert.ToUInt64(input);

                        if (currentNumbers.Contains(inputValue))
                        {
                            inputCheck = true;
                        }
                        else
                        {
                            sendOutputProvider.printOutput(textMessages.IncorrectChoice);
                        }
                    }
                    catch (Exception)
                    {
                        sendOutputProvider.printOutput(textMessages.IncorrectInputP1 + settings.GoBack + textMessages.IncorrectInputP2);
                    }
                }        
            }
            while (inputCheck == false);
        }

        public void run()
        {
            var randomNumbers = randomProvider.rand(settings);

            for (int i = 0; i < randomNumbers.Length; ++i)
            {
                currentNumbers[i] = randomNumbers[i];
            }

            sendOutputProvider.sendOutput(currentNumbers);

            bool exitCondition = false;

            while (!exitCondition)
            {
                checkInput();

                if (inputValue != 0)
                {
                    MyType[] tempNumbers = new MyType[settings.NumberOfValues];

                    try
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            tempNumbers[i] = checked(currentNumbers[i] * inputValue);
                        }

                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = tempNumbers[i];
                        }

                        storageProvider.push(inputValue);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(textMessages.MaxLevelReached);

                        if (storageProvider.Count > 0)
                        {
                            for (int i = 0; i < currentNumbers.Length; ++i)
                            {
                                currentNumbers[i] = currentNumbers[i] / storageProvider.peek();
                            }

                            storageProvider.pop();
                        }
                    }

                    sendOutputProvider.sendOutput(currentNumbers);
                }
                else
                {
                    if (storageProvider.Count > 0)
                    {
                        for (int i = 0; i < currentNumbers.Length; ++i)
                        {
                            currentNumbers[i] = currentNumbers[i] / storageProvider.peek();
                        }

                        sendOutputProvider.sendOutput(currentNumbers);

                        storageProvider.pop();
                    }
                    else
                    {
                        sendOutputProvider.printOutput(textMessages.EndReached);
                        Console.ReadKey();
                        exitCondition = true;
                    }
                }
            }
        }
    }
}

